using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRNGGameTask3
{
    class Generic_Messages
    {
        public static string Menu()
        {

            return "Welcome to The RNG Game\n(1)Start new round\n(2)Current ScoreBoard\n(3)Exit Application";
        }
        public static string Bar()
        {

            return "********************************************************************************";
        }
        public static string PressAnyK()
        {


            return "Press any key to continue";
        }
        public static string DiffMenu()
        {

            return "Please Chose a Difficulty\n(1)Easy\n(2)Medium\n(3)Hard\n(4)Back to Menu";
        }


        public static string RoundDisplayMsg(int diff)
        {

            return $"Attempt to guess the random number between 1-{diff}\n you have up to 3 Guesses Chose Wisely";
        }
        public static string DisplayPreviousGuesses(int[] Prev)
        {
            string myprev;
            if (Prev[0] >0)
            {
                myprev= $"Your Previous Guesses are: {Prev[0]}";
                if (Prev[1]>0)
                {
                    myprev += $",{Prev[1]}";
                    return myprev;
                }
                else
                {
                    return myprev;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
