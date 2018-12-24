using NUnit.Framework;
using SharpStructure;

namespace Tests
{
    public class Tests
    {
        private WordDictionary WordDictionary { get; set; }

        [SetUp]
        public void Setup()
        {
            WordDictionary = new WordDictionary();
            WordDictionary.AddToDictionary("Dog");
            WordDictionary.AddToDictionary("Cat");
            WordDictionary.AddToDictionary("Can");
            WordDictionary.AddToDictionary("Ant");
            WordDictionary.AddToDictionary("Anti");
        }

        [Test]
        public void Test1()
        {
            Assert.IsFalse(WordDictionary.IsValidWord("doge"));
        }

        [Test]
        public void Test2()
        {
            Assert.IsFalse(WordDictionary.IsValidWord("don"));
        }

        [Test]
        public void Test3()
        {
            Assert.IsTrue(WordDictionary.IsValidWord("dog"));
        }

        [Test]
        public void Test4()
        {
            Assert.IsTrue(WordDictionary.IsValidWord("anti"));
        }

        [Test]
        public void Test5()
        {
            Assert.IsTrue(WordDictionary.IsValidWord("ANTI"));
        }

        [Test]
        public void Test6()
        {
            Assert.IsTrue(WordDictionary.IsValidWord("CaT"));

        }
    }
}