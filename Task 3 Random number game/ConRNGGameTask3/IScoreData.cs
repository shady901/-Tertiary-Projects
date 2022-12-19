using System;
using System.Collections.Generic;
using System.Text;

namespace ConRNGGameTask3
{
    interface IScoreData
    {
        int Guesses { get; set; }
        int Points { get; set; }

        string Difficulty { get; set; }

        string Name { get; set; }

    }
}
