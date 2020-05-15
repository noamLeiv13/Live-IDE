using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    [Serializable]
    /*
    The server send this message after the user tries to craete call
    */
    public class ServerCallResponse: serverMsg
    {
        public bool checkNames { get; set; }// check if all user names are exsist in db
        public string error { get; set; }
        public ServerCallResponse(string Error, string topic, bool _checkNames) : base("104", topic)
        {
            error = Error;
            checkNames = _checkNames;
        }
    }
}
