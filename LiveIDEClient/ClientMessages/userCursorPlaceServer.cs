using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    /*
    The server send this message in pubSub after user change his cursor position
    */
    public class userCursorPlaceServer: serverMsg
    {
        public string Username { get; set; }
        public int CursorIndex { get; set; }
        public userCursorPlaceServer(string topic, string username, int cursorIndex): base("107", topic)
        {
            this.Username = username;
            this.CursorIndex = cursorIndex;
        }
    }
}
