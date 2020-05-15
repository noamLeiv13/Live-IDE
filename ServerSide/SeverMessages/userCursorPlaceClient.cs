using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    [Serializable]
    /*
    The Client send this message when the user change his cursor position in call
    */
    public class userCursorPlaceClient: ClientMSG
    {
        public int CursorIndex { get; set; }
        public string Topic { get; set; }
        public userCursorPlaceClient(string topic, string username, int cursorIndex): base("207", username)
        {
            Topic = topic;
            this.CursorIndex = cursorIndex;
        }
    }
}
