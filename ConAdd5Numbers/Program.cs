using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConAdd5Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int usernum1;
            int total = 0;
            Console.WriteLine("Please enter 5 items");
            for (int i = 1; i <= 5; i++)
            {
                Console.Write("item {0}: ",i);
                usernum1 = Convert.ToInt32(Console.ReadLine());
                total += usernum1;
            }

            Console.WriteLine("total Amount: ${0}",total);


            Console.ReadKey();
        }
    }
}
