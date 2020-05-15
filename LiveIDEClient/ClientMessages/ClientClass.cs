using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace ClassLibrary
{
    /*
    class that we use for creating the connection with the server (reqRep and pubSub).
    This class sends the messages to the correct window 
    */
    public class ClientClass
    {
        private string topic;
        private CustomQueue<ClientMSG> toSend = new CustomQueue<ClientMSG>();
        private bool exit = false;
        private List<int> cursorsPos;
        private ZSocket requester;
        public event EventHandler<serverMsg> Message;// event that occures every time the client get message from the server
        private Thread callThread;
        private Thread PUBSUBThread;
        private Thread REQREPThread;
        public ClientClass()
        {
            var context = new ZContext();
            requester = new ZSocket(context, ZSocketType.REQ);
            requester.Connect("tcp://127.0.0.1:5555");
            toSend.NewMessage += HandleMessages;// every time that you get new messsage the function occuers
            topic = "General";
            PUBSUBThread = new System.Threading.Thread(new System.Threading.ThreadStart(PUBSUB));
            PUBSUBThread.Start();

        }

        public Thread getCallThread()
        {
            return callThread;
        }

        public void setTopic(string _topic)
        {
            this.topic = _topic;
        }

        public string getTopic()
        {
            return topic;
        }

        public void closeCallThread()
        {
            callThread.Abort();
        }
        /*
         this function create the pubsub connection with the server.
         The function sends the messages to the correct window 
         */ 
        private void PUBSUB()
        {
            Console.WriteLine("Subscriber started for Topic : {0}", "General");

            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000; //for checking
                subSocket.Connect("tcp://localhost:12346");// 12346- port
                subSocket.Subscribe("General");// listen to topic name
                Console.WriteLine("Subscriber socket connecting...");

                while (!exit)
                {
                    string messageReceived = subSocket.ReceiveFrameString();
                    try
                    {
                        dynamic recived = DeserializeServerMsg(messageReceived);
                        if (recived != null)
                            Message.Invoke(this, recived);// event that create changes in the form
                    }
                    catch (ThreadAbortException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }
            }
        }
        // create subscribe to spesipic topic (pubSub)
        public void createPubSub(string _topic)
        {
            callThread = new Thread(() => createPubSubConnection(_topic));
            callThread.Start();
        }

        /*
         this function create the pubsub connection with the server.
         The function sends the messages to the correct window
         *** we call this function after the user connect to new call.
         */
        private void createPubSubConnection(string _topic)
        {
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect("tcp://localhost:12346");
                subSocket.Subscribe(_topic);
                while (!exit)
                {
                    string messageReceived = subSocket.ReceiveFrameString();
                    try
                    {
                        dynamic recived = DeserializeServerMsg(messageReceived);
                        if (recived != null)
                            Message.Invoke(this, recived);// event that create changes in the form
                    }
                    catch (ThreadAbortException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }
            }
        }

        /*
        this function deserialize the messages we gets from the server
        */
        private dynamic DeserializeServerMsg(string recivedMsg)
        {
            dynamic msg = JsonConvert.DeserializeObject<serverInsertMsg>(recivedMsg);
            switch (msg.Type.ToString())
            {
                case "100":
                    return JsonConvert.DeserializeObject<signResponse>(recivedMsg);
                case "103":
                    return JsonConvert.DeserializeObject<registerResponse>(recivedMsg);
                case "104":
                    return JsonConvert.DeserializeObject<ServerCallResponse>(recivedMsg);
                case "105":
                    return JsonConvert.DeserializeObject<serverInsertMsg>(recivedMsg);
                case "106":
                    return JsonConvert.DeserializeObject<serverDelete>(recivedMsg);
                case "107":
                    return JsonConvert.DeserializeObject<userCursorPlaceServer>(recivedMsg);
                case "109":
                    return JsonConvert.DeserializeObject<pubSubCallInvitation>(recivedMsg);
                case "110":
                    return JsonConvert.DeserializeObject<sendInitText>(recivedMsg);
                case "111":
                    return JsonConvert.DeserializeObject<serverEndCallPubSub>(recivedMsg);
                case "112":
                    return JsonConvert.DeserializeObject<UserAddedTocallPubSub>(recivedMsg);
                case "113":
                    return JsonConvert.DeserializeObject<serverDisconnectUserFromCall>(recivedMsg);

            }
            return null;

        }

        public void closeThreads()
        {
            exit = true;
            PUBSUBThread.Abort();

        }


        /*
        The function occurs every time the event(event handler in CustomQueue toSend) happens.
        every time you wants to send message you call addTosend function from client class that call this function by event handler
        */
        private void HandleMessages(object sender, dynamic requestVar)
        {
            if (toSend.Count > 0)
            {
                ClientMSG _msg = null;
                bool isSuccessful = toSend.TryDequeue(out _msg);//save in _msg 
                if (isSuccessful)
                {
                    //send message to server
                    requestVar.Send(new ZFrame(JsonConvert.SerializeObject(_msg)));
                    using (ZFrame reply = requestVar.ReceiveFrame())
                    {
                        string recived = reply.ReadString();
                        dynamic serverMsg = DeserializeServerMsg(recived);// we can't know whats the object type
                        if (serverMsg != null )
                        {
                            ///111 = serverEndCallPubSub
                            if (serverMsg.Type == "111" && callThread != null)
                            {
                                callThread.Abort();// stop subscribing to call topic
                                callThread = null;
                            }
                            Message.Invoke(this, serverMsg);// an event that does changes in the form

                        }
                    }
                }
            }
        }

        /*
        this function add new message to CustomQueue.
        (by the event handler you call HandleMessages function )
        */
        public void addTosend(ClientMSG msg)
        {
            toSend.Enqueue(msg);
            toSend.Notify(requester);
        }
    }
}
