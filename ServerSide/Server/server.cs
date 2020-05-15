using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ZeroMQ;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using library;
using System.Collections.Concurrent;
using LiteDB;


namespace Server
{
    public class server
    {
        private  Dictionary<string, List<string>> dictionary;
        private  Random random = new Random();
        private  ConcurrentQueue<dynamic> toSend = new ConcurrentQueue<dynamic>();
        private  List<string> connectedUsers = new List<string>();
        private  bool exit = false;
        private  int callNum;

        public server()
        {
            callNum = 0;
            dictionary = new Dictionary<string, List<string>>();
            List<string> temp = new List<string>();
            temp.Add("");
            temp.Add(callNum.ToString());
            dictionary.Add("General", temp);
            Thread PUBSUBThread = new System.Threading.Thread(new System.Threading.ThreadStart(PUBSUB));
            PUBSUBThread.Start();
        
            Thread REQREPThread = new System.Threading.Thread(new System.Threading.ThreadStart(REQREP));
            REQREPThread.Start();
        }

        private static dynamic DeserializeClientMsg(string recivedMsg)
        {
            dynamic msg = JsonConvert.DeserializeObject<ClientMSG>(recivedMsg);
            switch (msg.Type.ToString())
            {
                case "200":
                    return JsonConvert.DeserializeObject<signIn>(recivedMsg);
                case "203":
                    return JsonConvert.DeserializeObject<registerMsg>(recivedMsg);
                case "204":
                    return JsonConvert.DeserializeObject<createCallMsg>(recivedMsg);
                case "205":
                    return JsonConvert.DeserializeObject<InsertMsg>(recivedMsg);
                case "206":
                    return JsonConvert.DeserializeObject<deleteMsg>(recivedMsg);
                case "207":
                    return JsonConvert.DeserializeObject<userCursorPlaceServer>(recivedMsg);
                case "208":
                    return JsonConvert.DeserializeObject<GetInitText>(recivedMsg);
                case "209":
                    return JsonConvert.DeserializeObject<logOutMsg>(recivedMsg);
                case "211":
                    return JsonConvert.DeserializeObject<leaveCall>(recivedMsg);
                case "212":
                    return JsonConvert.DeserializeObject<addUsersToCall>(recivedMsg);
            }
            return null;
        }

        public string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //send pubsub messages
        public void PUBSUB()
        {
            using (var pubSocket = new PublisherSocket())
            {
                Console.WriteLine("Publisher socket binding...");
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind("tcp://*:12346");

                while (!exit)
                {
                    if (toSend.Count > 0)
                    {
                        string _tosend = "";
                        dynamic _msg = null;
                        bool isSuccessful = toSend.TryDequeue(out _msg);
                        if (isSuccessful && _msg != null)
                        {
                            switch (_msg.Type.ToString())
                            {
                                case "104":
                                    ServerCallResponse CallResponse = (ServerCallResponse)_msg;
                                    _tosend = JsonConvert.SerializeObject(CallResponse);
                                    pubSocket.SendMoreFrame(CallResponse.Topic).SendFrame(_tosend);
                                    break;
                                case "105":
                                    serverInsertMsg insertMsg = (serverInsertMsg)_msg;
                                    _tosend = JsonConvert.SerializeObject(insertMsg);
                                    pubSocket.SendMoreFrame(insertMsg.Topic).SendFrame(_tosend);
                                    break;
                                case "106":
                                    serverDelete delMsg = (serverDelete)_msg;
                                    pubSocket.SendMoreFrame(delMsg.Topic).SendFrame(JsonConvert.SerializeObject(delMsg));
                                    break;
                                case "107":
                                    userCursorPlaceServer changeCursorPos = (userCursorPlaceServer)_msg;
                                    pubSocket.SendMoreFrame(changeCursorPos.Topic).SendFrame(JsonConvert.SerializeObject(changeCursorPos));
                                    break;
                                case "109":
                                    pubSubCallInvitation initMsg = (pubSubCallInvitation)_msg;
                                    pubSocket.SendMoreFrame(initMsg.Topic).SendFrame(JsonConvert.SerializeObject(initMsg));
                                    break;
                                // case "110": not in pubsub (reqRep)

                                case "111":
                                    serverEndCallPubSub endCallMsg = (serverEndCallPubSub)_msg;
                                    _tosend = JsonConvert.SerializeObject(endCallMsg);
                                    pubSocket.SendMoreFrame(endCallMsg.Topic).SendFrame(_tosend);
                                    break;
                                case "112":
                                    UserAddedTocallPubSub addUserToCall = (UserAddedTocallPubSub)_msg;
                                    pubSocket.SendMoreFrame(addUserToCall.Topic).SendFrame(JsonConvert.SerializeObject(addUserToCall));
                                    break;
                                case "113":
                                    serverDisconnectUserFromCall disconnect = (serverDisconnectUserFromCall)_msg;
                                    pubSocket.SendMoreFrame(disconnect.Topic).SendFrame(JsonConvert.SerializeObject(disconnect));
                                    break;

                            }
                        }

                    }
                }
            }
        }
        // this function check if user is connected to the server (exsist in connectedUsers)
        private bool checkUserConnected(string name)
        {
            foreach (string User in connectedUsers)
            {
                if (User == name)
                {
                    return true;
                }
            }
            return false;
        }

