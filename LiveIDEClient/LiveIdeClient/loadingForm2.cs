using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormAnimation;

namespace liveIde
{
    public partial class loadingForm2 : Form
    {
     /*
     this form is loading Form
     */
        private ClientClass Client;
        private string userName;
        private string password;
        private string topic;
        private string data;
        private MainForm f1;
        private signInWindow signIn;
        private bool hiddenForm;

        public loadingForm2( string _data,ClientClass _client, string _username, string _topic, string _password, MainForm _f1, signInWindow _signIn = null)
        {
            InitializeComponent();
            hiddenForm = false;
            f1 = _f1;
            data = _data;
            Client = _client;
            userName = _username;
            password = _password;
            topic = _topic;
            Client.Message += client_Message;
            this.BackColor = Color.White;
            signIn = _signIn;
            this.FormClosed += loadingForm_FormClosed; // occurs after the closing of the form
        }

        
        public void setHiddenForm(bool hidden)
        {
            hiddenForm = hidden;
        }

        public void updateFormData(string _data, string _username, string _topic, string _password)//, signInWindow _signIn = null
        {
            hiddenForm = false;
            data = _data;
            userName = _username;
            password = _password;
            topic = _topic;
        }

        public void ChangeToshowForm()
        {
            hiddenForm = false;
        }
        private void loadingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // to write all lines after fixing management windows
            //Client.addTosend(new logOutMsg(userName));
          //  System.Threading.Thread.Sleep(500);
          //  Client.closeThreads();
          //  Environment.Exit(Environment.ExitCode);
        }

        //Handling received messages 
        private void client_Message(object sender, dynamic e)
        {
            bool connect;
            if (e.Type != "108")
            {
                switch (e.Type.ToString())
                {
                    case "100":
                        connect = e.Confirm;
                        if (connect && !hiddenForm)
                        {
                            InvokeIfNeeded(delegate ()
                            {
                                hiddenForm = true;
                                f1.ChangeToshowForm();
                                this.Hide();
                                f1.Show();
                            });
                        }
                        else if(!hiddenForm)
                        {
                            hiddenForm = true;
                            
                                this.Hide();
                                if (signIn != null)
                                {
                                    InvokeIfNeeded(delegate ()
                                    {
                                        signIn.setTextBoxToDefault();
                                        signIn.setErrorLabel(e.Error);
                                        signIn.Show();
                                    });
                                }
                                else
                                {
                                    InvokeIfNeeded(delegate ()
                                    {
                                        signInWindow _f = new signInWindow(Client, f1, e.Error);
                                        _f.Show();
                                    });
                                }
                               
                            
                        }
                        break;

                    case "103":
                        if(e.Confirm)
                        {
                            InvokeIfNeeded(delegate ()
                            {
                                hiddenForm = true;
                                this.Hide();
                                if(signIn != null)
                                {
                                    //signIn.setTextBoxToDefault();
                                    signIn.setErrorLabel(e.Error);
                                    signIn.Show();
                                }
                            });
                            
                        }
                        else
                        {
                            // if there is problem with registresion
                            InvokeIfNeeded(delegate ()
                            {
                                hiddenForm = true;
                                this.Hide();
                                register r = new register(Client, signIn,f1, e.Error);// the user register to the system
                                r.Show();
                            });
                        }
                        break;
                    case "104":
                        if (e.checkNames)// the usernamesare valid
                        {
                            hiddenForm = true;
                            InvokeIfNeeded(delegate ()
                            {
                                f1.ChangeToshowForm();
                                signIn.setUsername(userName);
                                this.Hide();
                                f1.Show();
                            });

                        }
                        else
                        {
                            // opening craete call again frinds and write error msg
                        }
                        break;
                    case "109":
                        bool checkUserCall = false;
                        foreach (string s in (List<string>)e.usersToAdd)
                        {
                            if (s == userName)
                            {
                                checkUserCall = true;
                            }
                        }

                        if (checkUserCall && !hiddenForm)
                        {

                            if (MessageBox.Show("Do you want to connect to a conversation created by " + e.admin, "New call", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Client.addTosend(new GetInitText(e.callId, userName, password));
                                InvokeIfNeeded(delegate ()
                                {
                                    f1.setStartCall();
                                    hiddenForm = true;
                                    f1.ChangeToshowForm();
                                    this.Hide();
                                    f1.Show();
                                });
                            }
                            else
                            {
                                Client.addTosend(new leaveCall(userName, topic));// the user already in the call users list
                            }
                        }

                        break;

                }
            }
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


        private void proccessBarTimer_Tick(object sender, EventArgs e)
        {
            
        }
    }
}