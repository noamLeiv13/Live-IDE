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
    In this form you can create new call or add users to existing call
    */
    public partial class createCall : Form
    {
        private int option;
        private string password;
        private string userName;
        private string topic;
        private string text;
        private MainForm f1;
        private ClientClass client;
        private bool hiddenForm;
        public createCall(ClientClass _client, string _username, string _topic, string _password, string Text, MainForm _f1, int _option = 0)
        {
            InitializeComponent();
            hiddenForm = false;
            option = _option;
            text = Text;
            password = _password;
            userName = _username;
            topic = _topic;
            this.client = _client;
            client.Message += client_Message;
            f1 = _f1;
            errorLabel.Text = "";

        }

        public void ChangeToshowForm()
        {
            hiddenForm = false;
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

        //Handling received messages 
        private void client_Message(object sender, dynamic e)
        {
            if (e.Type != "108" )
            {
                switch (e.Type.ToString())
                {
                    //ServerCallResponse 
                    case "104":
                        if (e.checkNames)// the usernames are valid
                        {
                            InvokeIfNeeded(delegate () {
                                hiddenForm = true;
                                f1.ChangeToshowForm();
                                this.Hide();
                                f1.Show();
                            });
                        }
                        else
                        {
                            InvokeIfNeeded(delegate () {
                                usersList.Items.Clear();
                                errorLabel.Text = "You can not add one or more users to the conversation. Please try again";
                            });
                        }
                        break;
                    case "109":
                        //>pubSubCallInvitation
                        bool checkUserCall = false;
                        //check if you invited to the call
                        foreach (string s in (List<string>)e.usersToAdd)
                        {
                            if (s == userName)
                            {
                                checkUserCall = true;
                            }
                        }
                        if (checkUserCall && !hiddenForm)// if you invited
                        {
                            if (MessageBox.Show("Do you want to connect to a conversation created by " + e.admin, "New call", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    f1.setStartCall();
                                    f1.ChangeToshowForm();
                                    hiddenForm = true;
                                    this.Hide();
                                    f1.Show();
                                    client.addTosend(new GetInitText(e.callId, userName, password));
                                });
                            }
                            else
                            {
                                client.addTosend(new leaveCall(userName, topic));// the user already in the call users list
                            }
                        }
                        break;
                }
            }
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }
        // handele key events
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // add user to call user list
            if (keyData == Keys.Enter)
            {
                if(usersTextbox.Text != "")
                {
                    usersList.Items.Add(usersTextbox.Text);
                    usersTextbox.Text = "";
                    errorLabel.Text = "";
                }
                else
                {
                    errorLabel.Text = "You can't add empty username to call";
                }
            }
            // remove user from call user list
            if (keyData == Keys.Delete || keyData == Keys.Back)
            {
               if (usersList.SelectedItem != null)
                {
                    var items = usersList.Items;
                    string item = (string)usersList.SelectedItem;
                    usersList.Items.Remove(item);
                }
            }
                return base.ProcessCmdKey(ref msg, keyData);
        }

        // check if name is in list<string>
        private bool checkNameInList(string name, List<string> names)
        {
            foreach(String user in names)
            {
                if (name == user)
                    return true;
            }
            return false;
        }
        // logout and close the program
        private void createCall_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.addTosend(new logOutMsg(userName));
            System.Threading.Thread.Sleep(500);
            client.closeThreads();
            Environment.Exit(Environment.ExitCode);

        }

        //don't create call
        private void BackButton_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                hiddenForm = true;
                f1.ChangeToshowForm();
                f1.cancelCallUpdateButtons();
                this.Hide();
                f1.Show();// show the previous window
            });
        }

        private void createCallbutton_Click(object sender, EventArgs e)
        {
            List<string> usersToAdd;
            //check if the user try to add himself to call
            if (checkNameInList(userName, usersList.Items.Cast<string>().ToList<string>()))
            {
                errorLabel.Text = "You can't add yourself to call";
                usersList.Items.Clear();
            }
            else if (usersList.Items.Count == 0)
            {
                errorLabel.Text = "You need to add at least one user to the conversation";
                usersList.Items.Clear();
            }
            else
            {
                usersToAdd = usersList.Items.Cast<string>().ToList<string>();
                if (option == 0)
                {
                    
                   Invoke((MethodInvoker)delegate
                   {

                       hiddenForm = true;
                       this.Hide();
                       f1.ChangeToshowForm();
                       this.Hide();
                       client.addTosend(new createCallMsg(topic, userName, text, usersToAdd));
                       f1.Show();
                       //loadingForm2 _f = new loadingForm2(text, client, userName, topic, password, f1);//General - default topic
                       //_f.Show();                      
                   });
                 
                }
                else if (option == 1)//add users to exsisting call
                {
                    client.addTosend(new addUsersToCall(topic, userName, usersToAdd));
                    Invoke((MethodInvoker)delegate
                    {
                        f1.ChangeToshowForm();
                        hiddenForm = true;
                        this.Hide();
                        f1.Show();
                    });
                }
            }


        }
    }
}
