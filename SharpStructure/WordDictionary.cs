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
            Initialize(this,characters);
        }


        public void Initialize(WordNode parentNode,string characters)
        {
            if (string.IsNullOrEmpty(characters)) return;
            var firstChar = characters[0];

            var node = ChildNodes.Find(r => r.Character == firstChar);
            if (node == null)
            {
                var newChild = new WordNode(characters.Remove(0, 1)) {Character = firstChar};
                newChild.Parent = parentNode;
                if (characters.Length == 1)
                {
                    newChild.IsWord = true;
                    
                }
                
                ChildNodes.Add(newChild);
            }
            else
            {
                node.Initialize(node,characters.Remove(0, 1));
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
            WordNode lastNode = GetLastNodeOfWord(this,prefix);
            Stack<WordNode> nodes=new Stack<WordNode>();
            nodes.Push(lastNode);

            while (nodes.Count>0)
            {
                WordNode node = nodes.Pop();
                foreach (WordNode childNode in node.ChildNodes)
                {
                    nodes.Push(childNode);
                }

                if (node.IsWord)
                {
                    string word = "";
                    Stack<WordNode> wordsStack = new Stack<WordNode>();
                    while (node.Parent!=null)
                    {
                   
                        wordsStack.Push(node);
                        node = node.Parent;
                    }

                    while (wordsStack.Count>0)
                    {
                        word += wordsStack.Pop().Character;
                    }
                    words.Add(word);
                }

                
            }

            return words;
        }

        private WordNode GetLastNodeOfWord(WordNode targetNode,string word)
        {
            if (word.Length == 0)
            {
                return targetNode;
            }

            targetNode = targetNode.ChildNodes.Find(r => r.Character == word[0]);
            return targetNode.GetLastNodeOfWord(targetNode,word.Remove(0, 1));
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
            Root.Initialize(Root,word.ToUpper());
        }

        public bool IsValidWord(string word)
        {
            word = word.ToUpper();
            var wordNode = Root.ChildNodes.Find(r => r.Character == word[0]);
            return wordNode != null && wordNode.IsValidWord(word);
        }

        public List<string> GetByPrefix(string prefix)
        {
            return Root.GetWordsByPrefix(prefix.ToUpper());
        }

    }
}
