using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using SharpStructure;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            WordDictionary WordDictionary = new WordDictionary();
/*            WordDictionary.AddToDictionary("Dog");
            WordDictionary.AddToDictionary("Cat");
            WordDictionary.AddToDictionary("Can");
            WordDictionary.AddToDictionary("Ant");
            WordDictionary.AddToDictionary("Anti");
            WordDictionary.AddToDictionary("And");

            

            WordDictionary.SaveLocal(Path.Combine(Directory.GetCurrentDirectory(), "Dictionary.json"));*/


            WordDictionary.LoadLocal(Path.Combine(Directory.GetCurrentDirectory(), "Dictionary.json"));
            List<string> data = WordDictionary.GetByPrefix("An");
            Console.ReadLine();
        }
    }
}
