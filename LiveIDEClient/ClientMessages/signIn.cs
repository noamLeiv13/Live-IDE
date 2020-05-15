using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The client send this message when the user wants to sign in
    */
    public class signIn : ClientMSG
    {
        public string Password { get; set; }
        public signIn(string username, string password) : base("200", username)
        {
            this.Password = password;
        }
    }
}
