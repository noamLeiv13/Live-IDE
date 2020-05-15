using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /*
    The Client send this message when the user wants to create new call.
    */
    [Serializable]
    public class createCallMsg : ClientMSG
    {
        public string Text { get; set; }
        public string Topic { get; set; }
        public List<string> users { get; set; }
        public createCallMsg(string topic, string username, string text, List<string> Users) : base("204", username)
        {
            users = Users;
            Topic = topic;
            this.Text = text;
        }
    }
}
