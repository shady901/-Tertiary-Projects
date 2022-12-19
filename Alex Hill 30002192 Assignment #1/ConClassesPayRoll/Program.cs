using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConClassesPayRoll
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeClass EmployeeC = new EmployeeClass();// this line tells the main program that were using a class file by calling the constructor
            Console.WriteLine("**********************************************************");
            Console.WriteLine("Welcome to the Premier Transport Limited payroll system");
            Console.WriteLine("**********************************************************");


            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();//this line asks the user for a name and is put in a string varible
            EmployeeC.SetEmployeeName(name);// this assigns the name varible to the employee class in the setemployee method

            Console.Write("Please enter your Gross Salary: ");
            double grossSalary =Convert.ToDouble(Console.ReadLine());// this line asks the user a taxrate amount and is put in a double varible
            EmployeeC.SetGrossSalary(grossSalary);//this assigns the GrossSalary varible to the employee class in the setGrossSalary method


            Console.Write("Please enter your Tax Rate: ");
            double taxRate = Convert.ToDouble(Console.ReadLine());// this line asks the user a taxrate amount and is put in a double varible
            EmployeeC.SetTaxRate(taxRate);  //this assigns the taxrate varible to the employee class in the setTaxRate method

            Console.WriteLine("\n******************************************************");
            Console.WriteLine("Employee Name: {0}",EmployeeC.GetEmployeeName());
            Console.WriteLine("Gross Salary: {0}",EmployeeC.GetGrossSalary());
            Console.WriteLine("Tax Rate: {0}",EmployeeC.GetTaxrate());
            Console.WriteLine("******************************************************");
            Console.WriteLine("Employee Net Salary: {0}",EmployeeC.Calcnet());
           //  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
           // this displays all of the relevent data such as name, salary,taxrate and net salary

            Console.ReadKey();
        }
    }
}
