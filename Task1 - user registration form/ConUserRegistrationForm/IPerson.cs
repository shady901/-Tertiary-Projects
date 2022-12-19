using System;
using System.Collections.Generic;
using System.Text;

namespace ConUserRegistrationForm
{
    interface IPerson
    {
        string Fname { get; set; }
        string Lname { get; set; }
        string Email { get; set; }
    }
}
