using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The Client send this message when the user delete data from the Code textbox when he is in call.
    */
    public class deleteMsg : ClientMSG
    {
        public int Index { get; set; }
        public int Amount { get; set; }
        public string Topic { get; set; }
        // msg type 206
        public deleteMsg(string topic, string username, int index, int amount) : base("206", username)
        {
            Topic = topic;
            this.Index = index;
            this.Amount = amount;
        }
    }
}
