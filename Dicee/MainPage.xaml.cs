using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public MainPage()
        {
            this.InitializeComponent();

            turnScore = 0;
            totalScore = 0;
            numOfRolls = 0;
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
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
        }

        private void CalculateTurnScore(int firstRoll, int secondRoll)
        {
            if (firstRoll == secondRoll)
            {
                if (firstRoll == 6)
                    turnScore = 100;
                else
                    turnScore = 10;
            }
            else
                turnScore = 0;
        }

        private void UpdateTotalScore()
        {
            totalScore += turnScore;
        }
    }
}
