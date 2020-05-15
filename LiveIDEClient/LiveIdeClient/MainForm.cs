using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScintillaNET;
using ScintillaNET.Demo.Utils;
using ClassLibrary;
using System.Text.RegularExpressions;
using Tulpep.NotificationWindow;

namespace liveIde
{
    public partial class MainForm : Form
    {
        private ClientClass client;
        private bool SelectedText;
        private bool changeFromServer;
        private bool isSpecialAction; 
        private bool checkIndentationGuides;
        private bool isInCall;
        private bool checkWhiteSpace;
        private bool insideChange;
        private bool hiddenForm;
        private const int imageSize =  25;
        private List<Label> tabs;
        private Dictionary<Label, string> tabsData;// the text(code) in every tab
        private Dictionary<Label, PictureBox> closeButtons;//there is close button in every tab
        private Label currShowTab;
        private PictureBox closeImage;
        private string userName;//
        private string password;//
        private string topic;// topic for the server, he needs to send that to other users
        private string _selectedText;// if part from the text is selected when pressing on key
        ToolTip toolTip1;
        private signInWindow signIn;//the main window - used for logOut
        private Point lastMousePos;
        private int lastCursorPos = -1;
        private int liveTabIndex;
        private int countCalls;



        public void setUserName(string _userName)
        {
            userName = _userName;
        }

        public void setPassword(string _password)
        {
            password = _password;
        }

        public void setTopic(string _topic)
        {
            topic = _topic;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            countCalls = 0;
            endCall.Enabled = false;
            addUsers.Enabled = false;
            hiddenForm = false;
            isInCall = false;
            SelectedText = false;
            isSpecialAction = false;
            changeFromServer = false;
            insideChange = false;
            checkWhiteSpace = false;
            checkIndentationGuides = true;
            _selectedText = "";
            liveTabIndex = 0;
            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(callButton, "create call");
            
            // CREATE CONTROL

            // BASIC CONFIG
            code.Dock = System.Windows.Forms.DockStyle.Fill;
            //code.TextChanged += (this.OnTextChanged);

            // INITIAL VIEW CONFIG
            code.WrapMode = WrapMode.None;
            code.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            //disable hotkeys
            InitHotkeys();
           
        }

        // update call options buttons
        public void cancelCallUpdateButtons()
        {
            callButton.Enabled = true;
            endCall.Enabled = false;
            addUsers.Enabled = false;
        }

        public void setSignInWindow(signInWindow _signIn)
        {
            signIn = _signIn;
        }

        public void ChangeToshowForm()
        {
            hiddenForm = false;
        }

        //disable hotkeys
        private void InitHotkeys()
        {
            // remove conflicting hotkeys from scintilla
            code.ClearCmdKey(Keys.Control | Keys.F);
            code.ClearCmdKey(Keys.Control | Keys.L);
            code.ClearCmdKey(Keys.Control | Keys.U);

        }

        // handele key events
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            IDataObject iData;
            string pasteText = "";
            if(code.SelectedText.Length >0)
            {
                SelectedText = true;
                _selectedText = code.SelectedText;
            }
            else
            {
                SelectedText = false;
                _selectedText = "";
            }
            if (keyData == (Keys.Control | Keys.V))//user pressed control+v
            {
                isSpecialAction = true;
                iData = Clipboard.GetDataObject();// get data from the board (control + c data)
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    pasteText = (String)iData.GetData(DataFormats.Text);// convert conrol +c data to string
                    if (code.SelectedText.Length > 0)// for pasteing on selection text
                    {
                        if(code.SelectionStart - 1 > 0)
                        {
                            tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(code.SelectionStart - 1, code.SelectedText.Length);
                            client.addTosend(new deleteMsg(topic, userName, code.SelectionStart - 1, code.SelectedText.Length));//deleteing when pasting
                            client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart - 1));
                        }
                        else
                        {
                            tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(0, code.SelectedText.Length);
                            client.addTosend(new deleteMsg(topic, userName, 0, code.SelectedText.Length));//deleteing when pasting
                            client.addTosend(new userCursorPlaceClient(topic, userName, 0));
                        }
                            
                    }
                    string tosendStr = paddingEndOfLine(pasteText);
                    client.addTosend(new InsertMsg(topic, userName, code.SelectionStart, tosendStr));
                    tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Insert(code.SelectionStart, tosendStr);

