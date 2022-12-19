using System;
using System.Collections.Generic;

namespace ConPhoneBookTask2
{
    class Program
    {
        static Collection Phonebook = new Collection();
        
        static void Main(string[] args)
        {
            setPhoneBook();
            string answer = null;
            do
            {
                Console.Clear();
                DisplayMenu();

                answer = Console.ReadLine();
                if (answer == "2")
                {
                    Supervisor user = new Supervisor();
                    DisplayPhoneBook(Phonebook.getList(), user.Role);
                }
                else if (answer == "1")
                {
                    Guest user = new Guest();
                    DisplayPhoneBook(Phonebook.getList(), user.Role);
                }
                else if (answer == "3")
                {
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice");
                }
            } while (true);
           
        }



        static void setPhoneBook()
        {
            //hard coded 5 users
            Users u1 = new Users();
            u1.FName = "Leanne" ;
            u1.LName = "Graham";
            u1.Email = "Sincere@april.biz";
            u1.Address = "Kulas Light, Gwenborough";
            u1.PNumber = "1-770-736-8031";
           
            Users u2 = new Users();
            u2.FName = "Ervin";
            u2.LName = "Howell";
            u2.Email = "Shanna@melissa.tv";
            u2.Address = "Victor Plains, Wisokyburgh";
            u2.PNumber = "010-692-6593";

            Users u3 = new Users();
            u3.FName = "Clementine";
            u3.LName = "Bauch";
            u3.Email = "Nathan@yesenia.net";
            u3.Address = "Douglas Extension, McKenziehaven";
            u3.PNumber = "1-463-123-4447";

            Users u4 = new Users();
            u4.FName = "Patricia";
            u4.LName = "Lebsack";
            u4.Email = "Julianne.OConner@kory.org";
            u4.Address = "Hoeger Mall, South Elvis";
            u4.PNumber = "493-170-9623";

            Users u5 = new Users();
            u5.FName = "Chelsey";
            u5.LName = "Dietrich";
            u5.Email = "Lucio_Hettinger@annie.ca";
            u5.Address = "Skiles Walks, Roscoeview";
            u5.PNumber = "(254)954-1289";
            Phonebook.setUser(u1);
            Phonebook.setUser(u2);
            Phonebook.setUser(u3);
            Phonebook.setUser(u4);
            Phonebook.setUser(u5);
        }
        static void DisplayPhoneBook(Dictionary<int, Users> collection, object role)
        {
            Console.Clear();
            if (role.ToString() == "Supervisor")
            {
                Console.WriteLine("User Index  :  First Name  :  Last Name  :     Phone      :     Email               :       Address     ");
                foreach (var user in collection)
                {
                    Console.WriteLine("       " + user.Key + "      " + user.Value.FName + "         " + user.Value.LName + "        " + user.Value.PNumber + "      " + user.Value.Email + "        " + user.Value.Address);
                }
            }
            else
            {
                Console.WriteLine("User Index  :  First Name  :  Last Name  :   Phone      :     Email");
                foreach (var user in collection)
                {
                    Console.WriteLine("       " + user.Key + "      " + user.Value.FName + "         " + user.Value.LName + "      " + user.Value.PNumber + "      " + user.Value.Email);
                }

            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        static void DisplayMenu()
        {
            Console.WriteLine("Welcome to The Phone book Application Please chose an option below\n(1)Access phone book as guest user\n(2)Access phone book as supervisor\n(3)Exit Application");
        }
    }
}
