using System;
using System.Collections.Generic;

namespace ConUserRegistrationForm
{
    class Program
    {
       static UserCollections collection = new UserCollections();
        static void Main(string[] args)
        {
            
            Menu();
            Console.ReadLine();
        }
       static void CreateNewPerson()
        {
            string p1, p2;
            Console.Clear();
            StandardUser person = new StandardUser();
            Console.Write(GenericMessages.PleaseEnter("first Name"));
            person.Fname = Console.ReadLine();
            Console.Write(GenericMessages.PleaseEnter("last Name"));
            person.Lname = Console.ReadLine();
            Console.Write(GenericMessages.PleaseEnter("Email"));
            person.Email = Console.ReadLine();
            person.Username = person.GenerateUsername(person.Fname, person.Lname);

            do
            {
                Console.Write(GenericMessages.PleaseEnter("Password"));
                p1 = Console.ReadLine();
                Console.Write(GenericMessages.PleaseEnter("Password again"));
                p2 = Console.ReadLine();
                if (person.CheckPassword(p1, p2))
                {
                    person.Password = p1;
                }
                else
                {
                    Console.WriteLine("Password is not the same try again:");
                }
            } while (!person.CheckPassword(p1, p2));
            string[] fields = new string[] { person.Fname, person.Lname, person.Password, person.Email };
            Console.WriteLine(person.CheckallProperties(fields));
            collection.AddSUser(person);
            Console.WriteLine(GenericMessages.PressAnyK());
            Console.ReadKey();
            Menu();

        }
        static void CreateNewAdmin()
        {
            AdminUser person = new AdminUser();
            string p1, p2;
            Console.Clear();
           
            Console.Write(GenericMessages.PleaseEnter("first Name"));
            person.Fname = Console.ReadLine();
            Console.Write(GenericMessages.PleaseEnter("last Name"));
            person.Lname = Console.ReadLine();
            Console.Write(GenericMessages.PleaseEnter("Email"));
            person.Email = Console.ReadLine();
            person.Username = person.GenerateUsername(person.Fname, person.Lname);
            do
            {
                Console.Write(GenericMessages.PleaseEnter("Password"));
                 p1 = Console.ReadLine();
                Console.Write(GenericMessages.PleaseEnter("Password again"));
                p2 = Console.ReadLine();
                if (person.CheckPassword(p1, p2))
                {
                    person.Password = p1;
                }
                else
                {
                    Console.WriteLine("Password is not the same try again:");
                }
            } while (!person.CheckPassword(p1,p2));
            
            string[] fields = new string[] { person.Fname, person.Lname, person.Password, person.Email };
            Console.WriteLine(person.CheckallProperties(fields));            
            collection.AddAUser(person);
            Console.WriteLine(GenericMessages.PressAnyK());
            Console.ReadKey();
            Menu();


        }
        static void DisplayCollection(Dictionary<int,UserAccount> userCollect)
        {
            int count =1;
            Console.Clear();
            foreach (var user in userCollect)
            {
                Console.WriteLine(GenericMessages.Bar());
                Console.WriteLine("User : "+count);
                Console.WriteLine("first Name:"+user.Value.Fname);
                Console.WriteLine("Last Name:"+user.Value.Lname);
                Console.WriteLine("UserName:"+user.Value.Username);
                Console.WriteLine("Email:"+user.Value.Email);
                Console.WriteLine("Role:"+user.Value.Role);
                Console.WriteLine(GenericMessages.Bar());

                count++;
                               
            }
            Console.WriteLine(GenericMessages.PressAnyK());
            Console.ReadKey();
            Menu();
        }
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine(GenericMessages.Bar());
            Console.WriteLine(GenericMessages.Menu());
            Console.WriteLine(GenericMessages.Bar());

            string userInput;
            do
            {
                
                userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    CreateNewPerson();
                }
                else if (userInput == "2")
                {
                    CreateNewAdmin();
                }
                else if (userInput == "3")
                {
                    DisplayCollection(collection.ReturnCollection());
                }
                else if (userInput == "4")
                {
                    Console.WriteLine("Press any key to close the application");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine(GenericMessages.PleaseEnter("A correct Input"));
                   
                }
            } while (true);

        }
       
        
    }
}
