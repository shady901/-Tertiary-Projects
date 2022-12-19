using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConClassesPayRoll
{
    class EmployeeClass
    {
        //these varibles are private and can only be acessed by the employee class
        private string employeeName;
        private double grossSalary;
        private double taxRate;
        private double taxAmount;
        private double answer;

        public EmployeeClass()// this is a default constructor using default parameters displaying a text when run
        {
            Console.WriteLine("Employee record created");
            
        }

        public void SetEmployeeName(string Name)//this is a set method that sets a varible into the class private varibles that was received from the main program
        {
            employeeName = Name;////this sets the varible that was sent from the main program into employee name
            Console.WriteLine("Employee name has been set\n");
        }

        public void SetGrossSalary(double GrossSal)//this is a set method that sets a varible into the class private varibles that was received from the main program
        {
            grossSalary = GrossSal;//this sets the varible that was sent from the main program into gross salary
            Console.WriteLine("GrossSalary name has been set\n");
        }
        public void SetTaxRate(double TaxR)//this is a set method that sets a varible into the class private varibles that was received from the main program
        {
            taxRate = TaxR;// this sets the varible that was sent from the main program into taxrate
            Console.WriteLine("TaxRate name has been set\n");
        }
        public string GetEmployeeName()//this is just a get method which helps pinpoint what varible the code is asking for
        {
            return employeeName;//this the varible back to the main program when it is needed
        }
        public double GetGrossSalary()//this is just a get method which helps pinpoint what varible the code is asking for
        {
            return grossSalary;//this the varible back to the main program when it is needed
        }
        public double GetTaxrate()//this is just a get method which helps pinpoint what varible the code is asking for
        {
            return taxRate;//this the varible back to the main program when it is needed
        }

        public double Calcnet()//this method will calculate the tax amount and reduct it from the total giving answer
        {

            taxAmount = grossSalary * taxRate;// this line calculates the tax amount of the gross salary and assigns it into a double varible 
            
            answer = grossSalary - taxAmount;// this line minus's the tax amount from the gross salary giving the total which is assigned into answer

            


            return answer;//this returns the answer back to the main program when it is needed
        }
    }
}
