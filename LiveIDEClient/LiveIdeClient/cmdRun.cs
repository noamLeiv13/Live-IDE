using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace liveIde
{
    /*
    This form diplay the error after the user try to run his code.
    */
    public partial class cmdRun : Form
    {
        private ClientClass client;
        private Process cmd = new Process();
        private MainForm f1;
        private string username;
        
        // try to run the code if there is errors display them in label
        public cmdRun(string codeText, ClientClass _client, MainForm _f1, string _username)
        {
            InitializeComponent();
            username = _username;
            f1 = _f1;
            client = _client;

            output.Text = "";
            System.IO.StreamWriter sw = new System.IO.StreamWriter("csharpcode.cs");
            sw.Write(codeText);
            sw.Close();

            try
            {
                cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.WriteLine("set path=%path%;v3.5");// use default constrector of windows
                cmd.StandardInput.WriteLine("csc csharpcode.cs");// use default constrector of windows
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
            }
            catch (Exception ex)
            {
                Instructions.Text = ex.Message;
            }
            string temp = cmd.StandardOutput.ReadToEnd().ToString();
            int errors = Regex.Matches(temp, "error").Count;
            if (errors == 0)
            {
                Process process = Process.Start("csharpcode.exe");
                int id = process.Id;
                Process tempProc = Process.GetProcessById(id);
                tempProc.WaitForExit();

            }
            else
            {
                temp = temp.Substring(errors + 5);//5 - length of error
                output.Text = temp;
            }
        }

        private void parameters_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        // return to the code form
        private void BackButton_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                this.Hide();
                f1.ChangeToshowForm();
                f1.Show();
            });
        }

        //when the form close
        private void cmdRun_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.addTosend(new logOutMsg(username));
            if (client.getCallThread() != null)
                client.closeCallThread();
            System.Threading.Thread.Sleep(500);
            client.closeThreads();
            Environment.Exit(Environment.ExitCode);
        }
    }

}
