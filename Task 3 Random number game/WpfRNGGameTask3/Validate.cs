using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRNGGameTask3
{
    class Validate
    {
        public static bool Inbounds(int diff, int guess)
        {
            if (guess>0&&guess<=diff)
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
