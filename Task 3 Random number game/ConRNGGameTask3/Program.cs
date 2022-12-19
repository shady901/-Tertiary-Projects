using System;
using System.Collections.Generic;

namespace ConRNGGameTask3
{
    class Program
    {
        static ScoreCollection CurrentScore = new ScoreCollection();
       static Level level = new Level();
        static void Main(string[] args)
        {

            Menu();




        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine(Generic_Messages.Bar());
            Console.WriteLine(Generic_Messages.Menu());
            Console.WriteLine(Generic_Messages.Bar());

            string userInput;
            do
            {

                userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    StartGame();
                }
                else if (userInput == "2")
                {
                    DisplayScoreBoard(CurrentScore.GetScoreData());
                }
                else if (userInput == "3")
                {
                    Environment.Exit(0);
                }
              
                else
                {
                    Console.WriteLine("Please enter a possible option");

                }
            } while (true);

        }

        private static void DisplayScoreBoard(Dictionary<int, ScoreData> collection)
        {
                           
                Console.Clear();
            foreach (var item in collection)
            {
                Console.WriteLine(Generic_Messages.Bar());
                Console.WriteLine("Game Number : "+ item.Key);
                Console.WriteLine("Points:" + item.Value.Points);
                Console.WriteLine("Difficulty:" + item.Value.Difficulty);
                Console.WriteLine("Guesses:" + item.Value.Guesses);
                Console.WriteLine("Name:" + item.Value.Name);                
                Console.WriteLine(Generic_Messages.Bar());
            }
                Console.WriteLine(Generic_Messages.PressAnyK());
                Console.ReadKey();
                Menu();
            
        }

        private static void StartGame()
        {
            
            Console.Clear();
            Console.WriteLine(Generic_Messages.Bar());
            Console.WriteLine(Generic_Messages.DiffMenu());
            Console.WriteLine(Generic_Messages.Bar());
            string userInput;
            do
            {

                userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    StartRound(level.Easy);
                }
                else if (userInput == "2")
                {
                    StartRound(level.Medium);
                }
                else if (userInput == "3")
                {
                    StartRound(level.Hard);
                }
                else if (userInput == "4")
                {
                    Menu();
                }
                else
                {
                    Console.WriteLine("Please enter a possible option");

                }
            } while (true);

          


        }
        static void setScoreData(int diff, int guesses)
        {
            ScoreData score = new ScoreData();
            if (diff == level.Easy)
            {
                score.Difficulty = "Easy";
            }
            else if (diff == level.Medium)
            {
                score.Difficulty = "Medium";
            }
            else if (diff == level.Hard)
            {
                score.Difficulty = "Hard";
            }

            score.Points = ScoreData.Pointcheck(guesses);
            score.Guesses = guesses;
            Console.WriteLine(Generic_Messages.Bar());
            Console.Write("Do you want to Enter a UserName Y/N: ");
           string answer =  Console.ReadLine().ToLower();
            if (answer =="y")
            {
              
                string name;
                do
                {
                    Console.WriteLine("Please Enter a user Name of 5 characters");
                    name = Console.ReadLine();
                    
                } while (name.Length > 5);
                score.Name = name;
            }
            else
            {
                score.Name = "?????";
            }

            CurrentScore.setScoreData(score);
            Menu();

        }
        static void StartRound(int Diff)
        {
            int[] Guesses = new int[3];
            int targetNumber = level.Target(Diff);
            for (int i = 0; i < 3; i++)
            {

                Console.Clear();
                Console.WriteLine(Generic_Messages.Bar());
                Console.WriteLine(Generic_Messages.RoundDisplayMsg(Diff));
                Console.WriteLine(Generic_Messages.DisplayPreviousGuesses(Guesses));
                Console.WriteLine(Generic_Messages.Bar());
                Console.Write("Please Guess a number: ");
                Guesses[i]=Convert.ToInt32(Console.ReadLine());
                if (Validate.Inbounds(Diff, Guesses[i]))
                {
                    if (level.CheckGuess(targetNumber, Guesses[i]))
                    {
                        Console.WriteLine(Generic_Messages.Bar());
                        Console.WriteLine($"You Guessed the correct Number and you are Awarded {ScoreData.Pointcheck(i+1)} Points for beating it in {i+1} Guesses");

                        setScoreData(Diff,i+1);

                        
                    }

                }
                else
                {
                    Console.WriteLine("number entered was outside of guess range");
                    Guesses[i] = 0;
                    i--;
                }
                
            }
            Console.WriteLine("You Lose");
            Console.ReadKey();
            StartGame();
            


        }
    }
}
