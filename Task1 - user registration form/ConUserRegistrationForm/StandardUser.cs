using System;
using System.Collections.Generic;
using System.Text;

namespace ConUserRegistrationForm
{
    class StandardUser : UserAccount
    {

        
        public StandardUser()
        {
            Role = new string(new char[] { 'S', 't', 'a', 'n', 'd', 'a', 'r', 'd' });

        }


    }
}
