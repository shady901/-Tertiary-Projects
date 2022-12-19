using System;
using System.Collections.Generic;
using System.Text;

namespace WpfPhoneBookApp
{
     class Users : IPerson
    {
        public  string FName { get; set; }
        public  string LName { get; set; }
        public  string PNumber { get; set; }
        public  string Address { get; set; }

        public string Email { get; set; }
     

    }
}
