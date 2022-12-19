using System;
using System.Collections.Generic;
using System.Text;

namespace ConUserRegistrationForm
{
    interface IUserAccount
    {

        string Username { get; }
        string Password { get; }

        bool CheckPassword(string input1, string input2);       

        string CheckallProperties(string[] senders);



    }



   
}
