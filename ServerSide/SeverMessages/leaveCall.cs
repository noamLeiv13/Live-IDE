using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    [Serializable]
    /*
    The Client send this message when the user wants to leave the call.
    */
    public class leaveCall: ClientMSG
    {
        public string topic { get; set; }
        public string callId { get; set; }
        public leaveCall(string userName, string _topic, string _callId = "")
        {
            topic = _topic;
            Username = userName;// set property of clientMsg
            Type = "211";// set property of clientMsg
            callId = _callId;
        }
    }
}
