using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The server send this message in pubsub after the user added to call (the users needs to add him to cursors list)
    */
    public class UserAddedTocallPubSub: serverMsg
    {
        public string username;
        public UserAddedTocallPubSub(string topic, string _userName) : base("112", topic)
        {
            username = _userName;
        }
    }
}
