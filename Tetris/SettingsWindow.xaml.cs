//-----------------------------------------------------------------------
// <copyright file="SettingsWindow.xaml.cs" company="CompanyName">
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
    /// Interaction logic for SettingsWindow
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private string AILevel = "None";

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindow" /> class.
        /// </summary>
        public SettingsWindow()
        {
            this.InitializeComponent();

            this.IsClicked = false;
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
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Close();
            var win1 = new MainWindow(AILevel);
            win1.ShowDialog();

            if (!win1.IsClicked)
            {
                App.Current.Shutdown();
            }
        }

        private void NoAIRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AILevel = "None";
        }

        private void EasyAIRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AILevel = "Easy";
        }

        private void InsaneAIRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AILevel = "Insane";
        }
    }
}
