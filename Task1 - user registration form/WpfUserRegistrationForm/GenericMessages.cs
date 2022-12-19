using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUserRegistrationForm
{
    class GenericMessages
    {
        public static string PleaseEnter(string varible)
        {

            return $"Please enter the {varible}: ";
        }


        public static string Menu()
        {

            return "Welcome to The User Registration Form Menu\n(1)Create a new User\n(2)Create a new Administrator\n(3)List all current users\n(4)Exit Application";
        }
        public static string Bar()
        {

            return "********************************************************************************";
        }
        public static string PressAnyK()
        {


            return "Press any key to continue";
        }
        public static string userReg()
        {


            return "User Registered";
        }
    }
}
