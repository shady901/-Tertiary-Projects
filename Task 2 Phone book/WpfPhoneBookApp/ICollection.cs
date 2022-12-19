using System;
using System.Collections.Generic;
using System.Text;

namespace WpfPhoneBookApp
{
    interface ICollection
    {
        int dictionaryCount { get; set; }

        Dictionary<int, Users> getList();

        void setUser(Users user);

    }
}
