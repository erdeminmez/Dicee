using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Dicee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int turnScore;
        int totalScore;
        int numOfRolls;
        bool isJackpot;
        bool isDoubleJackpot;
        bool isAnother;
        int highestTotalScore;

        public MainPage()
        {
            this.InitializeComponent();

            turnScore = 0;
            totalScore = 0;
            numOfRolls = 0;
            isJackpot = false;
            isDoubleJackpot = false;
            isAnother = true;
            highestTotalScore = 0;
        }

        private async void RollButton_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int firstRoll = r.Next(1, 7);
            int secondRoll = r.Next(1, 7);

            FirstDice.Text = $"{firstRoll}";
            SecondDice.Text = $"{secondRoll}";

            this.CalculateTurnScore(firstRoll, secondRoll);
            this.UpdateTotalScore();
            numOfRolls += 1;

            TurnScore.Text = $"Turn Score is {turnScore} point(s)";
            OverallScore.Text = $"Overall Score is {totalScore} point(s)";
            RolledTimes.Text = $"Dice rolled {numOfRolls} times";

            if (isDoubleJackpot)
            {
                isDoubleJackpot = false;
                MessageDialog messageDialog = new MessageDialog("You've got double jackpot!");
                await messageDialog.ShowAsync();
            }

            if (numOfRolls == 10)
            {
                this.EndTheGame();
            }
        }

        private void CalculateTurnScore(int firstRoll, int secondRoll)
        {
            if (firstRoll == secondRoll)
            {
                if (firstRoll == 6) 
                {
                    if (isJackpot)
                    {
                        if (isAnother)
                        {
                            turnScore = 200;
                            isDoubleJackpot = true;
                        }
                        isJackpot = false;
                        isAnother = false;
                    }
                    else
                    {
                        turnScore = 100;
                        isJackpot = true;
                    }
                }
                else
                {
                    turnScore = 10;
                    isJackpot = false;
                    isAnother = true;
                }
            }
            else
            {
                turnScore = 0;
                isJackpot = false;
                isAnother = true;
            }
        }

        private void UpdateTotalScore()
        {
            totalScore += turnScore;
        }

        private async void EndTheGame()
        {
            MessageDialog messageDialog = new MessageDialog($"Game is over! Your overall score is {totalScore} points");
            await messageDialog.ShowAsync();

            if (highestTotalScore < totalScore)
            {
                highestTotalScore = totalScore;
                MessageDialog messageDialog2 = new MessageDialog("Congratulations! New score record!");
                await messageDialog2.ShowAsync();
            }
            this.resetTheGame();
        }

        private void resetTheGame()
        {
            turnScore = 0;
            totalScore = 0;
            numOfRolls = 0;
            isJackpot = false;
            isDoubleJackpot = false;
            isAnother = true;

            FirstDice.Text = "?";
            SecondDice.Text = "?";
            TurnScore.Text = $"Turn Score is {turnScore} point(s)";
            OverallScore.Text = $"Overall Score is {totalScore} point(s)";
            RolledTimes.Text = $"Dice rolled {numOfRolls} times";
        }
    }
}
