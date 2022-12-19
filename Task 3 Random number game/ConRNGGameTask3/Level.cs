using System;
using System.Collections.Generic;
using System.Text;

namespace ConRNGGameTask3
{
    class Level : ILevel
    {
        public int Easy { get ; set ; }
        public int Medium { get; set; }
        public int Hard { get; set; }
        static Random r =new Random();
        public int Target(int diff)
        {
            int randomN = r.Next(1, diff + 1);
            return randomN;

        }

        public Level()
        {
            Easy = 5;
            Medium = 10;
            Hard = 20;
        }


        public bool CheckGuess(int target, int guess)
        {
            if (target ==guess)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
