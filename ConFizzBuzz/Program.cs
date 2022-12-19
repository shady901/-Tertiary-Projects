using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConFizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            string Fizz = "Fizz";
            string Buzz = "Buzz";
            int Threes, Fives, TFBOTH;

            for (int i = 0; i <= 100; i++)
            {
                Threes = i % 3;
                Fives = i % 5;
                if ((Threes == 0)&&(Fives!=0)) Console.WriteLine("{0}",Fizz);
                else if ((Fives == 0)&&(Threes!=0)) Console.WriteLine("{0}",Buzz);
                else if ((Fives == 0) && (Threes == 0)) Console.WriteLine("{0}{1}",Fizz,Buzz);
                else Console.WriteLine("{0}", i);
                
                
            }






            Console.ReadKey();
        }
    }
}
