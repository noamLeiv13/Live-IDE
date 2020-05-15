using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The server send this message in pubsub after the user leave call (the users in the call needs to delete his cursor)
    */
    public class serverDisconnectUserFromCall : serverMsg
    {
        public string username { get; set; }
        public serverDisconnectUserFromCall(string _username, string topic) : base("113", topic)
        {
            username = _username;
        }

    }
}
