using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRNGGameTask3
{
    interface ILevel
    {
        int Easy { get;set; }
        int Medium { get; set; }
        int Hard { get;set; }

        int Target(int diff);

    }
}
