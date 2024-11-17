//-----------------------------------------------------------------------
// <copyright file="PauseWindow.xaml.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Tetris
{
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
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for PauseWindow
    /// </summary>
    public partial class PauseWindow : Window
    {
        private string AILevel = "None";

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseWindow" /> class.
        /// </summary>
        public PauseWindow(string AILevel)
        {
            this.InitializeComponent();

            this.AILevel = AILevel;

            this.ResumeIsClicked = false;
            this.IsClicked = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsClicked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool ResumeIsClicked { get; set; }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            this.ResumeIsClicked = true;
            this.IsClicked = true;

            this.Hide();
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Close();
            var win1 = new GameWindow(AILevel);
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
            var win1 = new MainWindow();
            win1.ShowDialog();

            if (!win1.IsClicked)
            {
                App.Current.Shutdown();
            }
        }
    }
}
