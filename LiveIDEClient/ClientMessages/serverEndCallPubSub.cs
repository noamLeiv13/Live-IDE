using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The server send this message in pubsub after the call is finished (the admin leaves or there is one user in call)
    */
    public class serverEndCallPubSub:serverMsg
    {
        public serverEndCallPubSub(string topic)
        {
            Type = "111";
            Topic = topic;
        }

    }
}
