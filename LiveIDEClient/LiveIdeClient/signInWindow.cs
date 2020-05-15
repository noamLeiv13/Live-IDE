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

namespace liveIde
{
    /*
    signin form
    */
    public partial class signInWindow : Form
    {
        private string usernameConnected;
        private ClientClass Client;
        private MainForm f1;
        private register r;
        public signInWindow(ClientClass client, MainForm _f1, string error)
        {
            InitializeComponent();
            usernameConnected = "";
            r = null;
            Client = client;
            f1 = _f1;
            if (error != "")// error msg after connection attempt
            {
                errorMsgLabel.Text = error;
            }
            else
            {
                errorMsgLabel.Visible = false;
            }
            password.isPassword = true;
            this.ActiveControl = personPIctureBox;// move the focus from the textboxes
            signInPanel.BringToFront();
        }

        public void setErrorLabel(string _error)
        {
            errorMsgLabel.Visible = true;
            errorMsgLabel.Text = _error;
        }

        public void setUsername(string _username)
        {
            usernameConnected = _username;
        }

        public void setTextBoxToDefault()
        {
            userName.Text = "";
            password.Text = "";
        }

        private void signInWindow_Load(object sender, EventArgs e)
        {
        }
        // login action
        private void loginButton_Click(object sender, EventArgs e)
        {

            InvokeIfNeeded(delegate ()
            {
                f1.setPassword(password.Text);
                f1.setUserName(userName.Text);
                f1.setTopic("General");
                loadingForm2 _f = new loadingForm2("", Client, userName.Text, "General", password.Text, f1, this);//General - default topic 
                this.Hide();
                _f.Show();
            });
            Client.addTosend(new signIn(userName.Text, password.Text));
        }

        //hide the password when the user is typing
        private void password_OnValueChanged(object sender, EventArgs e)
        {
            password.isPassword = true;
        }

        // close button click event - close all program
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (usernameConnected != "")
            {
                Client.addTosend(new logOutMsg(usernameConnected));
                if (Client.getCallThread() != null)
                    Client.closeCallThread();
            }
            System.Threading.Thread.Sleep(500);
            Client.closeThreads();
            Environment.Exit(Environment.ExitCode);
        }

        // move to register form
        private void SignUpLabel_Click(object sender, EventArgs e)
        {
            InvokeIfNeeded(delegate ()
            {
                if (r == null)
                    r = new register(Client, this, f1);
                this.Hide();
                r.setTextBoxesToDefault();
                r.Show();
            });

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
