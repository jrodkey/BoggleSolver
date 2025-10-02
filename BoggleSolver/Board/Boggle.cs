using System.Text.RegularExpressions;
using BoggleSolver.Board.TrieStructure;

namespace BoggleSolver.Board
{
    /// <summary>
    /// Defines the Boggle type and has a GameLogistics object that is
    /// used during gameplay to interact with the trie structure.
    /// </summary>
    public class Boggle
    {
        private Trie m_trie;
        private HashSet<string> m_resultList;
        private GameLogistics m_gameLogistics;

        public Boggle()
        {
            m_gameLogistics = new GameLogistics();
            m_trie = new Trie();
            m_resultList = new HashSet<string>();
        }

        /// <summary>
        /// Prior to solving a board, configure legal words.
        /// </summary>
        /// <param name="allWords">Legal words, alphabetically-sorted</param>
        public void SetLegalWords(IEnumerable<string> allWords)
        {
            // Rules: ==========================================
            // Must not have capitalizations or hyphenated words.
            // Must not contain duplicates.
            // Must have at least 3 letters.
            foreach(string word in allWords)
            {
                bool valid = true;
                int letterCount = 0;
                Regex regex = new Regex("^[A-Z]+$");
                if (regex.IsMatch(word))
                {
                    continue;
                }

                HashSet<char> validLetters = new HashSet<char>();
                for(int i = 0; i < word.Length; ++i)
                {
                    if (word[i] == '-' || word[i] == '_')
                    {
                        valid = false;
                        break;
                    }

                    if (validLetters.Contains(word[i]))
                    {
                        valid = false;
                        break;
                    }

                    validLetters.Add(word[i]);
                    letterCount++;
                }

                if (letterCount < 3)
                {
                    valid = false;
                }

                if (!valid)
                {
                    continue;
                }

                m_trie.AddWord(word);
            }
        }

        /// <summary>
        /// Find all words on the specified board.
        /// </summary>
        /// <param name="boardWidth">Width of the board</param>
        /// <param name="boardHeight">Height of the board</param>
        /// <param name="boardLetters">Board width*height characters in row major order</param>
        /// <returns>List of words that exist.</returns>
        public IEnumerable<string> SolveBoard(int boardWidth, int boardHeight, string boardLetters)
        {
            boardLetters = m_gameLogistics.VerifyBoardPopulation(boardWidth, boardHeight, boardLetters);
            m_gameLogistics.ResetGameboard(boardWidth, boardHeight);

            // Create gameboard which is a two-dimensional array filled with boardLetters that
            // exist as a string in row major order.
            int letterIterator = 0;
            for (int row = 0; row < boardHeight; ++row)
            {
                for (int column = 0; column < boardWidth; ++column)
                {
                    m_gameLogistics.GameBoard[row, column] = new GameSpace();
                    m_gameLogistics.GameBoard[row, column].Letter = boardLetters[letterIterator++];
                }
            }

            m_resultList.Clear();
            TrieNode root = m_trie.Root;
            string word = "";
            for (int row = 0; row < boardHeight; ++row)
            {
                for (int column = 0; column < boardWidth; ++column)
                {
                    // Retrieve the node that correlates to the gameboard's position, if it exists,
                    // then traverse.
                    TrieNode node = root.GetNode(m_gameLogistics.GameBoard[row, column].Letter);
                    if (node != null)
                    {
                        word = word + m_gameLogistics.GameBoard[row, column].Letter;
                        TraverseNode(node, row, column, word);
                        word = "";
                    }
                }
            }

            return m_resultList;
        }

        /// <summary>
        /// Recursively traverses from the row and column specified, in all eight directions, for any
        /// word from our valid dictionary.
        /// </summary>
        /// <param name="root">Node to traverse</param>
        /// <param name="row">Target row</param>
        /// <param name="column">Target column</param>
        /// <param name="word">Current state of word</param>
        private void TraverseNode(TrieNode root, int row, int column, string word)
        {
            // If we have found that the node has been marked as terminal, add the state of the word.
            if (root.IsWordComplete)
            {
                m_resultList.Add(word);
            }

            // Check the bounds of the gameboard and that we haven't visited this cell.
            if (m_gameLogistics.BoundsCheck(row, column) && !m_gameLogistics.HasVisited(row, column))
            {
                m_gameLogistics.GameBoard[row, column].Visited = true;

                // For every edge that exists, check the letter of the node and each
                // one of its adjacents. 
                for (int i = 0; i < TrieNode.MAX_LETTERS; ++i)
                {
                    char nextLetter = (char)(i + 'a');
                    TrieNode nextNode = root.GetNode(nextLetter);
                    if (nextNode != null)
                    {
                        // Recursive Search Pattern:
                        // (-1,-1), (-1, 0), (-1,+1), 
                        //        \\   ||   //
                        // ( 0,-1),-( 0, 0),-( 0,+1),
                        //        //   ||   \\
                        // (+1,-1), (+1, 0), (+1,+1)
                        TryTraverseNode(nextNode, row - 1, column - 1, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row - 1, column, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row - 1, column + 1, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row, column - 1, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row, column, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row, column + 1, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row + 1, column - 1, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row + 1, column, nextLetter, word + nextLetter);
                        TryTraverseNode(nextNode, row + 1, column + 1, nextLetter, word + nextLetter);
                    }
                }

                m_gameLogistics.GameBoard[row, column].Visited = false;
            }
        }

        /// <summary>
        /// Wraps the traversal from the node with safe guard checks.
        /// </summary>
        /// <param name="node">Node to traverse</param>
        /// <param name="row">Target row</param>
        /// <param name="column">Target column</param>
        /// <param name="letter">Next letter of the word</param>
        /// <param name="word">Current state of word</param>
        private void TryTraverseNode(TrieNode node, int row, int column, char letter, string word)
        {
            if (m_gameLogistics.BoundsCheck(row, column) &&
                !m_gameLogistics.HasVisited(row, column) &&
                m_gameLogistics.GameBoard[row, column].Letter == letter)
            {
                TraverseNode(node, row, column, word);
            }
        }
    }
}