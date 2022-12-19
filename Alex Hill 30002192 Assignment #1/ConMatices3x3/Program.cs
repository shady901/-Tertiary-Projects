using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConMatices3x3
{
    class Program
    {
        // this program is to randomly generate two arrays and add them
        // this was coded by alex
        // ver 1.0
        static void Main(string[] args)
        {
            int[,] arrayA = new int[3, 3]; //declaring a 2 dimentional array of 3x3
            int[,] arrayB = new int[3, 3]; //declaring a 2 dimentional array of 3x3
            int[,] arrayC = new int[3, 3]; //declaring a 2 dimentional array of 3x3
            Random rnd = new Random(); // this creates a random value to rnd 

            Console.WriteLine("this is array A"); // this writes a title to the console
            for (int i = 0; i < 3; i++) //this is a for loop looping 3 times for the y axis of the arrays
            {
                for (int j = 0; j < 3; j++) // this is a nested loop which loops 3 times for the x axis of the arrays
                {
                    arrayA[i, j] = rnd.Next(-10, 11); // this allocates a random value to the ArrayA elements between -10 and 10
                    Console.Write(arrayA[i, j] + "\t"); // this writes the elements on to the console and then spaces the display
                }
                Console.WriteLine(); // this creates the new line 
            }

            Console.WriteLine();
            Console.WriteLine("this is array B");
            for (int i = 0; i < 3; i++) //this is a for loop looping 3 times for the y axis of the arrays
            {
                for (int j = 0; j < 3; j++) // this is a nested loop which loops 3 times for the x axis of the arrays
                {
                    arrayB[i, j] = rnd.Next(-10, 11); // this allocates a random value to the ArrayA elements between -10 and 10
                    Console.Write(arrayB[i, j] + "\t"); // this writes the elements on to the console and then spaces the display
                }
                Console.WriteLine();// this creates the new line 
            }




            Console.WriteLine();// this creates the new line 
            Console.WriteLine("this is array C");
            for (int i = 0; i < 3; i++) //this is a for loop looping 3 times for the y axis of the arrays
            {
                for (int j = 0; j < 3; j++) // this is a nested loop which loops 3 times for the x axis of the arrays
                {
                    arrayC[i, j] = arrayA[i, j] + arrayB[i, j]; // this adds the elements of the array a and array b and assigns them to array c
                    Console.Write(arrayC[i, j] + "\t"); // this writes the elements on to the console and then spaces the display
                }
                Console.WriteLine();// this creates the new line 
            }







            Console.ReadKey(); // this waits for the user to press a key before closing the console
        }
    }
}
