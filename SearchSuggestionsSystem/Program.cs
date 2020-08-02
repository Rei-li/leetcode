using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchSuggestionsSystem
{
    /// <summary>
    /// leetcode https://leetcode.com/problems/search-suggestions-system/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var s = new Solution();
            var products = new string[1] { "havana" };
            var results = s.SuggestedProducts(products, "tatiana");

            Console.WriteLine("Results:");
            Console.WriteLine(results.ToString());
            Console.ReadLine();
        }

    }

    public class Solution
    {
        public class Node
        {
            public Dictionary<char, Node> dict = new Dictionary<char, Node>();
            public string word;
            public LinkedList<char> orderedKeys = new LinkedList<char>();
        }

        public void AddBrunch(string word, Node node)
        {
            var lastNode = node;

            foreach (char letter in word)
            {
                lastNode = AddNode(letter, lastNode);
            }

            lastNode.word = word;
        }

        public Node AddNode(char letter, Node node)
        {
            var nextLevelNode = new Node();
            if (!node.orderedKeys.Contains(letter))
            {
                node.dict.Add(letter, nextLevelNode);

                if (node.orderedKeys.Any())
                {
                    var lastKey = node.orderedKeys.Last;
                    if (lastKey.Value < letter)
                    {
                        node.orderedKeys.AddAfter(lastKey, letter);
                    }
                    else
                    {
                        foreach (var key in node.orderedKeys)
                        {
                            if (letter < key)
                            {
                                node.orderedKeys.AddBefore(node.orderedKeys.Find(key), letter);
                                break;
                            }
                        }
                    }

                }
                else
                {
                    node.orderedKeys.AddFirst(letter);
                }
            }
            else
            {
                node.dict.TryGetValue(letter, out nextLevelNode);
            }

            return nextLevelNode;
        }

        public Node BuildIndex(string[] products)
        {
            var root = new Node();
            foreach (var prod in products)
            {
                AddBrunch(prod, root);
            }

            return root;
        }

        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            Node selectedNode = BuildIndex(products);
            var results = new List<IList<string>>();

            foreach (var letter in searchWord)
            {
                List<string> letterResults = new List<string>();
                if (selectedNode != null)
                {
                    selectedNode.dict.TryGetValue(letter, out selectedNode);

                    if (selectedNode != null)
                    {
                        LetterSearch(selectedNode, letterResults);

                    }
                }
                results.Add(letterResults);
            }

            return results;
        }

        public void LetterSearch(Node node, IList<string> letterResults)
        {
            if (node.word != null)
            {
                letterResults.Add(node.word);
            }

            if (node.orderedKeys.Any())
            {
                foreach (var key in node.orderedKeys)
                {
                    if (letterResults.Count >= 3)
                    {
                        break;
                    }

                    var selectedNode = new Node();
                    node.dict.TryGetValue(key, out selectedNode);
                    LetterSearch(selectedNode, letterResults);
                }
            }
        }
    }
}
