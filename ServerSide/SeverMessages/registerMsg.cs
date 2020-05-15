
namespace library
{
    [Serializable]
    /*
    The client send this message when the user wants to register
    */
    public class registerMsg: ClientMSG
    {
        public string Password { get; set; }
        public registerMsg(string username, string password) : base("203", username)
        {
            this.Password = password;
        }
    }
}
