using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace SharpStructure
{
    internal class WordNode
    {
        public WordNode Parent { get; set; }
        public List<WordNode> ChildNodes { get; set; }


        public bool IsWord { get; set; }
        public char Character { get; set; }


        public WordNode()
        {
            ChildNodes=new List<WordNode>();
        }

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
            //TODO Memory Profiling
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

                if (!node.IsWord) continue;
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
        private WordNode Root { get; set; }

        public WordDictionary()
        {
            Root = new WordNode("");
        }
       
        /// <summary>
        /// Adds new words to dictionary
        /// </summary>
        /// <param name="word"></param>
        public void AddToDictionary(string word)
        {
            Root.Initialize(Root,word.ToUpper());
        }

        /// <summary>
        /// Validates word from dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Boolean</returns>
        public bool IsValidWord(string word)
        {
            word = word.ToUpper();
            var wordNode = Root.ChildNodes.Find(r => r.Character == word[0]);
            return wordNode != null && wordNode.IsValidWord(word);
        }

        /// <summary>
        /// Search the dictionary with prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns>All words from dictionary matching prefix in list</returns>
        public List<string> GetByPrefix(string prefix)
        {
            return Root.GetWordsByPrefix(prefix.ToUpper());
        }

        /// <summary>
        /// Saves File as JSON
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>True if success otherwise false</returns>
        public bool SaveLocal(string filePath)
        {
            try
            {

               string json= JsonConvert.SerializeObject(Root, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Reads and loads JSON File
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>True if success otherwise false</returns>
        public bool LoadLocal(string filePath)
        {
            try
            {
                string data=File.ReadAllText(filePath);
                Root = JsonConvert.DeserializeObject<WordNode>(data,new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
