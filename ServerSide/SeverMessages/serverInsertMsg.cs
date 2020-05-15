using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    [Serializable]
    /*
    The server send this message in pubsub after the user insert data to the code textbox
    */
    public class serverInsertMsg : serverMsg
    {
        public int Index { get; set; }
        public string Data { get; set; }
        public string Username { get; set; }
        public serverInsertMsg(string topic, int index, string data, string username) : base("105", topic)
        {
            this.Index = index;
            this.Data = data;
            this.Username = username;
        }
    }
}
