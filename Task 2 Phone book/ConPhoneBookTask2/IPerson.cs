using System;
using System.Collections.Generic;
using System.Text;

namespace ConPhoneBookTask2
{
    interface IPerson
    {
        string FName { set; get; }

        string LName { set; get; }
        string PNumber { set; get; }
        string Address { set; get; }
        string Email { set; get; }
       

    }
}
