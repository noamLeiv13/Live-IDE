using ClassLibrary;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZeroMQ;

namespace liveIde
{
    

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ClientClass client = new ClientClass();
            MainForm f1 = new MainForm("", client,"","","", null);
            signInWindow f = new signInWindow(client,f1, "");
            Forms.loadingForm = new loadingForm2("", client, "", "", "", f1,f);
            Forms.loadingForm.setHiddenForm(true);
            f1.setSignInWindow(f);
            Application.Run(f);
        }

    }
}
       