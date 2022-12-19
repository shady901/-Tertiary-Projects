using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConAssesmentUserNamePassword
{
    class Program
    {
        //this is a program that used to authenticate a password with 8 char's or more
        //coded by Alex
        //Ver 1.0

        static void Main(string[] args)
        {

            Console.WriteLine("This is Premier Transport Limited's login system\n");

            Console.Write("Please enter a username that is at least 8 characters long: ");
            string usernameInput = Console.ReadLine();// this asks the user to for string and assigns it to a string for the username
            UsernameCheck(usernameInput);// this links the usernameinput string varible to the method username check

            Console.Write("Please enter a password that is at least 8 characters long: ");
            string userPasswordInput = Console.ReadLine();// this asks the user to for string and assigns it to a string for the password
            Console.WriteLine("Please re-enter the password again: ");
            string userPasswordInput2 = Console.ReadLine();// this asks the user to for string and assigns it to a string for the password

            PasswordCheck(userPasswordInput,userPasswordInput2);//this links the userpasswordinput string varible to the method password check

            Console.WriteLine("Username and password have been accepted and set");
            Console.ReadKey();// this waits for a key press to close the console
        }

        static void UsernameCheck(string username)// this method will be used to check the username that is sent from the main 
        {

          
           while (username.Length < 8)// this checks if the username is less than 8 charachters then it will send text to the screen 
            {
                Console.WriteLine("Your Username you entered is Not long enough try again: ");
                username = Console.ReadLine();// this asks the user for a username and assigns it back into the username to be replaced
            }
             Console.WriteLine("{0} is within the limits of 8 or more characters long", username);
        }

        static void PasswordCheck(string password, string password2)// this method gets sent the string varibles called password and password2 and will check it 
        {
            do
            {
               if ((password != password2))// this if statment checks the password if they dont match and will ask the user again 
                {
                    Console.Write("Your Passwords is NOT the same try again: ");
                    password = Console.ReadLine();//
                    Console.Write("Please re-enter password: ");
                    password2 = Console.ReadLine();//
                }


                else if ((password.Length < 8) && (password2.Length < 8))//this if statment checks the password if they are not 8 characters long
                {
                    Console.Write("Your Password You Entered is not long enough try again: ");
                    password = Console.ReadLine();//
                    Console.Write("Please re-enter password: ");
                    password2 = Console.ReadLine();//
                }

            } while ((password.Length < 8) && (password2.Length < 8)|| (password != password2));// this loop repeats till the passwords are the same or are both 8 or more characters long 

            Console.WriteLine("Your Password is within the limits of 8 or more characters long");           
            
        }
    }
}
