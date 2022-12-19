using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRNGGameTask3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ScoreCollection CurrentScore = new ScoreCollection();
        static Level level = new Level();
       static int[] Guesses = new int[3];
        static int difficulty ;
        int targetNumber;
       static int i = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Chck_easy_Checked(object sender, RoutedEventArgs e)
        {
            chck_medium.IsEnabled = false;
            chck_Hard.IsEnabled = false;
            btn_StartGame.IsEnabled = true;
        }
        private void Chck_medium_Checked(object sender, RoutedEventArgs e)
        {
            chck_easy.IsEnabled = false;
            chck_Hard.IsEnabled = false;
            btn_StartGame.IsEnabled = true;
        }
        private void Chck_Hard_Checked(object sender, RoutedEventArgs e)
        {
            chck_medium.IsEnabled = false;
            chck_easy.IsEnabled = false;
            btn_StartGame.IsEnabled = true;
        }
        private void Btn_StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (chck_easy.IsEnabled)
            {
                difficulty = level.Easy;
                targetNumber = level.Target(difficulty);
                Btn_guess.IsEnabled = true;
            }
            else if (chck_medium.IsEnabled)
            {
                difficulty = level.Medium;
                targetNumber = level.Target(difficulty);
                Btn_guess.IsEnabled = true;
            }
            else if (chck_Hard.IsEnabled)
            {
                difficulty = level.Hard;
                targetNumber = level.Target(difficulty);
                Btn_guess.IsEnabled = true;
            }
            lbl_lvlTitle.Content = Generic_Messages.RoundDisplayMsg(difficulty);
        }

         void StartRound(int difficulty)
        {
           
         
           

            if (i < 3)
            {
               
               
                    Guesses[i] = Convert.ToInt32(txtbox_Guess.Text);
                    txtbox_Guess.Clear();
                   
               
               
              
                if (Validate.Inbounds(difficulty, Guesses[i]))
                {
                    lbl_Response.Content = Generic_Messages.DisplayPreviousGuesses(Guesses);

                    if (level.CheckGuess(targetNumber, Guesses[i]))
                    {
                        lbl_Response.Content = $"You Guessed the correct Number and\n you are Awarded {ScoreData.Pointcheck(i + 1)}\n Points for beating it in {i + 1} Guesses";

                        setScoreData(difficulty, i + 1);


                    }
 
                }
                else
                {
                    lbl_Response.Content = "number entered was outside of guess range";
                    Guesses[i] = 0;
                    i--;
                }
               
            }
            else if (i >2)
            {
            
                lbl_Response.Content = "You Lose";
            }
            i++;
           
        }



        private void Btn_guess_Click(object sender, RoutedEventArgs e)
        {
           
            StartRound(difficulty);
        }

        private void Btn_getscoreList_Click(object sender, RoutedEventArgs e)
        {
            DisplayScoreBoard(CurrentScore.GetScoreData());
        }



        private  void DisplayScoreBoard(Dictionary<int, ScoreData> collection)
        {
            lstb_ScoreBoard.Items.Clear();
           
            foreach (var item in collection)
            {
                lstb_ScoreBoard.Items.Add("Game Number: " + item.Key + "\nPoints:" + item.Value.Points + "\nDifficulty:" + item.Value.Difficulty + "\nGuesses:" + item.Value.Guesses + "\nName:" + item.Value.Name);
               
            }
          
        }
         void setScoreData(int diff, int guesses)
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



            if (txt_name.Text.Length <6)
            {
                score.Name = txt_name.Text;
                txt_name.Clear();
            }
            else
            {
                score.Name = "?????";
            }

            CurrentScore.setScoreData(score);
            
        }

            private void Chck_easy_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