                    client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart + tosendStr.Length));
                }
            }
            else if (keyData == (Keys.Control | Keys.X) && topic != "General" && code.showingLiveTab)
            {
                isSpecialAction = true;
                tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(code.SelectionStart, code.SelectedText.Length);
                client.addTosend(new deleteMsg(topic, userName, code.SelectionStart, code.SelectedText.Length));//deleteing when preesing control +x
                client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart));
                isSpecialAction = true;
            }
            else if (keyData == (Keys.Back)  && code.Text != "")//backspace - delete
            {
                isSpecialAction = true;// when you press back you get in to textchanged function 
                if (code.SelectionStart > 0 && code.Text[code.SelectionStart - 1] == '\n')// deleteing end of line - need to delete \n and /r
                {
                    if (topic != "General" && code.showingLiveTab)
                    {
                        isSpecialAction = true;
                        tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(code.SelectionStart - 2, 2);
                        client.addTosend(new deleteMsg(topic, userName, code.SelectionStart - 2, 2));
                        client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart - 2));
                    }
                    int posBeforeDelete = code.SelectionStart;
                    changeFromServer = true;// when you delete /r/n it opens the textchanged function****
                    code.Text = code.Text.Remove(code.SelectionStart - 2, 2);//delete '/r'
                    code.SelectionStart = posBeforeDelete - 2;// update your cursor position
                    changeFromServer = false;
                    return true;
                }
                else if (topic != "General" && code.showingLiveTab)
                {
                   
                    if (code.Text != ""  && code.SelectionStart >= 0 && code.SelectedText.Length > 0)
                    {
                        isSpecialAction = true;
                        int index = code.SelectionStart;
                        pasteText = code.SelectedText;
                        tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(index, code.SelectedText.Length);
                        client.addTosend(new deleteMsg(topic, userName, index, code.SelectedText.Length));//deleteing selected text
                        client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart));
                    }
                    else if (code.Text != "" && code.SelectionStart != 0)// if form is 0 he can't delete anything and there is no changes in the text
                    {
                        if (code.Text[code.SelectionStart - 1] !='\n')// if you dont delete end of line (you excexute deleting end of line few lines before)
                        {
                            isSpecialAction = true;
                            tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(code.SelectionStart - 1, 1);
                            client.addTosend(new deleteMsg(topic, userName, code.SelectionStart - 1, 1));//delete one char
                            client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart - 1));
                        }
                    }
                }
            }
            else if (keyData == (Keys.Delete) && topic != "General" && code.showingLiveTab)// delete button is different then backspace
            {
                isSpecialAction = true;
                if (code.SelectionStart != code.Text.Length)// if you dont try to delete the end of the text (nothing)
                {
                    isSpecialAction = true;
                    if (code.SelectedText.Length > 0)
                    {
                        tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(code.SelectionStart - 1, code.SelectedText.Length);
                        client.addTosend(new deleteMsg(topic, userName,code.SelectionStart-1, code.SelectedText.Length));//deleteing selected text
                        client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart -1));
                    }
                    else
                    {
                        tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(code.SelectionStart - 1, 1);
                        client.addTosend(new deleteMsg(topic, userName,code.SelectionStart-1, 1));//deleteing selected text
                        client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart - 1));
                    }


                }

            }
            //don't do nothing when the user press control + z
            else if (keyData == (Keys.Control | Keys.Z)|| keyData == (Keys.Control | Keys.D))
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public MainForm(string data,ClientClass _client, string _username, string _topic, string _password, signInWindow _signin)
        {
            InitializeComponent();
            signIn = _signin;
            password = _password;
            userName = _username;
            topic = _topic;
            code.isIncall = false;
            code.Text = data;
            this.client = _client;
            lastMousePos = new Point(0, 0);
            tabs = new List<Label>();
            closeButtons = new Dictionary<Label, PictureBox>();
            tabsData = new Dictionary<Label, string>();
            this.KeyPreview = true;
            addTab("New Tab", "");
            client.Message += client_Message;// call the client_Message function every time the client get message from the server

        }

        public void setStartCall()
        {
            isInCall = true;
            endCall.Enabled = true;
        }

        //Handling received messages 
        private void client_Message(object sender, dynamic e)
        {
            bool checkUserCall = false;//check if the user is invited to call
            if (e.Type != "108" ) 
            {
                switch (e.Type.ToString())
                {
                    //serverCall response
                    case "104":
                        if (!hiddenForm)
                        {
                            if (e.checkNames)// if there is no error in creating the call
                            {
                                topic = e.Topic;// create subscribe to new topic
                                client.createPubSub(e.Topic);
                                // set call buttons to situation
                                InvokeIfNeeded(delegate ()
                                {
                                    changeFromServer = true;
                                    endCall.Enabled = true;
                                    code.isIncall = true;
                                    isInCall = true;
                                    code.showingLiveTab = true;
                                    liveTabIndex = tabs.IndexOf(currShowTab);
                                    showPressedTab(currShowTab, null);
                                    currShowTab.Text = "Live Tab " + countCalls;
                                    updateTabsLocations();
                                    countCalls++;
                                    addUsers.Enabled = true;                             
                                    changeFromServer = false ;

                                });
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Failed to create conversation: " + e.error, "Alert");
                                InvokeIfNeeded(delegate ()
                                {
                                    callButton.Enabled = true;
                                    endCall.Enabled = false;
                                    isInCall = false;
                                    code.isIncall = false;
                                    addUsers.Enabled = false;
                                });
                            }
                        }
                       
                        break;
                    case "105":
                        //insert msg
                        if (e.Username != userName)// if you didn't send the new data insert it to the text 
                        {
                            changeFromServer = true;
                            try
                            {
                                tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Insert(e.Index, e.Data);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("error occured");
                            }
                            Invoke((MethodInvoker)delegate
                            {
                                int cursorPos = code.SelectionStart;
                                if (code.showingLiveTab)
                                {
                                    code.Text = code.Text.Insert(e.Index, e.Data);
                                    if (e.Index < code.SelectionStart)
                                    {
                                        code.SelectionStart = cursorPos + e.Data.ToString().Length;
                                    }
                                }
                            });
                            changeFromServer = false;
                        }
                        break;
                    case "106":
                        //delete msg
                        if (e.Username != userName)// if you didn't send the new data use it for change the text 
                        {
                            if (e.Index < code.Text.Length)
                            {
                                changeFromServer = true;
                                try
                                {
                                    tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(e.Index, e.Amount);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("error occured");
                                }

                                Invoke((MethodInvoker)delegate {
                                    int cursorPos = code.SelectionStart;
                                    if (code.showingLiveTab)
                                    {
                                        code.Text = code.Text.Remove(e.Index, e.Amount);
                                        //change your cursor to the correct index
                                        if (e.Index < code.SelectionStart)
                                        {
                                            code.SelectionStart = cursorPos - e.Amount;
                                        }
                                    }
                                });
                                changeFromServer = false;
                            }
                        }

                        break;
                    case "107":
                        //userCursorPlaceServer - user change is cursor position in the call
                        if (e.Username != userName)
                            code.setIndex(e.CursorIndex, e.Username);// change the user cursor Pos

                        break;
                        //pubsub call invitation
                    case "109":
                        //check if you invited to the call
                        foreach (string s in (List<string>)e.usersToAdd)
                        {
                            if(s == userName)
                            {
                                checkUserCall = true;
                            }
                        }
                        if(checkUserCall && !hiddenForm)// if you invited
                        {
                            if(isInCall)// if you are in call you can't answer the call
                            {
                                client.addTosend(new leaveCall(userName, "", e.callId));// the user already in the call users list

                            }
                            else if (MessageBox.Show("Do you want to connect to a conversation created by " + e.admin, "New call", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //set call buttons to the situation
                                InvokeIfNeeded(delegate () {
                                    client.addTosend(new GetInitText(e.callId, userName, password));
                                    isInCall = true;
                                    endCall.Enabled = true;
                                    callButton.Enabled = false;
                                    addUsers.Enabled = true;
                                    code.isIncall = true;
                                    code.addUser(e.admin, 0);
                                });
                               // add users in call to display cursors cursors 
                                foreach (string name in (List<string>)e.usersInCall)
                                {
                                    if(name != userName)
                                    {
                                        code.addUser(name, 0);
                                    }
                                }
                            }
                            else
                            {
                                client.addTosend(new leaveCall(userName, "",e.callId));// the user already in the call users list
                            }
                        }
                        break;
                    case "110":
                        //sendInitText
                        //you get the initial text of the call you connected to
                        changeFromServer = true;
                        InvokeIfNeeded(delegate () {
                            tabsData[currShowTab] = code.Text;
                            addTab("Live Tab " + countCalls, e.code);
                            liveTabIndex = tabs.Count - 1;//live tab is the last tab
                            code.Text = e.code;
                            topic = e.Topic;
                            countCalls++;
                            code.showingLiveTab = true;
                            isInCall = true;
                            showPressedTab(tabs[liveTabIndex], null);
                            client.createPubSub(e.Topic);
                        });
                        changeFromServer = false;

                        break;
                    //serverEndCallPubSub - the call is closed
                    case "111":
                        InvokeIfNeeded(delegate () {
                            endCall.Enabled = false;
                            callButton.Enabled = true;
                            addUsers.Enabled = false;
                            isInCall = false;
                            topic = "General";
                            code.clearUsers();
                            code.isIncall = false;
                            liveTabIndex = 0;
                            showPressedTab(currShowTab, null); //update colors
                            System.Windows.Forms.MessageBox.Show("The call is end", "Alert", MessageBoxButtons.OK);
                            client.closeCallThread();
                        });
                        
                        break;
                    // add user to cursor position list
                    case "112":
                        if (userName != e.username && topic == e.Topic)
                        code.addUser(e.username, 0);
                        break;
                    case "113":
                        //serverDisconnectUserFromCall
                        //user leave the call - you need to delete him from cursor list
                        code.removeUser(e.username);
                        break;

                }
            }
        }

        //creating new file button in menu 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label lastTab = currShowTab;
            tabsData[currShowTab] = code.Text;
            code.Text = "";
            addTab("New Tab", "");//add tab and changed the last displyed tab color to default
            if (code.showingLiveTab)
            {
                code.showingLiveTab = false;
                lastTab.BackColor = Color.LightBlue;
            }
            
        }

        //Add new tab
        private void addTab(string name, string text)
        {
            Label newTab = new Label();
            newTab.ForeColor = Color.White;
            newTab.BorderStyle = BorderStyle.Fixed3D;
            tabs.Add(newTab);// add tab to tabs list
            //if the tab you added is not the first tab
            if (tabs.Count - 2 >= 0)
            {
                newTab.Left = tabs[tabs.Count - 2].Left + tabs[tabs.Count - 2].Width;
            }
            else//this is the first tab
            {
                newTab.Left = 0;
            }
            newTab.TextAlign = ContentAlignment.MiddleLeft;
            newTab.Top = 0;
            
            Image fakeImage = new Bitmap(1, 1); //we cannot use CreateGraphics() so the fake image is used to load the Graphics.
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(name, this.Font);// size of the text that will display in label

            newTab.Height = tabsPanel.Height;
            newTab.Width = (int)size.Width + imageSize + 5;
            newTab.Text = name;
            newTab.Click += new System.EventHandler(showPressedTab);
            tabsData.Add(newTab, text);

            //close option iin tab
            closeImage = new PictureBox();
            closeImage.Size = new Size(imageSize, imageSize);
            closeImage.Image = liveIde.Properties.Resources.closeBlack;
            closeImage.Location = new Point(newTab.Location.X + newTab.Width - closeImage.Width, newTab.Location.Y);
            closeImage.SizeMode = PictureBoxSizeMode.StretchImage;
            closeImage.Click += new System.EventHandler(closeTab);
            closeImage.MouseHover += new System.EventHandler(mouseHoverCloseButton);
            closeImage.MouseLeave += new System.EventHandler(mouseLeaveCloseButton);
            closeImage.BringToFront();
            closeButtons.Add(newTab, closeImage);
            tabsPanel.Controls.Add(closeImage);

            // if the new tab don't fit to the width of the form
            if (tabs[tabs.Count - 1].Left + newTab.Width > tabsPanel.Width)
            {
                updateTabsLocations();
            }
            tabsPanel.Controls.Add(newTab);
            closeImage.BringToFront();

            //change the color of the tab you display before creating the new tab to default
            if (currShowTab != null)
            {
                currShowTab.BackColor = Color.White;
                currShowTab.ForeColor = Color.Black;
            }
            currShowTab = newTab;
            newTab.BackColor = Color.FromArgb(39, 40, 34);// set tab color to display tab color
        }

        private void mouseHoverCloseButton(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = liveIde.Properties.Resources.close;
        }

        private void mouseLeaveCloseButton(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = liveIde.Properties.Resources.closeBlack;
        }

        //close tab function
        private void closeTab(object sender, EventArgs e)
        {
            //sender = close image of the tab you want to close
            Label tabForColsing = closeButtons.FirstOrDefault(x => x.Value == sender as PictureBox).Key;

            //if a tab is closed while there is a call
            if (isInCall && tabs.IndexOf(tabForColsing) < liveTabIndex)
            {
                liveTabIndex--;
            }
            else if (isInCall && tabs.IndexOf(tabForColsing) == liveTabIndex)
            {
                endCall_Click(null, null);
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// close conversation
            }

            // if you close all tabs it will create new empty tab
            if (tabs.Count == 1)
            {
                code.Text = "";
                addTab("New Tab", "");
                if (currShowTab == tabForColsing)
                {
                    showPressedTab(tabs[1], e);//show the new tab we created
                }
            }
            //try to close the last tab
            else if (tabs.IndexOf(tabForColsing) == tabs.Count - 1 && currShowTab == tabForColsing)
            {
                showPressedTab(tabs[tabs.Count - 2], e);
            }
            //if you delete the tab you display now
            else if (currShowTab == tabForColsing)
            {
                showPressedTab(tabs[tabs.IndexOf(tabForColsing) + 1], e);// display the next tab
            }
            // find the tab you want to delete by closing button
            foreach (var curr in closeButtons)
            {
                //remove the tab you want to delete
                if (curr.Value == sender as PictureBox)
                {
                    tabForColsing = curr.Key;
                    tabsData.Remove(curr.Key);
                    tabsPanel.Controls.Remove(curr.Key);
                    tabsPanel.Controls.Remove(curr.Value);
                    tabs.Remove(tabForColsing);
                }
            }
            //remove closing button that in the tab
            closeButtons.Remove(tabForColsing);
            updateTabsLocations();
        }

        private void updateTabsLocations()
        {
            int place = 0;
            PictureBox tempButton;// the var that we used for seting closeButtons locations
            foreach (Label currTab in tabs)
            {
                tempButton = closeButtons[currTab];
                Image fakeImage = new Bitmap(1, 1); //As we cannot use CreateGraphics() in a class library, so the fake image is used to load the Graphics.
                Graphics graphics = Graphics.FromImage(fakeImage);
                SizeF size = graphics.MeasureString(currTab.Text, this.Font);// siz e of the text that display in the tab
                currTab.Left = place;
                currTab.Width = (int)size.Width + imageSize + 5;// imageSize = close button image size
                tempButton.Location = new Point(currTab.Location.X + currTab.Width - closeImage.Width, currTab.Location.Y);
                place = currTab.Left + currTab.Width;
                tempButton.BringToFront();
            }
            if(tabs[tabs.Count - 1].Left + tabs[tabs.Count-1].Width > tabsPanel.Width)// if the tab din't fit in the panel
            {
                int toReduce = tabs[tabs.Count - 1].Left + tabs[tabs.Count - 1].Width - tabsPanel.Width;// The size to be reduced
                toReduce /= tabs.Count;//how much should shorten in every tab
                place = 0;
                foreach (Label currTab in tabs)
                {
                    currTab.Left = place;
                    tempButton = closeButtons[currTab];
                    currTab.Width -= toReduce;
                    tempButton.Location = new Point(currTab.Location.X + currTab.Width - closeImage.Width, currTab.Location.Y);
                    place = currTab.Left + currTab.Width;
                    tempButton.BringToFront();
                }
            }
        }
        //show tab after pressing him = set his color and show in code.Text the tabs data
        //sender = new display tab
        private void showPressedTab(object sender, EventArgs e)
        {
            changeFromServer = true; // don't send insert and delete for when switching tabs
            tabsData[currShowTab] = code.Text;// save the data of the displayed tab
            // set the display tab to normal color
            if (isInCall && code.showingLiveTab)
            {
                currShowTab.BackColor = Color.LightBlue;
                code.showingLiveTab = false;
            }
            else
            {
                currShowTab.BackColor = Color.White;
            }

            currShowTab.ForeColor = Color.Black;// set the display tab to normal color
            code.Text = tabsData[sender as Label];
            currShowTab = sender as Label;

            if (isInCall && tabs.IndexOf(currShowTab) == liveTabIndex)
            {
                currShowTab.BackColor = Color.Blue;// set the new display color
                code.showingLiveTab = true;
            }
            else
            {
                currShowTab.BackColor = Color.FromArgb(39, 40, 34);// set the new display color
            }
            currShowTab.ForeColor = Color.White;// set the new display color
            changeFromServer = false; // end of switching tabs
        }

        //exit button
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                code.Text = File.ReadAllText(path);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tabsData[currShowTab] = code.Text;
            code.Text = "";
            OpenFileDialog ofd = new OpenFileDialog();// allow to browse file from your pc
            ofd.Filter = "Text files (.cs)|* .cs";//filter for files you allow to open
            ofd.Title = "Open a file";
            if (ofd.ShowDialog() == DialogResult.OK)//if the user choosed file
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                code.Text = sr.ReadToEnd();
                char slash = (char)92;
                string[] temp =  ofd.FileName.Split(slash);
                addTab(temp[temp.Length - 1], sr.ReadToEnd());
                sr.Close();
            }
        }

        // save file
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save a file";
            sfd.Filter = "Text files (.cs)|* .cs";
            if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName != "")//if the user choosed file
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName);
                sw.Write(code.Text);
                sw.Close();
            }
        }
        //show the last text you delete (control+z)
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Undo();
        }
        //
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Redo();
        }

        // cut text 
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Copy();
            code.SelectionStart = code.SelectionStart;
            //code.SetSelection(0, 0);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.SelectAll();
        }

        //create shortcuts
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string temp = "";
            
            if(e.Control && e.KeyCode == Keys.F)
            {
                OpenSearch();
                TxtSearch.Visible = true;
            }

            // control + down button - the cursor scroll to the last place in text box
            if (e.Control && e.KeyCode == Keys.Down)
            {
                code.SelectionStart = code.Text.Length;
                code.SelectionEnd = code.SelectionStart;
            }
            // control + up button - the cursor scroll to the head of the text box
            if (e.Control && e.KeyCode == Keys.Up)
            {
                code.SelectionStart = 0;
                code.SelectionEnd = code.SelectionStart;
            }
            if(e.Control && e.KeyCode == Keys.V)
            {
                string pasteData = "";
                try
                {
                    pasteData = Clipboard.GetText();// get copied text
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }               
        }

        private void InitColors()
        {
            code.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }

        // init syntax colors
        private void InitSyntaxColoring()
        {

            // Configure the default style
            code.StyleResetDefault();
            code.Font = new Font("Consolas", 14);
            code.Styles[Style.Default].Font = "Consolas";
            code.Styles[Style.Default].Size = 14;
            code.Styles[Style.Default].BackColor = IntToColor(0x212121);
            code.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            code.StyleClearAll();

            // Configure the CPP lexer styles
            code.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            code.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            code.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            code.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            code.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            code.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            code.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            code.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            code.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            code.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            code.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            code.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            code.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            code.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            code.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            code.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            code.Lexer = Lexer.Cpp;

            code.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            code.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        //**** this functions are scintila function that we used

        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin()
        {

            code.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            code.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            code.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            code.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = code.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            code.MarginClick += code_MarginClick;
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = code.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = code.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            code.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            code.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            code.SetProperty("fold", "1");
            code.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            code.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            code.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            code.Margins[FOLDING_MARGIN].Sensitive = true;
            code.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                code.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                code.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            code.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            code.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            code.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            code.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            code.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            code.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            code.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            code.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void code_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {       
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = code.Lines[code.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion

        #region Drag & Drop File
        // to replace 
        public void InitDragDropFile()
        {

            code.AllowDrop = true;
            code.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            code.DragDrop += delegate (object sender, DragEventArgs e) {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        LoadDataFromFile(path);

                    }
                }
            };

        }

        #endregion


        #region Uppercase / Lowercase

        private void Lowercase()
        {

            // save the selection
            int start = code.SelectionStart;
            int end = code.SelectionEnd;

            // modify the selected text
            code.ReplaceSelection(code.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            code.SetSelection(start, end);
        }

        private void Uppercase()
        {

            // save the selection
            int start = code.SelectionStart;
            int end = code.SelectionEnd;

            // modify the selected text
            code.ReplaceSelection(code.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            code.SetSelection(start, end);
        }

        #endregion

        #region Indent / Outdent

        private void Indent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to indent,
            // although the indentation function exists. Pressing TAB with the editor focused confirms this.
            GenerateKeystrokes("{TAB}");
        }

        private void Outdent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to outdent,
            // although the indentation function exists. Pressing Shift+Tab with the editor focused confirms this.
            GenerateKeystrokes("+{TAB}");
        }

        private void GenerateKeystrokes(string keys)
        {
            HotKeyManager.Enable = false;
            code.Focus();
            SendKeys.Send(keys);
            HotKeyManager.Enable = true;
        }

        #endregion

        #region Quick Search Bar

        bool SearchIsOpen = false;

        private void OpenSearch()
        {

            SearchManager.SearchBox = TxtSearch;
            SearchManager.TextArea = code;

            if (!SearchIsOpen)
            {
                SearchIsOpen = true;
                InvokeIfNeeded(delegate () {
                    PanelSearch.Visible = true;
                    TxtSearch.Text = SearchManager.LastSearch;
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
            else
            {
                InvokeIfNeeded(delegate () {
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
        }
        private void CloseSearch(object sender, EventArgs e)
        {
            if (SearchIsOpen)
            {
                SearchIsOpen = false;
                InvokeIfNeeded(delegate ()
                {
                    TxtSearch.Visible = false;
                    PanelSearch.Visible = false;
                });
            }
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            CloseSearch(null,null);
        }

        private void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(false, false);
        }
        private void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(true, false);
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(true, true);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchManager.Find(true, false);
            }
           
        }

        #endregion
        //***** this functions are scintila function that we used


        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void runCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Invoke((MethodInvoker)delegate
            {
                hiddenForm = true;
                this.Hide();
                Form f2 = new cmdRun(code.Text,client,this, userName);
                f2.Show();
            });
        }

        private void showWhiteSpacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkWhiteSpace = !checkWhiteSpace; 
            if (checkWhiteSpace)
            {
                code.ViewWhitespace = WhitespaceMode.VisibleAlways;
            }
            else
            {
                code.ViewWhitespace = WhitespaceMode.Invisible;
            }
        }

        private void showIndentGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkIndentationGuides = !checkIndentationGuides;
            if (checkIndentationGuides)
            {
                code.IndentationGuides = IndentView.LookBoth;
            }
            else
            {
                code.IndentationGuides = IndentView.None;
            }
        }

        private void code_TextChanged(object sender, EventArgs e)
        {//            if (!isSpecialAction && !changeFromServer && topic != "General" && code.showingLiveTab)// if occuerd special action don't check foradding 1 char

            if (!isSpecialAction && !changeFromServer && topic != "General" && code.showingLiveTab)// if occuerd special action don't check foradding 1 char
            {
                string add = "";
                if(code.Text.Length >0)
                {
                    if(SelectedText)// if there is changed when part from the text is selected it comes to this function 2 times one for delete and one for insert
                    {
                        tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Remove(code.SelectionStart, _selectedText.Length);
                        client.addTosend(new deleteMsg(topic, userName, code.SelectionStart, _selectedText.Length));
                        client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart - _selectedText.Length));
                        _selectedText = "";
                        SelectedText = false;
                    }
                    else
                    {
                        add += code.Text[code.SelectionStart];
                        int index = code.SelectionStart;
                        if(code.Text[index] == '\r')// change from server
                        {
                            insideChange = true;
                            code.Text.Insert(index + 1, "\n");
                            insideChange = false;
                        }
                        string toaddStr = paddingEndOfLine(add);
                        client.addTosend(new InsertMsg(topic, userName, index, toaddStr));
                        tabsData[tabs[liveTabIndex]] = tabsData[tabs[liveTabIndex]].Insert(index, toaddStr);
                        client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart + toaddStr.Length));
                    }

                }
            }
            else if(isSpecialAction)
            {
                isSpecialAction = false;
            }
        }

        //add /n evry time after /r
        private string paddingEndOfLine(string input)
        {
            int times = Regex.Matches(input, "\r").Count;
            for(int i =0;i<times;i++)
            {
                int index = input.IndexOf("\r");
                if(index+1 < input.Length)
                {
                    input.Insert(index + 1, "\n");
                }
                else
                {
                    input += "\n";
                }
                
            }
            return input;
        }
        private void code_UpdateUI(object sender, UpdateUIEventArgs e)
        {
        }

        private void menu_Click(object sender, EventArgs e)
        {
            
        }

        //create call option
        private void callButton_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                hiddenForm = true;
                callButton.Enabled = false;
                addUsers.Enabled = true;
                createCall _f = new createCall(client, userName, "General", password,code.Text,this);//General - default topic
                this.Hide();
                _f.Show();
            });
        }

        // close call and logout from user before closing the program
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isInCall)
            {
                client.addTosend(new leaveCall(userName, topic));
                isInCall = false;
                code.showingLiveTab = false;
                liveTabIndex = 0;
                topic = "General";
                endCall.Enabled = false;
            }
            client.addTosend(new logOutMsg(userName));
            System.Threading.Thread.Sleep(100);// wait till logOutMsg Will send
            client.closeThreads();
            Environment.Exit(Environment.ExitCode);
        }

        //end call option
        private void endCall_Click(object sender, EventArgs e)
        {
            if(isInCall)
            {
                code.showingLiveTab = false;
                client.addTosend(new leaveCall(userName,topic));
                endCall.Enabled = false;
                addUsers.Enabled = false;
                callButton.Enabled = true;
                isInCall = false;
                code.isIncall = false;
                topic = "General";
                code.clearUsers();
                client.closeCallThread();

            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        // add users to exsisting call
        private void addUsers_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                hiddenForm = true;
                createCall _f = new createCall(client, userName, topic, password, code.Text, this, 1);// option 1 = add users
                this.Hide();
                _f.Show();
            });
        }
        //logOut action
        private void logOutButton_Click(object sender, EventArgs e)
        {
            if (isInCall)
            {
                if (topic != "General")
                {
                    client.addTosend(new leaveCall(userName, topic));
                    client.closeCallThread();
                    topic = "General";
                }
                code.clearUsers();
                endCall.Enabled = false;
                addUsers.Enabled = false;
                callButton.Enabled = true;
                isInCall = false;
                code.isIncall = false;
                code.showingLiveTab = false;
                code.Text = "";
            }
            client.addTosend(new logOutMsg(userName));
            hiddenForm = true;

            InvokeIfNeeded(delegate ()
            {
                tabs.Clear();
                tabsData.Clear();
                closeButtons.Clear();
                addTab("New Tab", "");
                signIn.setTextBoxToDefault();
                this.Hide();
                signIn.Show();
            });

        }

        //if your cursor above other user cursor this function display his username
        private void code_MouseMove(object sender, MouseEventArgs e)
        {
            if(code.showingLiveTab)
                code.checkIfMouseOnUser();
        }

        //send to server change cursor position
        private void code_Click(object sender, EventArgs e)
        {
            if(code.isIncall && code.SelectionStart != lastCursorPos && topic != "General" && code.showingLiveTab)
            {
                lastCursorPos = code.SelectionStart;
                client.addTosend(new userCursorPlaceClient(topic, userName, code.SelectionStart));

            }
        }
    }
}
