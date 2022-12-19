using System;
using System.Collections.Generic;
using System.Text;

namespace ConUserRegistrationForm
{
    abstract class UserAccount : IUserAccount , IPerson,IUserRole
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public  string Fname { get; set; }
        public  string Lname { get; set; }
        public  string Email { get; set; }
        public  string Role { get; set; }

        public string CheckallProperties(string[] senders)
        {
            bool enteredProp = true;
            foreach (string item in senders)
            {
                if (item == null)
                {
                    enteredProp = false;
                    break;
                }
               
            }
            if (enteredProp == false)
            {
                return "Not All Fields have been entered";
            }
            else
            {
                return "All Fields Entered";
               
            }

        }
        public bool CheckPassword(string input1, string input2)
        {
            if (input1 == input2)
            {
                return true;
            }
            else
            {


                return false;
            }

        }
        public string GenerateUsername(string fname, string lname)
        {
            return (fname[0] + lname).ToLower();


        }
        
    }
}
