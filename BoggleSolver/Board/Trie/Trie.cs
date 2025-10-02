using System.Collections;

namespace BoggleSolver.Board.TrieStructure
{
    /// <summary>
    /// Defines a tree-like structure that stores words in memory, then 
    /// quickly retieves them.
    /// </summary>
    public class Trie
    {
        private TrieNode m_root;

        public Trie()
        {
            m_root = new TrieNode();
        }

        public TrieNode Root { get { return m_root; }}

        /// <summary>
        /// Adds the the words to the collection.
        /// </summary>
        /// <param name="words">Enumeration of strings</param>
        public void AddWords(IEnumerable[] words)
        {
            foreach (string word in words)
            {
                AddWord(word);
            }
        }

        /// <summary>
        /// Adds the word to all of its correlated TrieNodes.
        /// </summary>
        /// <param name="word">add word</param>
        public void AddWord(string word)
        {
            TrieNode node = m_root;
            for (int l = 0; l < word.Length; ++ l)
            {
                // If a node doesn't exists for this letter, create one, add it 
                // and store the node.
                char letter = word[l];
                TrieNode nextNode = node.GetNode(letter);
                if (nextNode == null)
                {
                    nextNode = new TrieNode();
                    nextNode.Letter = letter;
                    node.AddNode(letter, nextNode);
                }

                node = nextNode;
            }

            node.SetTerminal(true);
        }
    }
}