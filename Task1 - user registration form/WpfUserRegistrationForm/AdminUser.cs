﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUserRegistrationForm
{
    class AdminUser : UserAccount
    {
       

        public AdminUser()
        {
            Role = new string(new char[] { 'A', 'd', 'm', 'i', 'n' });

        }

    }
}
