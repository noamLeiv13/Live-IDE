using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The Client send this message when the user connect to call-
    he needs to get the initial text and the topic of the call
    */
    public class GetInitText: ClientMSG
    {
        public string password { get; set; }
        public string callId { get; set; }
        public GetInitText(string _callId,string userName, string Password) : base("208", userName)
        {
            callId = _callId;
            password = Password;
        }
    }



}
