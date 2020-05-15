namespace library
{
    [Serializable]
    /*
    The server send this message after the user tries to register
    */
    public class registerResponse : serverMsg
    {
        public bool Confirm { get; set; }
        public string Error { get; set; }
        public registerResponse(string topic, bool confirm, string error) : base("103", topic)
        {
            this.Confirm = confirm;
            this.Error = error;
        }
    }
}
