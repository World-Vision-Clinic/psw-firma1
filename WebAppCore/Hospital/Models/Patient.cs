using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }
        public bool Activated { get; set; }


        public Patient() { }

    }
}
