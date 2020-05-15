using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The server send this message in pubsub after the user delete somthing in the code textbox in call
    */
    public class serverDelete: serverMsg
    {
        public int Index { get; set; }
        public int Amount { get; set; }
        public string Username { get; set; }
        public serverDelete(string topic,int index, int amount, string username) : base("106",topic)
        {
            this.Index = index;
            this.Amount = amount;
            this.Username = username;
        }
    }
}
