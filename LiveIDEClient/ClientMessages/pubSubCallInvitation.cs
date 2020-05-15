using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The server send this message when the user wants to create call - the suitable users have the option to connect this call.
    */
    public class pubSubCallInvitation : serverMsg
    {
        public List<string> usersToAdd { get; set; }
        public List<string> usersInCall { get; set; }
        public string callId { get; set; }
        public string admin { get; set; }
        public pubSubCallInvitation(string Admin, List<string> _usersToAdd, string _callId, List<string> _usersIncall, string topic = "General") : base("109", topic)// allways send the msg in General topic
        {
            admin = Admin;
            callId = _callId;
            usersToAdd = _usersToAdd;
            usersInCall = _usersIncall;
        }
    }
}
