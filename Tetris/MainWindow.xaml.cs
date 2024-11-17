//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="CompanyName">
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        private string AILevel = "None";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }
        public MainWindow(string AILevel)
        {
            this.InitializeComponent();

            this.AILevel = AILevel;
        }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsClicked { get; set; }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Hide();
            GameWindow win1 = new GameWindow(AILevel);
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
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Hide();
            var win1 = new AboutWindow();
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
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Hide();
            var win1 = new SettingsWindow();
            win1.ShowDialog();

            if (!win1.IsClicked)
            {
                App.Current.Shutdown();
            }
        }
    }
}
