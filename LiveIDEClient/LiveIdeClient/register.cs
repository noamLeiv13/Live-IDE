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
    register form
    */
    public partial class register : Form
    {
        private ClientClass client;
        private signInWindow signIn;
        private MainForm f1;
        private string Error;
        public register(ClientClass _client, signInWindow _signIn, MainForm _f1, string error ="")
        {
            InitializeComponent();
            PasswordTextBox.isPassword = true;
            signIn = _signIn;
            client = _client;
            Error = error;
            f1 = _f1;
            if (Error != "")
                errorLabel.Text = Error;
            else
                errorLabel.Text = "";
            client.Message += client_Message;

        }

        //Handling received messages 
        private void client_Message(object sender, dynamic e)
        {
            if (e.Type != "108")
            {
                switch (e.Type.ToString())
                {
                    //register Response
                    case "103":
                        if (e.Confirm)
                        {
                            InvokeIfNeeded(delegate ()
                            {
                                this.Hide();
                                signIn.Show();
                            });
                        }
                        else
                        {
                            errorLabel.Text = e.Error;
                        }
                        break;
                }
            }
        }

        public void setTextBoxesToDefault()
        {
            userNameTextBox.Text = "";
            PasswordTextBox.Text = "";
        }
        // move the focus from the textboxes
        private void register_Load(object sender, EventArgs e)
        {
            this.ActiveControl = logoPicture;

        }
        //send the registration details to server
        private void signUpButton_Click(object sender, EventArgs e)
        {
            client.addTosend(new registerMsg(userNameTextBox.Text, PasswordTextBox.Text));
        }
        // return to signIn window
        private void BackButton_Click(object sender, EventArgs e)
        {
            InvokeIfNeeded(delegate ()
            {
                signIn.setTextBoxToDefault();
                this.Hide();
                signIn.Show();
            });

        }
        // set password option
        private void PasswordTextBox_OnValueChanged(object sender, EventArgs e)
        {
            PasswordTextBox.isPassword = true;
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
