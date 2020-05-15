using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
     message that the server sends after he get message but he
     don't need to response in reqRep - Constraint following use of zeroMq
     */
    public class ackMsg
    {
        public string Type { get; set; }
        public ackMsg()
        {
            this.Type = "108";
        }
    }
}
