using System;
using System.Collections.Generic;
using System.Text;

namespace SharpStructure
{
    internal class WordNode
    {
        public WordNode Parent { get; set; }
        public List<WordNode> ChildNodes { get; set; }


        private bool IsWord { get; set; }
        public char Character { get; set; }


        public WordNode(string characters)
        {
            ChildNodes = new List<WordNode>();
            Initialize(characters);
        }


        public void Initialize(string characters)
        {
            if (string.IsNullOrEmpty(characters)) return;
            var firstChar = characters[0];

            var node = ChildNodes.Find(r => r.Character == firstChar);
            if (node == null)
            {
                var newChild = new WordNode(characters.Remove(0, 1)) {Character = firstChar};
                if (characters.Length == 1)
                {
                    newChild.IsWord = true;
                    newChild.Parent = this;
                }

                ChildNodes.Add(newChild);
            }
            else
            {
                node.Initialize(characters.Remove(0, 1));
            }
        }

        public bool IsValidWord(string word)
        {
            if (word.Length == 1) return Character == word[0] && IsWord;

            var nextChar = word[1];
            var node = ChildNodes.Find(r => r.Character == nextChar);
            return node != null && node.IsValidWord(word.Remove(0, 1));
        }

        public List<string> GetWordsByPrefix(string prefix)
        {
            List<string> words=new List<string>();
            WordNode lastNode = GetLastNodeOfWord(prefix);
            foreach (WordNode nodeChild in lastNode.ChildNodes)
            {
                //TODO Finish implementation
            }

            return words;
        }

        private WordNode GetLastNodeOfWord(string word)
        {
            if (word.Length == 1)
            {
                return this;
            }

            var node = ChildNodes.Find(r => r.Character == word[0]);
            return node.GetLastNodeOfWord(word.Remove(0, 1));
        }


    }

    public class WordDictionary
    {
        private WordNode Root { get; }

        public WordDictionary()
        {
            Root = new WordNode("");
        }

        public void AddToDictionary(string word)
        {
            Root.Initialize(word.ToUpper());
        }

        public bool IsValidWord(string word)
        {
            word = word.ToUpper();
            var wordNode = Root.ChildNodes.Find(r => r.Character == word[0]);
            return wordNode != null && wordNode.IsValidWord(word);
        }

        

    }
}
