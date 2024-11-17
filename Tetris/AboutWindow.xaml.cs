//-----------------------------------------------------------------------
// <copyright file="AboutWindow.xaml.cs" company="CompanyName">
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
    /// Interaction logic for AboutWindow
    /// </summary>
    public partial class AboutWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutWindow" /> class.
        /// </summary>
        public AboutWindow()
        {
            // <summary>
            // Initializes a new instance of the <see cref="AboutWindow" /> class.
            // </summary>
            this.InitializeComponent();

            int count = 0;

            foreach (string line in File.ReadLines("HighScores.txt"))
            {
                count++;

                ((Label)this.FindName("HighScore" + count + "Label")).Content = line;
            }
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
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // <summary>
            // Return button click.
            // </summary>
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
