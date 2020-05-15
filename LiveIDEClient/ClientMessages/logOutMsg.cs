using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The Client send this message when the user wants to logout from his account.
    */
    public class logOutMsg : ClientMSG
    {
        public logOutMsg(string userName)
        {
            Username = userName;// set property of clientMsg
            Type = "209";// set property of clientMsg
        }

    }
}
