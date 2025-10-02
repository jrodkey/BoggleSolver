
namespace BoggleSolver.Board.TrieStructure
{
    /// <summary>
    /// Defines a node that is used by the Trie structure and 
    /// has a dictionary of all 26 edges.
    /// </summary>
    public class TrieNode
    {
        public static int MAX_LETTERS = 26;
        private char m_letter;
        private bool m_terminal;
        private Dictionary<char, TrieNode> m_edges;

        public TrieNode()
        {
            m_edges = new Dictionary<char, TrieNode>(MAX_LETTERS);
            m_letter = '\0';
        }

        /// <summary>
        /// True, if the node is marked as terminal or there isn't any edges.
        /// </summary>
        public bool IsWordComplete { get { return m_terminal || m_edges.Count == 0; } }

        public char Letter 
        {
            get { return m_letter; }
            set { m_letter = value; }
        }

        public void AddNode(char letter, TrieNode node)
        {
            m_edges.Add(letter, node);
        }

        public void SetTerminal(bool isTerminal)
        {
            m_terminal = isTerminal;
        }

        public TrieNode GetNode(char letter) 
        {
            if (m_edges.ContainsKey(letter))
            {
                return m_edges[letter];
            }

            return null;
        }
    }
}