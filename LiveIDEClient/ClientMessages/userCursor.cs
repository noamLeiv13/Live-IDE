using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
namespace ClassLibrary
{
    /*
    The data we save about every user cursor in call
    */
    public class userCursor
    {

        public string userName;
        public int index;
        public bool IsTipShown;
        public Color _color;
        public userCursor(string _userName, int _index)
        {
            Random rnd = new Random();

            userName = _userName;
            index = _index;
            IsTipShown = false;
            _color = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));//values ->0 -255


        }
    }
}
