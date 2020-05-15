using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
using ScintillaNET;

namespace liveIde
{
    class CustomTextBox : ScintillaNET.Scintilla
    {
        public bool isIncall;
        public bool showingLiveTab = false;
        private const int WM_PAINT = 15;
        dynamic FontHeight;
        private List<userCursor> cursors = new List<userCursor>();

        public CustomTextBox()
        {
            FontHeight = this.Font.Height + 7;
        }

        public void clearUsers()
        {
            cursors.Clear();
        }

        public void addUser(string _userName, int _index =-1)
        {
            cursors.Add(new userCursor(_userName, _index));
        }
        public bool removeUser(string _userName)
        {
            foreach (userCursor user in cursors)
            {
                if(_userName == user.userName)
                {
                    cursors.Remove(user);
                    return true;
                }
            }
            return false;

        }

        //set new cursor position to specific user
        public void setIndex(int _index, string userName)
        {
            foreach (userCursor user in cursors)
            {
                
                    // new Font(pfc.Families[0], label1.Font.Size);
                if (user.userName == userName)
                {
                    user.index = _index;
                }
            }
        }




        public bool checkIfMouseOnUser()
        {
            bool check = false;
            foreach (userCursor user in cursors)
            {
                Point start = new Point(PointXFromPosition(user.index), PointYFromPosition(user.index));
                Point end = new Point(start.X, start.Y + FontHeight);
                Rectangle bounds = new Rectangle(start, new Size(2, FontHeight));
                Point mousePoint = PointToClient(MousePosition);
                if (bounds.Contains(mousePoint))
                {
                    if (!user.IsTipShown)
                    {
                        ToolTip displayUserName = new ToolTip();
                        displayUserName.Show(user.userName, this, start, 500);
                        user.IsTipShown = true;
                        check = true;
                    }
                }
                else
                {
                    foreach (userCursor _user in cursors)
                    {
                        _user.IsTipShown = false;
                    }
                }
            }
            return check;
                
        }

        protected override void WndProc(ref Message m)
        {
            Point cursorPos = new Point();
            Point textBoxLocation = new Point();
            Point end = new Point(cursorPos.X, cursorPos.Y + FontHeight);
            switch (m.Msg)
            {
                case WM_PAINT: // this is the WM_PAINT message  
                               // invalidate the TextBox so that it gets refreshed properly  
                    this.Invalidate();
                    // call the default win32 Paint method for the TextBox first  
                    base.WndProc(ref m);
                    // now use our code to draw extra stuff over the TextBox
                    if(isIncall && showingLiveTab)
                    {
                        using (Graphics g = Graphics.FromHwnd(this.Handle))
                        {
                            foreach (userCursor user in cursors)
                            {
                                if (user.index >= 0 && this.Text.Length >= user.index)
                                {
                                    textBoxLocation = new Point(PointXFromPosition(user.index), PointYFromPosition(user.index));
                                    Pen temp = new Pen(user._color);
                                    g.DrawLine(temp, textBoxLocation, new Point(textBoxLocation.X, textBoxLocation.Y + FontHeight));

                                }
                            }
                        }
                    }
                    
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }


    }
}
