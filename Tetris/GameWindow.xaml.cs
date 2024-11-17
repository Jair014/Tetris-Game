//-----------------------------------------------------------------------
// <copyright file="GameWindow.xaml.cs" company="CompanyName">
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
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Disassemblers;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Interaction logic for GameWindow
    /// </summary>
    public partial class GameWindow : Window
    {
        /// <summary>
        /// Creating field.
        /// </summary>
        private int[,] boardState = new int[20, 10];

        /// <summary>
        /// Creating field.
        /// </summary>
        private Rectangle[,] cells = new Rectangle[20, 10];

        /// <summary>
        /// Creating field.
        /// </summary>
        private List<Rectangle> position = new List<Rectangle>();

        /// <summary>
        /// Creating field.
        /// </summary>
        private List<Rectangle> displayPosition = new List<Rectangle>();

        /// <summary>
        /// Creating field.
        /// </summary>
        private List<Rectangle> oldDisplayPosition = new List<Rectangle>();

        /// <summary>
        /// Creating field.
        /// </summary>
        private int pieceNum;

        /// <summary>
        /// Creating field.
        /// </summary>
        private int nextPieceNum;

        /// <summary>
        /// Creating field.
        /// </summary>
        private Piece newPiece = new Piece(0);

        /// <summary>
        /// Creating field.
        /// </summary>
        private Piece displayPiece = new Piece(0);

        /// <summary>
        /// Creating field.
        /// </summary>
        private int score;

        /// <summary>
        /// Creating field.
        /// </summary>
        private int level = 0;

        /// <summary>
        /// Creating field.
        /// </summary>
        private int linesCleared = 0;

        /// <summary>
        /// Creating field.
        /// </summary>
        private DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        /// <summary>
        /// Creating field.
        /// </summary>
        private int speed = 500;

        /// <summary>
        /// Creating field.
        /// </summary>
        private string AILevel = "None";

        /// <summary>
        /// Creating field.
        /// </summary>
        private bool isGameOver = false;

        /// <summary>
        /// Creating field.
        /// </summary>
        private bool isLocked = false;

        /// <summary>
        /// Creating field.
        /// </summary>
        private bool didMove = false;

        /// <summary>
        /// Creating field.
        /// </summary>
        private double scoreModifier = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWindow" /> class.
        /// </summary>
        public GameWindow()
        {
            this.InitializeComponent();
            
            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    this.boardState[row, col] = 0;
                }
            }

            int count = 1;

            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    this.cells[row, col] = (Rectangle)this.FindName("Cell" + count);

                    count++;
                }
            }

            for (int c = 1; c < 13; c++)
            {
                this.oldDisplayPosition.Add((Rectangle)this.FindName("DisplayCell" + c));
            }

            this.ScoreTextBox.Content = this.score.ToString();

            this.Start();

            this.timer.Tick += new EventHandler(this.Timer_Tick);
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            this.timer.Start();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWindow"/> class
        /// </summary>
        /// <param name="AILevel">Ai Level</param>
        public GameWindow(string AILevel)
        {
            this.InitializeComponent();

            this.AILevel = AILevel;

            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    this.boardState[row, col] = 0;
                }
            }

            int count = 1;

            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    this.cells[row, col] = (Rectangle)this.FindName("Cell" + count);

                    count++;
                }
            }

            for (int c = 1; c < 13; c++)
            {
                this.oldDisplayPosition.Add((Rectangle)this.FindName("DisplayCell" + c));
            }

            this.ScoreTextBox.Content = this.score.ToString();

            this.Start();

            this.timer.Tick += new EventHandler(this.Timer_Tick);
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            this.timer.Start();
        }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsClicked { get; set; }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        public void Start()
        {
            this.position.Clear();
            this.displayPosition.Clear();

            Random rand = new Random();

            this.pieceNum = rand.Next(7);
            this.nextPieceNum = rand.Next(7);

            this.ScoreTextBox.Content = this.score.ToString();
            this.LinesClearedTextBox.Content = this.linesCleared.ToString();

            switch (this.pieceNum)
            {
                case 0:
                    this.newPiece = new Piece(0);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell25);
                    this.position.Add(this.Cell35);
                    break;
                case 1:
                    this.newPiece = new Piece(1);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell16);
                    break;
                case 2:
                    this.newPiece = new Piece(2);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell14);
                    break;
                case 3:
                    this.newPiece = new Piece(3);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell16);
                    this.position.Add(this.Cell17);
                    break;
                case 4:
                    this.newPiece = new Piece(4);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell25);
                    this.position.Add(this.Cell26);
                    break;
                case 5:
                    this.newPiece = new Piece(5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell16);
                    this.position.Add(this.Cell26);
                    this.position.Add(this.Cell25);
                    break;
                case 6:
                    this.newPiece = new Piece(6);
                    this.position.Add(this.Cell4);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell15);
                    break;
            }

            switch (this.nextPieceNum)
            {
                case 0:
                    this.displayPiece = new Piece(0);
                    this.displayPosition.Add(this.DisplayCell2);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell11);
                    break;
                case 1:
                    this.displayPiece = new Piece(1);
                    this.displayPosition.Add(this.DisplayCell4);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell7);
                    this.displayPosition.Add(this.DisplayCell8);
                    break;
                case 2:
                    this.displayPiece = new Piece(2);
                    this.displayPosition.Add(this.DisplayCell6);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell7);
                    break;
                case 3:
                    this.displayPiece = new Piece(3);
                    this.displayPosition.Add(this.DisplayCell4);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell9);
                    break;
                case 4:
                    this.displayPiece = new Piece(4);
                    this.displayPosition.Add(this.DisplayCell2);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell9);
                    break;
                case 5:
                    this.displayPiece = new Piece(5);
                    this.displayPosition.Add(this.DisplayCell2);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell7);
                    break;
                case 6:
                    this.displayPiece = new Piece(6);
                    this.displayPosition.Add(this.DisplayCell4);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell6);
                    this.displayPosition.Add(this.DisplayCell8);
                    break;
            }

            this.displayPiece.DrawPiece(this.oldDisplayPosition, this.displayPosition);
            this.newPiece.DrawPiece(this.position, this.position);
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        public void NextBlock()
        {
            this.position.Clear();
            this.displayPosition.Clear();
            this.isLocked = false;
            this.didMove = false;

            Random rand = new Random();

            this.pieceNum = this.nextPieceNum;
            this.nextPieceNum = rand.Next(7);

            this.ScoreTextBox.Content = this.score.ToString();
            this.LinesClearedTextBox.Content = this.linesCleared.ToString();

            switch (this.pieceNum)
            {
                case 0:
                    this.newPiece = new Piece(0);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell25);
                    this.position.Add(this.Cell35);
                    break;
                case 1:
                    this.newPiece = new Piece(1);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell16);
                    break;
                case 2:
                    this.newPiece = new Piece(2);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell14);
                    break;
                case 3:
                    this.newPiece = new Piece(3);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell16);
                    this.position.Add(this.Cell17);
                    break;
                case 4:
                    this.newPiece = new Piece(4);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell15);
                    this.position.Add(this.Cell25);
                    this.position.Add(this.Cell26);
                    break;
                case 5:
                    this.newPiece = new Piece(5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell16);
                    this.position.Add(this.Cell26);
                    this.position.Add(this.Cell25);
                    break;
                case 6:
                    this.newPiece = new Piece(6);
                    this.position.Add(this.Cell4);
                    this.position.Add(this.Cell5);
                    this.position.Add(this.Cell6);
                    this.position.Add(this.Cell15);
                    break;
            }

            switch (this.nextPieceNum)
            {
                case 0:
                    this.displayPiece = new Piece(0);
                    this.displayPosition.Add(this.DisplayCell2);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell11);
                    break;
                case 1:
                    this.displayPiece = new Piece(1);
                    this.displayPosition.Add(this.DisplayCell4);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell7);
                    this.displayPosition.Add(this.DisplayCell8);
                    break;
                case 2:
                    this.displayPiece = new Piece(2);
                    this.displayPosition.Add(this.DisplayCell6);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell7);
                    break;
                case 3:
                    this.displayPiece = new Piece(3);
                    this.displayPosition.Add(this.DisplayCell4);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell9);
                    break;
                case 4:
                    this.displayPiece = new Piece(4);
                    this.displayPosition.Add(this.DisplayCell2);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell9);
                    break;
                case 5:
                    this.displayPiece = new Piece(5);
                    this.displayPosition.Add(this.DisplayCell2);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell8);
                    this.displayPosition.Add(this.DisplayCell7);
                    break;
                case 6:
                    this.displayPiece = new Piece(6);
                    this.displayPosition.Add(this.DisplayCell4);
                    this.displayPosition.Add(this.DisplayCell5);
                    this.displayPosition.Add(this.DisplayCell6);
                    this.displayPosition.Add(this.DisplayCell8);
                    break;
            }

            this.displayPiece.DrawPiece(this.oldDisplayPosition, this.displayPosition);
            this.newPiece.DrawPiece(this.position, this.position);

            this.timer.Stop();
            this.timer.Start();
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.AILevel.Equals("Easy") || this.AILevel.Equals("Insane"))
            {
                if (e.Key == Key.Escape)
                {
                    this.PauseButton_Click(sender, e);
                }

                return;
            }

            if (this.GameOver())
            {
                if (!this.isGameOver)
                {
                    this.isGameOver = true;

                    this.Close();

                    GameOverWindow win = new GameOverWindow(this.score);

                    win.ShowDialog();
                }

                return;
            }
            
            List<Rectangle> prevPos = this.position.ToList();

            if (e.Key == Key.A)
            {
                this.MoveLeft();
            }
            else if (e.Key == Key.D)
            {
                this.MoveRight();
            }
            else if (e.Key == Key.W)
            {
                this.Rotate();
            }
            else if (e.Key == Key.S)
            {
                this.MoveDown();
            }
            else if (e.Key == Key.Escape)
            {
                this.PauseButton_Click(sender, e);
            }
            else
            {
                return;
            }

            this.DrawAndVerifyPiece(prevPos);
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public bool IsBlockBelow(List<Rectangle> pos)
        {
            bool isBlockBelow = false;

            for (int count = 0; count < 4; count++)
            {
                for (int row = 0; row < 20; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        if (this.cells[row, col].Name == pos[count].Name)
                        {
                            if (row == 19)
                            {
                                isBlockBelow = false;
                            }
                            else if (this.boardState[(row + 1), col] == 1)
                            {
                                isBlockBelow = true;
                            }
                        }
                    }
                }
            }

            return isBlockBelow;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public bool IsBlockLeft(List<Rectangle> pos)
        {
            bool isBlockLeft = false;

            for (int count = 0; count < 4; count++)
            {
                for (int row = 0; row < 20; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        if (this.cells[row, col].Name == pos[count].Name)
                        {
                            if (col == 0)
                            {
                                isBlockLeft = false;
                            }
                            else if (this.boardState[row, (col - 1)] == 1)
                            {
                                isBlockLeft = true;
                            }
                        }
                    }
                }
            }

            return isBlockLeft;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public bool IsBlockRight(List<Rectangle> pos)
        {
            bool isBlockRight = false;

            for (int count = 0; count < 4; count++)
            {
                for (int row = 0; row < 20; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        if (this.cells[row, col].Name == pos[count].Name)
                        {
                            if (col == 9)
                            {
                                isBlockRight = false;
                            }
                            else if (this.boardState[row, (col + 1)] == 1)
                            {
                                isBlockRight = true;
                            }
                        }
                    }
                }
            }

            return isBlockRight;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public bool IsPieceInPosition(List<Rectangle> pos)
        {
            bool isPieceInPosition = false;

            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (this.cells[row, col].Name.Equals(pos[0].Name))
                    {
                        if (this.boardState[row, col] == 1)
                        {
                            isPieceInPosition = true;
                        }
                    }

                    if (this.cells[row, col].Name.Equals(pos[1].Name))
                    {
                        if (this.boardState[row, col] == 1)
                        {
                            isPieceInPosition = true;
                        }
                    }

                    if (this.cells[row, col].Name.Equals(pos[2].Name))
                    {
                        if (this.boardState[row, col] == 1)
                        {
                            isPieceInPosition = true;
                        }
                    }

                    if (this.cells[row, col].Name.Equals(pos[3].Name))
                    {
                        if (this.boardState[row, col] == 1)
                        {
                            isPieceInPosition = true;
                        }
                    }
                }
            }

            return isPieceInPosition;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <returns>The joined names.</returns>
        public bool GameOver()
        {
            bool gameOver = false;

            for (int i = 0; i < 10; i++)
            {
                if (this.boardState[0, i] == 1)
                {
                    this.IsClicked = true;
                    gameOver = true;
                }
            }

            return gameOver;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <returns>The joined names.</returns>
        public int FindFullRow()
        {
            int row = 0;

            for (int r = 20; r > 1; r--)
            {
                bool isRowFull = true;

                for (int c = 1; c < 11; c++)
                {
                    if (this.boardState[r - 1, c - 1] == 0)
                    {
                        isRowFull = false;
                    }
                }

                if (isRowFull == true)
                {
                    return r;
                }
            }

            return row;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <returns>The joined names.</returns>
        public int NumberFullRows()
        {
            int count = 0;

            for (int r = 20; r > 1; r--)
            {
                bool isRowFull = true;

                for (int c = 1; c < 11; c++)
                {
                    if (this.boardState[r - 1, c - 1] == 0)
                    {
                        isRowFull = false;
                    }
                }

                if (isRowFull == true)
                {
                    count += 1;
                }
            }

            return count;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            List<string> cellNames;
            List<Rectangle> prevPos = this.position.ToList();
            bool isValid = true;

            foreach (Rectangle part in this.position)
            {
                if (part == this.Cell191 || part == this.Cell192 || part == this.Cell193 || part == this.Cell194 || part == this.Cell195 ||
                    part == this.Cell196 || part == this.Cell197 || part == this.Cell198 || part == this.Cell199 || part == this.Cell200)
                {
                    isValid = false;
                    this.isLocked = true;
                }
                else if (this.IsBlockBelow(this.position))
                {
                    isValid = false;
                    this.isLocked = true;
                }
            }

            if (isValid)
            {
                cellNames = this.newPiece.MoveDown(this.position);

                this.position.Clear();

                var cell = (Rectangle)this.FindName(cellNames[0]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[1]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[2]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[3]);
                this.position.Add(cell);
            }

            this.newPiece.DrawPiece(prevPos, this.position);

            if (this.isLocked)
            {
                this.SetBoardState();

                int fullRows = this.NumberFullRows();

                for (int f = 1; f <= fullRows; f++)
                {
                    this.linesCleared++;

                    if (this.linesCleared != 0 && this.level < 9 && this.linesCleared % 10 == 0)
                    {
                        this.level++;
                        this.LevelTextBox.Content = this.level.ToString();

                        this.SpeedUpgrade();
                    }
                }

                switch (fullRows)
                {
                    case 0:
                        break;
                    case 1:
                        this.score += 100 + (int)(100 * this.scoreModifier);
                        break;                         
                    case 2:                            
                        this.score += 300 + (int)(300 * this.scoreModifier);
                        break;                        
                    case 3:                           
                        this.score += 500 + (int)(500 * this.scoreModifier);
                        break;                          
                    case 4:                             
                        this.score += 800 + (int)(800 * this.scoreModifier);
                        break;
                }

                if (this.GameOver())
                {
                    if (!this.isGameOver)
                    {
                        if (this.AILevel.Equals("Easy") || this.AILevel.Equals("Insane"))
                        {
                            this.Close();
                            GameWindow win1 = new GameWindow(this.AILevel);
                            win1.ShowDialog();

                            if (!win1.IsClicked)
                            {
                                App.Current.Shutdown();
                            }

                            return;
                        }

                        this.isGameOver = true;

                        this.Close();

                        GameOverWindow win = new GameOverWindow(this.score);

                        win.ShowDialog();
                    }

                    return;
                }

                while (this.FindFullRow() != 0)
                {
                    for (int cell = 0; cell < 10; cell++)
                    {
                        this.cells[this.FindFullRow() - 1, cell].Fill = Brushes.White;
                    }

                    for (int r = this.FindFullRow(); r > 2; r--)
                    {
                        for (int c = 0; c < 10; c++)
                        {
                            this.cells[r - 1, c].Fill = this.cells[r - 2, c].Fill;
                        }
                    }

                    this.SetBoardState();
                }

                if (!this.GameOver())
                {
                    this.NextBlock();

                    return;
                }
            }

            this.ComputerMove();
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        public void SpeedUpgrade()
        {
            this.timer.Stop();

            if (this.level != 0)
            {
                double speedModifier = (double)this.level / 10;
                this.speed = (int)(500 - (500 * speedModifier));

                this.scoreModifier = (double)this.level / 10;
            }

            this.timer.Interval = new TimeSpan(0, 0, 0, 0, this.speed);
            this.timer.Start();
        }

        /// <summary>
        /// Method to decide what the Easy AI will do
        /// </summary>
        public void EasyComputerMove()
        {
            Random rand = new Random();
            int num = rand.Next(4);
            
            List<Rectangle> prevPos = this.position.ToList();

            switch (num)
            {
                case 0:
                    this.MoveLeft();
                    break;
                case 1:
                    this.MoveRight();
                    break;
                case 2:
                    this.Rotate();
                    break;
                case 3:
                    this.MoveDown();
                    break;
            }

            this.DrawAndVerifyPiece(prevPos);
        }

        /// <summary>
        /// Method to determine Insane difficulty AI move
        /// </summary>
        public void InsaneComputerMove()
        {
            List<Rectangle> prevPos = this.position.ToList();
            List<Rectangle> toPos = new List<Rectangle>();

            switch (this.pieceNum)
            {
                ////I Piece
                case 0:
                    for (int row = 19; row >= 0; row--)
                    {
                        for (int col = 0; col < 7; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 && this.boardState[row, col + 2] == 0 &&
                                this.boardState[row, col + 3] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row, col + 2]);
                                toPos.Add(this.cells[row, col + 3]);
                                          
                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[3]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 0; col < 10; col++)
                        {
                            toPos.Clear();

                            toPos.Add(this.cells[row, col]);
                            toPos.Add(this.cells[row - 1, col]);
                            toPos.Add(this.cells[row - 2, col]);
                            toPos.Add(this.cells[row - 3, col]);

                            if (this.CanDrop(toPos, row))
                            {
                                this.MoveToColumn(col, this.position[3]);
                                this.DrawAndVerifyPiece(prevPos);

                                this.didMove = true;

                                return;
                            }
                        }
                    }

                    break;
                ////Square Piece
                case 1:
                    for (int row = 19; row >= 0; row--)
                    {
                        if (row < 19)
                        {
                            for (int col = 0; col <= 8; col++)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 &&
                                    this.boardState[row - 1, col] == 0 && this.boardState[row - 1, col + 1] == 0 &&
                                    this.boardState[row + 1, col] == 1 && this.boardState[row + 1, col + 1] == 1)
                                {
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row, col + 1]);
                                    toPos.Add(this.cells[row - 1, col]);
                                    toPos.Add(this.cells[row - 1, col + 1]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.MoveToColumn(col, this.position[2]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        for (int col = 0; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 &&
                                this.boardState[row - 1, col] == 0 && this.boardState[row - 1, col + 1] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 1, col + 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.MoveToColumn(col, this.position[2]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }
                    }

                    break;
                ////S Piece
                case 2:
                    for (int row = 19; row >= 0; row--)
                    {
                        for (int col = 0; col <= 7; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 && this.boardState[row - 1, col + 1] == 0 &&
                                this.boardState[row - 1, col + 2] == 0 && this.boardState[row, col + 2] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row - 1, col + 1]);
                                toPos.Add(this.cells[row - 1, col + 2]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.MoveToColumn(col, this.position[3]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 1; col <= 9; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 1, col - 1] == 0 &&
                                this.boardState[row - 2, col - 1] == 0 && this.boardState[row, col - 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 1, col - 1]);
                                toPos.Add(this.cells[row - 2, col - 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[0]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        if (row < 19)
                        {
                            for (int col = 0; col <= 7; col++)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 && this.boardState[row - 1, col + 1] == 0 &&
                                    this.boardState[row - 1, col + 2] == 0 && this.boardState[row + 1, col] == 1 && this.boardState[row + 1, col + 1] == 1)
                                {   
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row, col + 1]);
                                    toPos.Add(this.cells[row - 1, col + 1]);
                                    toPos.Add(this.cells[row - 1, col + 2]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.MoveToColumn(col, this.position[3]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        for (int col = 0; col <= 7; col++)
                        {
                            toPos.Clear();

                            toPos.Add(this.cells[row, col]);
                            toPos.Add(this.cells[row, col + 1]);
                            toPos.Add(this.cells[row - 1, col + 1]);
                            toPos.Add(this.cells[row - 1, col + 2]);

                            if (this.CanDrop(toPos, row))
                            {
                                this.MoveToColumn(col, this.position[3]);
                                this.DrawAndVerifyPiece(prevPos);

                                this.didMove = true;

                                return;
                            }
                        }
                    }

                    break;
                ////Z Piece
                case 3:
                    for (int row = 19; row >= 0; row--)
                    {
                        for (int col = 1; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 && this.boardState[row - 1, col] == 0 &&
                                this.boardState[row - 1, col - 1] == 0 && this.boardState[row, col - 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 1, col - 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.MoveToColumn(col, this.position[2]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 0; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 1, col + 1] == 0 &&
                                this.boardState[row - 2, col + 1] == 0 && this.boardState[row, col + 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 1, col + 1]);
                                toPos.Add(this.cells[row - 2, col + 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[3]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        if (row < 19)
                        {
                            for (int col = 1; col <= 8; col++)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 && this.boardState[row - 1, col] == 0 &&
                                    this.boardState[row - 1, col - 1] == 0 && this.boardState[row + 1, col] == 1 && this.boardState[row + 1, col + 1] == 1)
                                {
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row, col + 1]);
                                    toPos.Add(this.cells[row - 1, col]);
                                    toPos.Add(this.cells[row - 1, col - 1]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.MoveToColumn(col, this.position[2]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        for (int col = 9; col >= 2; col--)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col - 1] == 0 &&
                                this.boardState[row - 1, col - 1] == 0 && this.boardState[row - 1, col - 2] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col - 1]);
                                toPos.Add(this.cells[row - 1, col - 1]);
                                toPos.Add(this.cells[row - 1, col - 2]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.MoveToColumn(col, this.position[3]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }
                    }

                    break;
                ////L Piece
                case 4:
                    for (int row = 19; row >= 0; row--)
                    {
                        for (int col = 1; col <= 9; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0 &&
                                this.boardState[row - 2, col - 1] == 0 && this.boardState[row, col - 1] == 1 && this.boardState[row - 1, col - 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 2, col]);
                                toPos.Add(this.cells[row - 2, col - 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[0]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 0; col <= 7; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 &&
                                this.boardState[row - 1, col + 1] == 0 && this.boardState[row - 1, col + 2] == 0 &&
                                this.boardState[row, col + 1] == 1 && this.boardState[row, col + 2] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 1, col + 1]);
                                toPos.Add(this.cells[row - 1, col + 2]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[3]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        if (row < 19)
                        {
                            for (int col = 0; col <= 8; col++)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 &&
                                    this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0 &&
                                    this.boardState[row + 1, col] == 1 && this.boardState[row + 1, col + 1] == 1)
                                {
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row, col + 1]);
                                    toPos.Add(this.cells[row - 1, col]);
                                    toPos.Add(this.cells[row - 2, col]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.MoveToColumn(col, this.position[2]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        if (row < 19)
                        {
                            for (int col = 0; col <= 7; col++)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 &&
                                    this.boardState[row, col + 2] == 0 && this.boardState[row - 1, col + 2] == 0 &&
                                    this.boardState[row + 1, col] == 1 && this.boardState[row + 1, col + 1] == 1 && this.boardState[row + 1, col + 2] == 1)
                                {
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row, col + 1]);
                                    toPos.Add(this.cells[row, col + 2]);
                                    toPos.Add(this.cells[row - 1, col + 2]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.Rotate();
                                        this.Rotate();
                                        this.Rotate();
                                        this.DrawAndVerifyPiece(prevPos);
                                        prevPos = this.position.ToList();

                                        this.MoveToColumn(col, this.position[0]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        for (int col = 0; col <= 7; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 &&
                                this.boardState[row, col + 2] == 0 && this.boardState[row - 1, col + 2] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row, col + 2]);
                                toPos.Add(this.cells[row - 1, col + 2]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.Rotate();
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[0]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 0; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 &&
                                this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 2, col]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.MoveToColumn(col, this.position[2]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }
                    }

                    break;
                ////J Piece
                case 5:
                    for (int row = 19; row >= 0; row--)
                    {
                        for (int col = 0; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0 &&
                                this.boardState[row - 2, col + 1] == 0 && this.boardState[row, col + 1] == 1 && this.boardState[row - 1, col + 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 2, col]);
                                toPos.Add(this.cells[row - 2, col + 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[0]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 2; col <= 9; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 &&
                                this.boardState[row - 1, col - 1] == 0 && this.boardState[row - 1, col - 2] == 0 &&
                                this.boardState[row, col - 1] == 1 && this.boardState[row, col - 2] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 1, col - 1]);
                                toPos.Add(this.cells[row - 1, col - 2]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.Rotate();
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[3]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        if (row < 19)
                        {
                            for (int col = 9; col >= 1; col--)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row, col - 1] == 0 &&
                                    this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0 &&
                                    this.boardState[row + 1, col] == 1 && this.boardState[row + 1, col - 1] == 1)
                                {
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row, col - 1]);
                                    toPos.Add(this.cells[row - 1, col]);
                                    toPos.Add(this.cells[row - 2, col]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.MoveToColumn(col, this.position[2]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        if (row < 19)
                        {
                            for (int col = 0; col <= 7; col++)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 &&
                                    this.boardState[row, col + 1] == 0 && this.boardState[row, col + 2] == 0 &&
                                    this.boardState[row + 1, col] == 1 && this.boardState[row + 1, col + 1] == 1 && this.boardState[row + 1, col + 2] == 1)
                                {
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row - 1, col]);
                                    toPos.Add(this.cells[row, col + 1]);
                                    toPos.Add(this.cells[row, col + 2]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.Rotate();
                                        this.DrawAndVerifyPiece(prevPos);
                                        prevPos = this.position.ToList();

                                        this.MoveToColumn(col, this.position[2]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        for (int col = 0; col <= 7; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 &&
                                this.boardState[row, col + 1] == 0 && this.boardState[row, col + 2] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row, col + 2]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[2]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 9; col >= 1; col--)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col - 1] == 0 &&
                                this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col - 1]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 2, col]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.MoveToColumn(col, position[2]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }
                    }

                    break;
                ////T Piece
                case 6:
                    for (int row = 19; row >= 0; row--)
                    {
                        for (int col = 1; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 1, col + 1] == 0 &&
                                this.boardState[row - 1, col - 1] == 0 && this.boardState[row, col - 1] == 1 && this.boardState[row, col + 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 1, col + 1]);
                                toPos.Add(this.cells[row - 1, col - 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.MoveToColumn(col, this.position[3]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 0; col <= 7; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 &&
                                this.boardState[row, col + 2] == 0 && this.boardState[row - 1, col + 1] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row, col + 1]);
                                toPos.Add(this.cells[row, col + 2]);
                                toPos.Add(this.cells[row - 1, col + 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[2]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 1; col <= 9; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0 &&
                                this.boardState[row - 1, col - 1] == 0 && this.boardState[row, col - 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 2, col]);
                                toPos.Add(this.cells[row - 1, col - 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[2]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        for (int col = 0; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0 &&
                                this.boardState[row - 1, col + 1] == 0 && this.boardState[row, col + 1] == 1)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 2, col]);
                                toPos.Add(this.cells[row - 1, col + 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.Rotate();
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[0]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }

                        if (row < 19)
                        {
                            for (int col = 0; col <= 7; col++)
                            {
                                if (this.boardState[row, col] == 0 && this.boardState[row, col + 1] == 0 && this.boardState[row, col + 2] == 0 &&
                                    this.boardState[row - 1, col + 1] == 0 && this.boardState[row + 1, col] == 1 &&
                                    this.boardState[row + 1, col + 1] == 1 && this.boardState[row + 1, col + 2] == 1)
                                {
                                    toPos.Clear();

                                    toPos.Add(this.cells[row, col]);
                                    toPos.Add(this.cells[row, col + 1]);
                                    toPos.Add(this.cells[row, col + 2]);
                                    toPos.Add(this.cells[row - 1, col + 1]);

                                    if (this.CanDrop(toPos, row))
                                    {
                                        this.Rotate();
                                        this.Rotate();
                                        this.DrawAndVerifyPiece(prevPos);
                                        prevPos = this.position.ToList();

                                        this.MoveToColumn(col, this.position[2]);
                                        this.DrawAndVerifyPiece(prevPos);

                                        this.didMove = true;

                                        return;
                                    }
                                }
                            }
                        }

                        for (int col = 0; col <= 8; col++)
                        {
                            if (this.boardState[row, col] == 0 && this.boardState[row - 1, col] == 0 && this.boardState[row - 2, col] == 0 &&
                                this.boardState[row - 1, col + 1] == 0)
                            {
                                toPos.Clear();

                                toPos.Add(this.cells[row, col]);
                                toPos.Add(this.cells[row - 1, col]);
                                toPos.Add(this.cells[row - 2, col]);
                                toPos.Add(this.cells[row - 1, col + 1]);

                                if (this.CanDrop(toPos, row))
                                {
                                    this.Rotate();
                                    this.Rotate();
                                    this.Rotate();
                                    this.DrawAndVerifyPiece(prevPos);
                                    prevPos = this.position.ToList();

                                    this.MoveToColumn(col, this.position[0]);
                                    this.DrawAndVerifyPiece(prevPos);

                                    this.didMove = true;

                                    return;
                                }
                            }
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Method to execute ComputerMove.
        /// </summary>
        public void ComputerMove()
        {
            if (this.AILevel.Equals("Easy"))
            {
                this.EasyComputerMove();
            }
            else if (this.AILevel.Equals("Insane"))
            {
                if (!this.didMove)
                {
                    this.InsaneComputerMove();
                }
            }
        }

        /// <summary>
        /// Method to Rotate Block.
        /// </summary>
        public void Rotate()
        {
            bool cell1IsValid = false, cell2IsValid = false, cell3IsValid = false, cell4IsValid = false;

            List<string> cellNames = this.newPiece.Rotate(this.position);

            List<Rectangle> newPosition = new List<Rectangle>();

            var cell1 = (Rectangle)this.FindName(cellNames[0]);
            newPosition.Add(cell1);
            cell1 = (Rectangle)this.FindName(cellNames[1]);
            newPosition.Add(cell1);
            cell1 = (Rectangle)this.FindName(cellNames[2]);
            newPosition.Add(cell1);
            cell1 = (Rectangle)this.FindName(cellNames[3]);
            newPosition.Add(cell1);

            foreach (Rectangle square in this.cells)
            {
                if (cellNames[0].Equals(square.Name))
                {
                    cell1IsValid = true;
                }

                if (cellNames[1].Equals(square.Name))
                {
                    cell2IsValid = true;
                }

                if (cellNames[2].Equals(square.Name))
                {
                    cell3IsValid = true;
                }

                if (cellNames[3].Equals(square.Name))
                {
                    cell4IsValid = true;
                }
            }

            if (cell1IsValid && cell2IsValid && cell3IsValid && cell4IsValid)
            {
                if (!this.IsPieceInPosition(newPosition))
                {
                    this.position.Clear();

                    var cell = (Rectangle)this.FindName(cellNames[0]);
                    this.position.Add(cell);
                    cell = (Rectangle)this.FindName(cellNames[1]);
                    this.position.Add(cell);
                    cell = (Rectangle)this.FindName(cellNames[2]);
                    this.position.Add(cell);
                    cell = (Rectangle)this.FindName(cellNames[3]);
                    this.position.Add(cell);
                }
                else
                {
                    this.newPiece.ChangeRotation(false);
                }
            }
            else
            {
                this.newPiece.ChangeRotation(false);
            }
        }

        /// <summary>
        /// Method to move Block left.
        /// </summary>
        public void MoveLeft()
        {
            bool isValid = true;

            foreach (Rectangle part in this.position)
            {
                if (part == this.Cell1 || part == this.Cell11 || part == this.Cell21 || part == this.Cell31 || part == this.Cell41 ||
                    part == this.Cell51 || part == this.Cell61 || part == this.Cell71 || part == this.Cell81 || part == this.Cell91 ||
                    part == this.Cell101 || part == this.Cell111 || part == this.Cell121 || part == this.Cell131 || part == this.Cell141 ||
                    part == this.Cell151 || part == this.Cell161 || part == this.Cell171 || part == this.Cell181 || part == this.Cell191)
                {
                    isValid = false;
                }
                else if (this.IsBlockLeft(this.position))
                {
                    isValid = false;
                }
            }

            if (isValid)
            {
                List<string> cellNames = this.newPiece.MoveLeft(this.position);

                this.position.Clear();

                var cell = (Rectangle)this.FindName(cellNames[0]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[1]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[2]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[3]);
                this.position.Add(cell);
            }
        }

        /// <summary>
        /// Method to Move Block right.
        /// </summary>
        public void MoveRight()
        {
            bool isValid = true;

            foreach (Rectangle part in this.position)
            {
                if (part == this.Cell10 || part == this.Cell20 || part == this.Cell30 || part == this.Cell40 || part == this.Cell50 ||
                    part == this.Cell60 || part == this.Cell70 || part == this.Cell80 || part == this.Cell90 || part == this.Cell100 ||
                    part == this.Cell110 || part == this.Cell120 || part == this.Cell130 || part == this.Cell140 || part == this.Cell150 ||
                    part == this.Cell160 || part == this.Cell170 || part == this.Cell180 || part == this.Cell190 || part == this.Cell200)
                {
                    isValid = false;
                }
                else if (this.IsBlockRight(this.position))
                {
                    isValid = false;
                }
            }

            if (isValid)
            {
                List<string> cellNames = this.newPiece.MoveRight(this.position);

                this.position.Clear();

                var cell = (Rectangle)this.FindName(cellNames[0]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[1]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[2]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[3]);
                this.position.Add(cell);
            }
        }

        /// <summary>
        /// Method to move Block down.
        /// </summary>
        public void MoveDown()
        {
            bool isValid = true;

            foreach (Rectangle part in this.position)
            {
                if (part == this.Cell191 || part == this.Cell192 || part == this.Cell193 || part == this.Cell194 || part == this.Cell195 ||
                    part == this.Cell196 || part == this.Cell197 || part == this.Cell198 || part == this.Cell199 || part == this.Cell200)
                {
                    isValid = false;
                    this.isLocked = true;
                }
                else if (this.IsBlockBelow(this.position))
                {
                    isValid = false;
                    this.isLocked = true;
                }
            }

            if (isValid)
            {
                List<string> cellNames = this.newPiece.MoveDown(this.position);

                this.position.Clear();

                var cell = (Rectangle)this.FindName(cellNames[0]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[1]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[2]);
                this.position.Add(cell);
                cell = (Rectangle)this.FindName(cellNames[3]);
                this.position.Add(cell);
            }
        }

        /// <summary>
        /// Method to Move Column.
        /// </summary>
        /// <param name="colNum"></param>
        /// <param name="cell"></param>
        public void MoveToColumn(int colNum, Rectangle cell)
        {
            string cellName = cell.Name;
            int currColNum = int.Parse(cellName[cellName.Length - 1].ToString());

            if (currColNum == 0)
            {
                currColNum = 9;
            }
            else
            {
                currColNum--;
            }

            while (currColNum != colNum)
            {
                if (currColNum > colNum)
                {
                    this.MoveLeft();

                    currColNum--;
                }
                else if (currColNum < colNum)
                {
                    this.MoveRight();

                    currColNum++;
                }
            }
        }

        /// <summary>
        /// Method to check if row can drop.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool CanDrop(List<Rectangle> pos, int row)
        {
            bool canDrop = true;
            List<int> cols = new List<int>();

            string name = pos[0].Name;
            int col = int.Parse(name[name.Length - 1].ToString());
            if (col == 0)
                col = 9;
            else
                col--;
            cols.Add(col);

            name = pos[1].Name;
            col = int.Parse(name[name.Length - 1].ToString());
            if (col == 0)
                col = 9;
            else
                col--;
            cols.Add(col);

            name = pos[2].Name;
            col = int.Parse(name[name.Length - 1].ToString());
            if (col == 0)
                col = 9;
            else
                col--;
            cols.Add(col);

            name = pos[3].Name;
            col = int.Parse(name[name.Length - 1].ToString());
            if (col == 0)
                col = 9;
            else
                col--;
            cols.Add(col);

            foreach (int c in cols)
            {
                for (int r = row - 1; r >= 0; r--)
                {
                    if (this.boardState[r, c] == 1)
                    {
                        canDrop = false;
                    }
                }
            }

            return canDrop;
        }

        /// <summary>
        /// Method to increase speed of the blocks falling
        /// </summary>
        /// <param name="prevPos">increase speed</param>
        public void DrawAndVerifyPiece(List<Rectangle> prevPos)
        {
            this.newPiece.DrawPiece(prevPos, this.position);

            if (this.isLocked)
            {
                this.SetBoardState();

                int fullRows = this.NumberFullRows();

                for (int f = 1; f <= fullRows; f++)
                {
                    this.linesCleared++;

                    if (this.linesCleared != 0 && this.level < 9 && this.linesCleared % 10 == 0)
                    {
                        this.level++;
                        this.LevelTextBox.Content = this.level.ToString();

                        this.SpeedUpgrade();
                    }
                }

                switch (fullRows)
                {
                    case 0:
                        break;
                    case 1:
                        this.score += 100 + (int)(100 * this.scoreModifier);
                        break;                          
                    case 2:                             
                        this.score += 300 + (int)(300 * this.scoreModifier);
                        break;                          
                    case 3:                             
                        this.score += 500 + (int)(500 * this.scoreModifier);
                        break;                          
                    case 4:                             
                        this.score += 800 + (int)(800 * this.scoreModifier);
                        break;                          
                }

                while (this.FindFullRow() != 0)
                {
                    for (int cell = 0; cell < 10; cell++)
                    {
                        this.cells[this.FindFullRow() - 1, cell].Fill = Brushes.White;
                    }

                    for (int r = this.FindFullRow(); r > 2; r--)
                    {
                        for (int c = 0; c < 10; c++)
                        {
                            this.cells[r - 1, c].Fill = this.cells[r - 2, c].Fill;
                        }
                    }

                    this.SetBoardState();
                }

                if (!this.GameOver())
                {
                    this.NextBlock();
                }
            }
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        public void SetBoardState()
        {
            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (((SolidColorBrush)this.cells[row, col].Fill).Color == Color.FromRgb(255, 255, 255))
                    {
                        this.boardState[row, col] = 0;
                    }
                    else
                    {
                        this.boardState[row, col] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="sender">The first name to join.</param>
        /// <param name="e">The last name to join.</param>
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsClicked = true;

            this.Hide();
            this.timer.Stop();
            var win1 = new PauseWindow(this.AILevel);
            win1.ShowDialog();

            if (!win1.IsClicked && !win1.ResumeIsClicked)
            {
                App.Current.Shutdown();
            }

            if (win1.ResumeIsClicked)
            {
                this.timer.Start();
                this.Show();
            }
        }
    }
}
