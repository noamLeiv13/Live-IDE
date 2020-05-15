using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    // msg type = 204
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

