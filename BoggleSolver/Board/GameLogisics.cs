using System;

namespace BoggleSolver.Board
{
    /// <summary>
    /// Defines the gameboard, along with its dimensions, and gives bounds checking
    /// around the board and its game spaces.
    /// </summary>
    public class GameLogistics
    {
        public GameLogistics()
        {
            BoardWidth = 0;
            BoardHeight = 0;
            GameBoard = new GameSpace[0,0];
        }

        public int BoardWidth { get; set; }

        public int BoardHeight { get; set; }

        public GameSpace[,] GameBoard { get; set; }

        /// <summary>
        /// Checks that the row and column are within the bounds 
        /// of the game board.
        /// </summary>
        /// <param name="row">Row position</param>
        /// <param name="column">Column position</param>
        /// <returns>True, if within bounds, otherwise false.</returns>
        public bool BoundsCheck(int row, int column)
        {
            return row >= 0 && row < BoardHeight &&
                column >= 0 && column < BoardWidth;
        }

        /// <summary>
        /// Checks the visited flag at the row and column position.
        /// </summary>
        /// <param name="row">Row position</param>
        /// <param name="column">Column position</param>
        /// <returns></returns>
        public bool HasVisited(int row, int column)
        {
            return GameBoard[row, column].Visited;
        }

        /// <summary>
        /// Checks that the letters string contains enough to 
        /// populate the board and, if not, fills or subtracts
        /// letters and returns a new string.
        /// </summary>
        /// <param name="boardLetters">String that contains the input for the board</param>
        /// <returns>Returns a string that contains the verified board input</returns>
        public string VerifyBoardPopulation(int boardWidth, int boardHeight, string boardLetters)
        {
            int boardLength = boardWidth * boardHeight;
            if (boardLetters.Length == boardLength || boardLetters.Length > boardLength)
            {
                return boardLetters;
            }

            int diff = boardLength - boardLetters.Length;
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < diff; ++i)
            {
                boardLetters += (char)(random.Next(97, 122));
            }

            return boardLetters;
        }

        /// <summary>
        /// Checks the letter at the row and column position is equal to
        /// the letter that is given.
        /// </summary>
        /// <param name="row">Row position</param>
        /// <param name="column">Column position</param>
        /// <param name="letter">Target character</param>
        /// <returns>True, if equivalent</returns>
        public bool GameBoardHasLetter(int row, int column, char letter)
        {
            return letter == GameBoard[row, column].Letter;
        }

        /// <summary>
        /// Clears all logistics, then initializes.
        /// </summary>
        /// <param name="width">Width for board</param>
        /// <param name="height">Height for board</param>
        public void ResetGameboard(int width, int height)
        {
            BoardWidth = width;
            BoardHeight = height;
            GameBoard = new GameSpace[height, width];
        }
    }

    /// <summary>
    /// Defines a particular space on the game board.
    /// </summary>
    public class GameSpace
    {
        public char Letter { get; set; } = '\0';
        public bool Visited { get; set; } = false;
    }
}