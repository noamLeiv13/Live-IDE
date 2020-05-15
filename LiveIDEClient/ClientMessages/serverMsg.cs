using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    public class serverMsg
    {
        public string Type { get; set; }
        public string Topic { get; set; }
        public serverMsg(string type, string topic)
        {
            this.Type = type;
            this.Topic = topic;
        }
        public serverMsg() // empty ctor for JSON
        {
            this.Type = "";
            this.Topic = "";
        }
    }
}
