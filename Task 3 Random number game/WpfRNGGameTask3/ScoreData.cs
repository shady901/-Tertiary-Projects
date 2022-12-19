using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRNGGameTask3
{
    class ScoreData : IScoreData
    {
      public  int Guesses { get; set; }
        public int Points { get; set; }

        public string Difficulty { get; set; }

        public string Name { get; set; }


        public static int Pointcheck(int GuessAmount)
        {
            if (GuessAmount == 1)
            {
                return 10;
            }
            else if (GuessAmount ==2)
            {
                return 6;
            }
            else if (GuessAmount ==3)
            {
                return 2;
            }
            else
            {
                return 0;
            }
            



        }

    }
}
