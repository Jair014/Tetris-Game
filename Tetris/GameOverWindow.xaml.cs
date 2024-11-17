//-----------------------------------------------------------------------
// <copyright file="GameOverWindow.xaml.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Tetris
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for GameOverWindow
    /// </summary>
    public partial class GameOverWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverWindow" /> class.
        /// </summary>
        /// <param name="score">The first name to join.</param>
        public GameOverWindow(int score)
        {
            this.InitializeComponent();

            this.Score = score;

            if (this.IsHighScore())
            {
                this.HighScoreText.Visibility = Visibility.Visible;
                this.InstructionText.Visibility = Visibility.Visible;
                this.InitialsTextBox.Visibility = Visibility.Visible;
                this.SubmitButton.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsClicked { get; set; }

        /// <summary>
        /// Gets a value indicating whether.
        /// </summary>
        private int Score { get; }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <returns>The joined names.</returns>
        public bool IsHighScore()
        {
            bool isHighScore = false;

            List<int> scores = new List<int>();

            foreach (string line in File.ReadLines("HighScores.txt"))
            {
                scores.Add(int.Parse(line.Remove(0, 5)));
            }

            foreach (int s in scores)
            {
                if (this.Score >= s)
                {
                    isHighScore = true;
                }
            }

            return isHighScore;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="initials">The first name to join.</param>
        public void UpdateHighScores(string initials)
        {
            List<int> scores = new List<int>();
            List<string> fullScores = new List<string>();
            List<string> highScores = new List<string>();
            int scoreNum, count = 0;

            foreach (string line in File.ReadLines("HighScores.txt"))
            {
                fullScores.Add(line);

                scores.Add(int.Parse(line.Remove(0, 5)));
            }

            foreach (int s in scores)
            {
                count++;

                if (this.Score >= s)
                {
                    scoreNum = count;

                    switch (scoreNum)
                    {
                        case 1:
                            highScores.Add(initials + " - " + this.Score.ToString());
                            highScores.Add(fullScores[0]);
                            highScores.Add(fullScores[1]);
                            break;
                        case 2:
                            highScores.Add(fullScores[0]);
                            highScores.Add(initials + " - " + this.Score.ToString());
                            highScores.Add(fullScores[1]);
                            break;
                        case 3:
                            highScores.Add(fullScores[0]);
                            highScores.Add(fullScores[1]);
                            highScores.Add(initials + " - " + this.Score.ToString());
                            break;
                    }
                }
            }

            File.WriteAllText("HighScores.txt", highScores[0] + Environment.NewLine + highScores[1] + Environment.NewLine + highScores[2]);
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Close();

            GameWindow win1 = new GameWindow();

            win1.ShowDialog();

            if (!win1.IsClicked)
            {
                App.Current.Shutdown();
            }
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Close();

            MainWindow win1 = new MainWindow();

            win1.ShowDialog();

            if (!win1.IsClicked)
            {
                App.Current.Shutdown();
            }
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            App.Current.Shutdown();
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string initials = InitialsTextBox.Text.ToUpper();

            if (initials.Length != 2)
            {
                MessageBox.Show("Please enter your first and last initial.");
                InitialsTextBox.Clear();
            }
            else
            {
                if (char.IsLetter(initials[0]) && char.IsLetter(initials[1]))
                {
                    SubmitButton.Visibility = Visibility.Hidden;
                    InitialsTextBox.Visibility = Visibility.Hidden;

                    this.UpdateHighScores(initials);
                }
                else
                {
                    MessageBox.Show("Please enter only letters for your initials.");
                    InitialsTextBox.Clear();
                }
            }
        }
    }
}
