using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The server send this message after the user tries to sign in
    */
    public class signResponse: serverMsg
    {
        public bool Confirm { get; set; }
        public string Error { get; set; }
        public signResponse(string topic, bool confirm, string error) : base("100",topic)
        {
            this.Confirm = confirm;
            this.Error = error;
        }
    }
}
