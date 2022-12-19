using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUserRegistrationForm
{
     class UserCollections
    {
        Dictionary<int, UserAccount> userCollection = new Dictionary<int, UserAccount>();
        public static int key = 0;
        public  void AddSUser(StandardUser user)
        {
            userCollection.Add(key, user);
          //  Console.WriteLine(user.Role);
            key++;
        }
        public void AddAUser(AdminUser user)
        {
            userCollection.Add(key, user);
           // Console.WriteLine(user.Role);
            key++;
        }


        public  Dictionary<int, UserAccount> ReturnCollection()
        {
            return userCollection;
        }
    }
}
