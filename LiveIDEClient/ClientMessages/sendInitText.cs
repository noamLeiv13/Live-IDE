using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
   The server send to the user the topic and initial text when the user accept call
   */
    public class sendInitText:serverMsg
    {
        public string code { get; set; }// the data of the call
        public sendInitText(string topic, string _code) : base("110", topic)
        {
            code = _code;
        }
    }
}