        // this function check if userName and password are valid (register)
        private static string checkUserAndPassword(string userName, string password)
        {
            using (var db = new LiteDatabase(@"userData.db"))
            {
                var users = db.GetCollection<User>("users");
                dynamic userData = users.FindOne(x => x.userName.Equals(userName));
                if (userData == null)// if the username is not exsit in db
                {
                    // check password
                    if (password.Length < 4)
                    {
                        return "Password is too short";
                    }
                    return "";
                }
                return "Username already used";
            }
        }

        //reqrep connection
        public void REQREP()
        {
            int index = -1;
            // Create 
            using (var context = new ZContext())
            using (var responder = new ZSocket(context, ZSocketType.REP))
            {
                // Bind
                responder.Bind("tcp://*:5555");

                while (!exit)
                {
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        int countUsers = 0;
                        List<string> usersInCall;
                        List<string> _tempList;
                        string UsernameSender = "";
                        dynamic userData;
                        string recived = request.ReadString();
                        List<string> tempList = new List<string>();
                        dynamic recivedMsg = DeserializeClientMsg(recived);
                        dynamic _msg = null;
                        dynamic pubSubMsg = null;
                        bool connected = false;
                        if (recivedMsg != null && (recivedMsg.Type == "200")) //if the client needs a reply message (signin message)
                        {

                            using (var db = new LiteDatabase(@"userData.db"))
                            {
                                var users = db.GetCollection<User>("users");
                                UsernameSender = recivedMsg.Username;// every user have uniqe userName
                                userData = users.FindOne(x => x.userName.Equals(UsernameSender));
                            }
                            connected = checkUserConnected(UsernameSender);// checks in connectes users list
                            if (userData != null && recivedMsg.Username == ((User)userData).userName && recivedMsg.Password == ((User)userData).password && !connected)// check if user is exsist in db
                            {
                                _msg = new signResponse("", true, "");
                                connectedUsers.Add(UsernameSender);
                            }
                            else
                            {
                                string error = "An error occurred while sign inUsername or password is incorrect, please try again";
                                if (userData == null)
                                    error = "Username does not exist, please try again";
                                else if ((recivedMsg.Username != ((User)userData).userName) || (recivedMsg.Password != ((User)userData).password))
                                    error = "Username or password is incorrect, please try again";
                                else if (connected)
                                    error = "The user is already connected";
                                _msg = new signResponse("", false, error);
                            }

                        }
                        if (recivedMsg != null && (recivedMsg.Type == "203"))//if the client needs a reply message (register message)
                        {
                            string errorMsg = checkUserAndPassword(recivedMsg.Username, recivedMsg.Password);
                            if (errorMsg == "")
                            {
                                //create the new user
                                User tempUser = new User
                                {
                                    userName = recivedMsg.Username,
                                    password = recivedMsg.Password
                                };
                                using (var db = new LiteDatabase(@"userData.db"))
                                {
                                    var users = db.GetCollection<User>("users");

                                    users.Insert(tempUser);// insert new user
                                }
                                _msg = new registerResponse("", true, errorMsg);
                            }
                            else
                            {
                                _msg = new registerResponse("", false, errorMsg);
                            }

                        }
                        else  // if the client doesn't need a reply about his msg the server will send ack msg
                        {
                            if (recivedMsg != null)
                            {
                                switch (recivedMsg.Type.ToString())// because it dynamic object the switch case dont know that the property is string
                                {
                                    // craete call message
                                    case "204":
                                        callNum++;
                                        string randomText = RandomString(random.Next(22, 42));
                                        tempList.Add(recivedMsg.Text);
                                        tempList.Add(callNum.ToString());
                                        tempList.Add(recivedMsg.Username);// the name of the call admin
                                        foreach (string s in (List<string>)recivedMsg.users)
                                        {
                                            // check if the uer didn't add himself and if he connected - he can get the call
                                            if (s != recivedMsg.Username && checkUserConnected(s))
                                            {
                                                tempList.Add(s);
                                            }
                                            else
                                            {
                                                _msg = new ServerCallResponse("", "", false);// without Topic - in request reply 
                                            }

                                        }
                                        dictionary.Add(randomText, tempList);
                                        if (_msg == null)
                                            _msg = new ServerCallResponse("", randomText, true);// without Topic - in request reply 
                                        usersInCall = new List<string>();
                                        countUsers = 0;
                                        foreach (string user in tempList)
                                        {
                                            if (countUsers > 1)
                                            {
                                                usersInCall.Add(user);
                                            }
                                            countUsers++;
                                        }
                                        // send invitation to the call to the correct users
                                        pubSubMsg = new pubSubCallInvitation(recivedMsg.Username, recivedMsg.users, callNum.ToString(), usersInCall);//callNum.ToString() = call id
                                        toSend.Enqueue(pubSubMsg);
                                        break;
                                    case "205":
                                        index = recivedMsg.Index;
                                        if (index <= dictionary[recivedMsg.Topic][0].Length + 1)// check if the insert is vallid
                                        {
                                            // there is bug in scintila that show cursor position 1 when the text is empty
                                            if (dictionary[recivedMsg.Topic][0] == "" && index == 1)
                                            {
                                                dictionary[recivedMsg.Topic][0] = dictionary[recivedMsg.Topic][0].Insert(index - 1, recivedMsg.Data);
                                                pubSubMsg = new serverInsertMsg(recivedMsg.Topic.ToString(), index - 1, recivedMsg.Data.ToString(), recivedMsg.Username.ToString());
                                            }
                                            else if (index - dictionary[recivedMsg.Topic][0].Length == 1)
                                            {
                                                dictionary[recivedMsg.Topic][0] = dictionary[recivedMsg.Topic][0].Insert(index - 1, recivedMsg.Data);
                                                pubSubMsg = new serverInsertMsg(recivedMsg.Topic.ToString(), index - 1, recivedMsg.Data.ToString(), recivedMsg.Username.ToString());
                                            }
                                            else
                                            {
                                                dictionary[recivedMsg.Topic][0] = dictionary[recivedMsg.Topic][0].Insert(index, recivedMsg.Data);
                                                pubSubMsg = new serverInsertMsg(recivedMsg.Topic.ToString(), index, recivedMsg.Data.ToString(), recivedMsg.Username.ToString());
                                            }
                                            toSend.Enqueue(pubSubMsg);

                                        }
                                        break;
                                    case "206":
                                        // delete message
                                        index = recivedMsg.Index;
                                        int amount = recivedMsg.Amount;
                                        // <
                                        if (recivedMsg.Index <= dictionary[recivedMsg.Topic][0].Length)//dictionary[recivedMsg.Topic][0]  = code
                                        {
                                            dictionary[recivedMsg.Topic][0] = dictionary[recivedMsg.Topic][0].Remove(recivedMsg.Index, recivedMsg.Amount);
                                        }
                                        //send to all users about the change
                                        pubSubMsg = new serverDelete(recivedMsg.Topic.ToString(), index, amount, recivedMsg.Username.ToString());
                                        toSend.Enqueue(pubSubMsg);
                                        break;
                                    case "207":
                                        //change in user cursor position
                                        pubSubMsg = new userCursorPlaceServer(recivedMsg.Topic, recivedMsg.Username, recivedMsg.CursorIndex);
                                        toSend.Enqueue(pubSubMsg);
                                        break;


                                    case "208":
                                        //getInitText = the user ask for the initial text of the call he connected to/ 
                                        // chck if the user is invited
                                        if (checkUserInvited(recivedMsg.Username, recivedMsg.callId))
                                        {
                                            // check if the user didn't fake his identity - send the correct password
                                            using (var db = new LiteDatabase(@"userData.db"))
                                            {
                                                var users = db.GetCollection<User>("users");
                                                UsernameSender = recivedMsg.Username;
                                                userData = users.FindOne(x => x.userName.Equals(UsernameSender));
                                            }
                                            if (userData != null && recivedMsg.Username == ((User)userData).userName && recivedMsg.password == ((User)userData).password)// check if user is exsist in db
                                            {

                                                foreach (var temp in dictionary)
                                                {
                                                    if (temp.Value[1] == recivedMsg.callId)
                                                    {
                                                        //list[0] = code(text), list[1] = call id
                                                        //temp.Key = code , temp.Value[0] = topic
                                                        _msg = new sendInitText(temp.Key, (temp.Value[0]));
                                                        pubSubMsg = new UserAddedTocallPubSub(temp.Key, recivedMsg.Username);
                                                        toSend.Enqueue(pubSubMsg);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    // user logout
                                    case "209":
                                        if (recivedMsg.Username != "")
                                            connectedUsers.Remove(recivedMsg.Username);// remove user from connected users list
                                        //
                                        break;
                                    case "211":
                                        // disconnect from call = leaveCall
                                        if (recivedMsg.topic != "General" && recivedMsg.topic != "")// if the topic is valid (topic of call)
                                        {
                                            _tempList = dictionary[recivedMsg.topic];
                                            //if there is 4 items in list there is 2 users in call - now one close the call
                                            if (_tempList.Count() == 4 || (_tempList.Count() > 2 && dictionary[recivedMsg.topic][2] == recivedMsg.Username))//dictionary[recivedMsg.topic][2] = admin user - thre is one user in call or admin closed the call
                                            {
                                                // delete call
                                                dictionary[recivedMsg.topic].Clear();
                                                dictionary.Remove(recivedMsg.topic);
                                                pubSubMsg = new serverEndCallPubSub(recivedMsg.topic);
                                                toSend.Enqueue(pubSubMsg);
                                            }
                                            else if (_tempList.Count() > 2)
                                            {
                                                dictionary[recivedMsg.topic].Remove(recivedMsg.Username);// remove user from call
                                                pubSubMsg = new serverDisconnectUserFromCall(recivedMsg.Username, recivedMsg.topic);
                                                toSend.Enqueue(pubSubMsg);
                                            }
                                        }
                                        else if (recivedMsg.callId != "")// if the user isn't in call he need to use call id to delete himself from the call (at the begining the user don't get in pubsub the call topic after checking in db if he is invitted to the call)
                                        {
                                            //temp.Key = topic 
                                            List<string> keys = new List<string>(dictionary.Keys);
                                            foreach (string key in keys)
                                            {
                                                // close the call if one user will stay in her. [0] = ,[1] = , others = users
                                                if (dictionary[key].Count == 4 && dictionary[key][1] == recivedMsg.callId)// close call
                                                {
                                                    dictionary[key].Clear();
                                                    dictionary.Remove(key);
                                                    pubSubMsg = new serverEndCallPubSub(key);
                                                    toSend.Enqueue(pubSubMsg);
                                                }
                                                else if (dictionary[key].Count > 2)
                                                {
                                                    dictionary[key].Remove(recivedMsg.Username);// remove user from call
                                                    pubSubMsg = new serverDisconnectUserFromCall(recivedMsg.Username, key);
                                                    toSend.Enqueue(pubSubMsg);
                                                }

                                            }
                                        }

                                        break;
                                    case "212":
                                        //addUsersToCall 
                                        // send to the users invitation about the call
                                        _tempList = dictionary[recivedMsg.Topic];
                                        usersInCall = new List<string>();
                                        countUsers = 0;
                                        foreach (string user in _tempList)
                                        {
                                            if (countUsers > 1)
                                            {
                                                usersInCall.Add(user);
                                            }
                                            countUsers++;
                                        }
                                        //dictionary[recivedMsg.Topic][2] = admin of the call
                                        // default topic = general
                                        //_tempList[1] = call id
                                        pubSubMsg = new pubSubCallInvitation(dictionary[recivedMsg.Topic][2], recivedMsg.users, _tempList[1], usersInCall);
                                        toSend.Enqueue(pubSubMsg);
                                        foreach (string s in recivedMsg.users)
                                        {
                                            _tempList.Add(s);// add the new user to call users list
                                        }
                                        dictionary[recivedMsg.Topic] = _tempList;
                                        break;
                                }
                            }
                            if (_msg == null)
                            {
                                _msg = new ackMsg();// send an empty reply to fill the protocol
                            }
                        }
                        //send message to user
                        string msgToSend = JsonConvert.SerializeObject(_msg);
                        responder.Send(new ZFrame(msgToSend));
                    }
                }
            }
        }
        // check if user is exsist in the users who invited to list
        public bool checkUserInvited(string username, string callId)
        {
            int count = 0;
            foreach (var temp in dictionary)
            {
                //dictionary[x].value[1] = callid
                if (temp.Value[1] == callId)
                {
                    count = 0;
                    foreach (string s in temp.Value)
                    {
                        count++;
                        if (s == username && count > 1)//check if the username is invited to call. list[0] is code(text) list[1] = call id, other places are users
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }

    }
}
