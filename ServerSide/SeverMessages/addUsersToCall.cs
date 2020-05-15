using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    /*
    The Client send this message when the user wants to add users to call.
    */
    [Serializable]
    public class addUsersToCall : ClientMSG
    {
        public string Text { get; set; }
        public string Topic { get; set; }
        public List<string> users { get; set; }
        public addUsersToCall(string topic, string username, List<string> Users) : base("212", username)
        {
            users = Users;
            Topic = topic;
            users = Users;
        }
    }
}
