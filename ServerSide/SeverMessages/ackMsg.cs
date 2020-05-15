using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
  /*
  message that the server sends after he get message but he
  don't need to response in reqRep - Constraint following use of zeroMq
  */
    [Serializable]
    public class ackMsg
    {
        public string Type { get; set; }
        public ackMsg()
        {
            this.Type = "108";
        }
    }
}
