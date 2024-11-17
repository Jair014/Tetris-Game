//-----------------------------------------------------------------------
// <copyright file="Piece.cs" company="CompanyName">
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
    /// Interaction logic for Piece
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// Creating field.
        /// </summary>
        private int pieceNum;

        /// <summary>
        /// Creating field.
        /// </summary>
        private int rotationNum;

        /// <summary>
        /// Initializes a new instance of the <see cref="Piece" /> class.
        /// </summary>
        /// <param name="pieceNum">The first name to join.</param>
        public Piece(int pieceNum)
        {
            this.rotationNum = 1;

            this.pieceNum = pieceNum;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="previousPosition">The first name to join.</param>
        /// <param name="currentPosition">The last name to join.</param>
        public void DrawPiece(List<Rectangle> previousPosition, List<Rectangle> currentPosition)
        {
            var lightBlueBrush = new SolidColorBrush(Color.FromRgb(0, 255, 239));
            var yellowBrush = new SolidColorBrush(Color.FromRgb(239, 255, 0));
            var redBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            var greenBrush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            var orangeBrush = new SolidColorBrush(Color.FromRgb(255, 162, 0));
            var blueBrush = new SolidColorBrush(Color.FromRgb(0, 0, 255));
            var pinkBrush = new SolidColorBrush(Color.FromRgb(255, 132, 244));
            var whiteBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            foreach (Rectangle cell in previousPosition)
            {
                cell.Fill = whiteBrush;
            }
            
            switch (this.pieceNum)
                {
                    case 0:
                        foreach (Rectangle cell in currentPosition)
                        {
                            cell.Fill = lightBlueBrush;
                        }

                        break;
                    case 1:
                        foreach (Rectangle cell in currentPosition)
                        {
                            cell.Fill = yellowBrush;
                        }

                        break;
                    case 2:
                        foreach (Rectangle cell in currentPosition)
                        {
                            cell.Fill = redBrush;
                        }

                        break;
                    case 3:
                        foreach (Rectangle cell in currentPosition)
                        {
                            cell.Fill = greenBrush;
                        }

                        break;
                    case 4:
                        foreach (Rectangle cell in currentPosition)
                        {
                            cell.Fill = orangeBrush;
                        }

                        break;
                    case 5:
                        foreach (Rectangle cell in currentPosition)
                        {
                            cell.Fill = blueBrush;
                        }

                        break;
                    case 6:
                        foreach (Rectangle cell in currentPosition)
                        {
                            cell.Fill = pinkBrush;
                        }

                        break;
            }
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public List<string> MoveDown(List<Rectangle> pos)
        {
            List<int> cellNums = this.GetCellNums(pos);

            cellNums[0] += 10;
            cellNums[1] += 10;
            cellNums[2] += 10;
            cellNums[3] += 10;

            List<string> result = this.GetNewCellNames(cellNums);

            return result;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public List<string> MoveLeft(List<Rectangle> pos)
        {
            List<int> cellNums = this.GetCellNums(pos);

            cellNums[0]--;
            cellNums[1]--;
            cellNums[2]--;
            cellNums[3]--;

            List<string> result = this.GetNewCellNames(cellNums);

            return result;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public List<string> MoveRight(List<Rectangle> pos)
        {
            List<int> cellNums = this.GetCellNums(pos);

            cellNums[0]++;
            cellNums[1]++;
            cellNums[2]++;
            cellNums[3]++;

            List<string> result = this.GetNewCellNames(cellNums);

            return result;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public List<string> Rotate(List<Rectangle> pos)
        {
            List<string> result = new List<string>();
            List<int> cellNums = new List<int>();

            switch (this.pieceNum)
            {
                case 0:
                    if (this.rotationNum % 4 == 1)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[0] % 10 == 1 || cellNums[0] % 10 == 0 || cellNums[0] % 10 == 9)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 12;
                        cellNums[1] += 1;
                        cellNums[2] -= 10;
                        cellNums[3] -= 21;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 2)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[0] % 180 <= 10 || cellNums[0] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 19;
                        cellNums[1] += 10;
                        cellNums[2] += 1;
                        cellNums[3] -= 8;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 3)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[0] % 10 == 1 || cellNums[0] % 10 == 0 || cellNums[0] % 10 == 9)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 12;
                        cellNums[1] -= 1;
                        cellNums[2] += 10;
                        cellNums[3] += 21;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 0)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[0] % 180 <= 10 || cellNums[0] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 19;
                        cellNums[1] -= 10;
                        cellNums[2] -= 1;
                        cellNums[3] += 8;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }

                    break;
                case 1:
                    result.Add(pos[0].Name);
                    result.Add(pos[1].Name);
                    result.Add(pos[2].Name);
                    result.Add(pos[3].Name);

                    return result;
                case 2:
                    if (this.rotationNum % 4 == 1)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[2] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 9;
                        cellNums[1] += 0;
                        cellNums[2] -= 11;
                        cellNums[3] -= 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 2)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 0)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 11;
                        cellNums[1] += 0;
                        cellNums[2] -= 9;
                        cellNums[3] += 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 3)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 9;
                        cellNums[1] += 0;
                        cellNums[2] += 11;
                        cellNums[3] += 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 0)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 1)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 11;
                        cellNums[1] += 0;
                        cellNums[2] += 9;
                        cellNums[3] -= 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }

                    break;
                case 3:
                    if (this.rotationNum % 4 == 1)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[2] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 9;
                        cellNums[1] += 0;
                        cellNums[2] -= 11;
                        cellNums[3] -= 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 2)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 0)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 11;
                        cellNums[1] += 0;
                        cellNums[2] -= 9;
                        cellNums[3] -= 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 3)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 9;
                        cellNums[1] += 0;
                        cellNums[2] += 11;
                        cellNums[3] += 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 0)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 1)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 11;
                        cellNums[1] += 0;
                        cellNums[2] += 9;
                        cellNums[3] += 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }

                    break;
                case 4:
                    if (this.rotationNum % 4 == 1)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 1)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 11;
                        cellNums[1] += 0;
                        cellNums[2] -= 11;
                        cellNums[3] -= 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 2)
                    {
                        cellNums = this.GetCellNums(pos);

                        cellNums[0] += 9;
                        cellNums[1] += 0;
                        cellNums[2] -= 9;
                        cellNums[3] -= 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 3)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 0)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 11;
                        cellNums[1] += 0;
                        cellNums[2] += 11;
                        cellNums[3] += 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 0)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 9;
                        cellNums[1] += 0;
                        cellNums[2] += 9;
                        cellNums[3] += 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }

                    break;
                case 5:
                    if (this.rotationNum % 4 == 1)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 0)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 11;
                        cellNums[1] += 0;
                        cellNums[2] -= 11;
                        cellNums[3] -= 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 2)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 9;
                        cellNums[1] += 0;
                        cellNums[2] -= 9;
                        cellNums[3] += 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 3)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 1)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 11;
                        cellNums[1] += 0;
                        cellNums[2] += 11;
                        cellNums[3] += 20;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 0)
                    {
                        cellNums = this.GetCellNums(pos);

                        cellNums[0] -= 9;
                        cellNums[1] += 0;
                        cellNums[2] += 9;
                        cellNums[3] -= 2;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }

                    break;
                case 6:
                    if (this.rotationNum % 4 == 1)
                    {
                        cellNums = this.GetCellNums(pos);

                        cellNums[0] -= 9;
                        cellNums[1] += 0;
                        cellNums[2] += 9;
                        cellNums[3] -= 11;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 2)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 0)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 11;
                        cellNums[1] += 0;
                        cellNums[2] -= 11;
                        cellNums[3] -= 9;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 3)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 190 <= 10)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] += 9;
                        cellNums[1] += 0;
                        cellNums[2] -= 9;
                        cellNums[3] += 11;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }
                    else if (this.rotationNum % 4 == 0)
                    {
                        cellNums = this.GetCellNums(pos);

                        if (cellNums[1] % 10 == 1)
                        {
                            result.Add(pos[0].Name);
                            result.Add(pos[1].Name);
                            result.Add(pos[2].Name);
                            result.Add(pos[3].Name);

                            return result;
                        }

                        cellNums[0] -= 11;
                        cellNums[1] += 0;
                        cellNums[2] += 11;
                        cellNums[3] += 9;

                        result = this.GetNewCellNames(cellNums);

                        this.rotationNum++;

                        return result;
                    }

                    break;
            }

            return result;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="shouldRotate">The first name to join.</param>
        public void ChangeRotation(bool shouldRotate)
        {
            if (!shouldRotate)
            {
                this.rotationNum -= 1;
            }
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="pos">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public List<int> GetCellNums(List<Rectangle> pos)
        {
            List<int> result = new List<int>();

            string blockName = pos[0].Name;
            blockName = blockName.Remove(0, 4);
            int cellNum = int.Parse(blockName);
            result.Add(cellNum);

            blockName = pos[1].Name;
            blockName = blockName.Remove(0, 4);
            cellNum = int.Parse(blockName);
            result.Add(cellNum);

            blockName = pos[2].Name;
            blockName = blockName.Remove(0, 4);
            cellNum = int.Parse(blockName);
            result.Add(cellNum);

            blockName = pos[3].Name;
            blockName = blockName.Remove(0, 4);
            cellNum = int.Parse(blockName);
            result.Add(cellNum);

            return result;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="cellNums">The first name to join.</param>
        /// <returns>The joined names.</returns>
        public List<string> GetNewCellNames(List<int> cellNums)
        {
            List<string> cellNames = new List<string>();

            string blockName = "Cell" + cellNums[0].ToString();
            cellNames.Add(blockName);
            blockName = "Cell" + cellNums[1].ToString();
            cellNames.Add(blockName);
            blockName = "Cell" + cellNums[2].ToString();
            cellNames.Add(blockName);
            blockName = "Cell" + cellNums[3].ToString();
            cellNames.Add(blockName);

            return cellNames;
        }
    }
}
