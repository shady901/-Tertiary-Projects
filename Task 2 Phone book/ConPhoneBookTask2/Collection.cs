using System;
using System.Collections.Generic;
using System.Text;

namespace ConPhoneBookTask2
{
    class Collection : ICollection
    {
        Dictionary<int, Users> PhoneBook = new Dictionary<int, Users>();

        public int dictionaryCount { get; set; }

        public Dictionary<int,Users> getList()
        {
            return PhoneBook;
        }

        public void setUser(Users user)
        {
            PhoneBook.Add(dictionaryCount, user);


            dictionaryCount++;
        }
    }
}
