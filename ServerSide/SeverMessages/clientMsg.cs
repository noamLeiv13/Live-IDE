using System;

namespace library
{

        [Serializable]
        public class ClientMSG
        {
            public string Type { get; set; }
            public string Username { get; set; }
            public ClientMSG() // empty ctor for JSON
        {
                this.Type = "";
                this.Username = "";
            }

            public ClientMSG(string type, string username)
            {
                this.Type = type;
                this.Username = username;
            }
        }
}
