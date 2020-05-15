using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;


namespace liveIde
{
    public partial class loadingWindow : Form
    {
        private ClientClass Client;
        private string userName;
        private string password;
        private string topic;
        private MainForm f1;
        public loadingWindow(ClientClass _client, string _username, string _topic, string _password, MainForm _f1 = null)
        {
            f1 = _f1;
            Client = _client;
            userName = _username;
            password = _password;
            topic = _topic;
            Client.Message += client_Message;
            InitializeComponent();

        }

        private void client_Message(object sender, dynamic e)
        {
            if (e.Type != "108")
            {
                switch (e.Type.ToString())
                {
                    //sign in Response 
                    case "100":
                        bool connect = e.Confirm;
                        if (connect)
                        {
                            //display code form
                            if (f1 != null)
                            {
                                InvokeIfNeeded(delegate ()
                                {
                                    this.Hide();
                                    f1.Show();
                                });
                            }
                            else
                            {
                                InvokeIfNeeded(delegate ()
                                {
                                    MainForm f = new MainForm("", Client, userName, topic, password, null);
                                    signInWindow signinForm = new signInWindow(Client, f, "");
                                    f.setSignInWindow(signinForm);
                                    f.Show();
                                });
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


    }
}
