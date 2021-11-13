
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Integration
{
    public class Patient
    {
        public int Id { get; set; }
        private bool isGuest = false;
        private bool isBlocked = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalID { get; set; }


        public Patient() { }

        public bool IsGuest
        {
            get => isGuest;
            set
            {
                isGuest = value;
            }
        }

        public bool IsBlocked
        {
            get { return isBlocked; }
            set
            {
                isBlocked = value;
            }
        }

        override
        public string ToString()
        {
            return FirstName + " " + LastName + " " + PersonalID;
        }
    }
}