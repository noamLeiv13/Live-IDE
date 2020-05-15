using System;
using System.Collections.Generic;
using System.Text;

namespace library
{
    /*
    the data we save about every user who register to our program
    */
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
