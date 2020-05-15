using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    [Serializable]
    /*
    The Client send this message when the user insert data to the Code textbox when he is in call.
    */
    public class InsertMsg : ClientMSG
    {
        public int Index { get; set; }
        public string Data { get; set; }
        public string Topic { get; set; }
        public InsertMsg(string topic, string username, int index, string data) : base("205", username)
        {
            Topic = topic;
            this.Index = index;
            this.Data = data;
        }
    }
}
