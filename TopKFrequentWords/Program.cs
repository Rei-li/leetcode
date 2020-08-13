using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopKFrequentWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var solution = new Solution();
            var results = solution.TopKFrequent(new string[] { "i", "love", "leetcode", "i", "love", "coding" }, 2);
            foreach (var word in results)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("Start2");
            var results2 = solution.TopKFrequent(new string[] { "the", "day", "is", "sunny", "the", "the", "the", "sunny", "is", "is" }, 4);
            foreach (var word in results2)
            {
                Console.WriteLine(word);
            }


            Console.ReadKey();
        }         
    }

    public class Solution
    {
        public IList<string> TopKFrequent(string[] words, int k)
        {
            Dictionary<string, int> countedWords = CountWordsEntries(words);
            List<KeyValuePair<string, int>>[] bucketsList = ThrowToBucket(countedWords, words);

            var results = new List<string>();

            for(int i = bucketsList.Count()-1; i >= 0; i--)
            {
                if (results.Count >= k)
                {
                    break;
                }

                var bucket = bucketsList[i];
                if (bucket != null && bucket.Any())
                {
                    var orderedBucket = bucket.OrderBy(obj => obj.Key);
                    foreach(var pair in orderedBucket)
                    {
                        results.Add(pair.Key);
                        if (results.Count >= k)
                        {
                            break;
                        }
                    }
                }
            }

            return results;

        }

        public Dictionary<string, int> CountWordsEntries(string[] words)
        {
            var countedWords = new Dictionary<string, int>();

            foreach(var word in words)
            {
                int wordCount;
                if (countedWords.TryGetValue(word, out wordCount))
                {
                    wordCount++;
                    countedWords[word] = wordCount;
                }
                else
                {
                    countedWords.Add(word, 1);
                }
            }

            return countedWords;
        }

        public List<KeyValuePair<string, int>>[] ThrowToBucket(Dictionary<string, int> countedWords, string[] words)
        {
            var bucketsList = new List<KeyValuePair<string, int>>[words.Count()];

            foreach(var pair in countedWords)
            {
                var index = pair.Value;

                if (bucketsList[index] == null)
                {
                    var bucket = new List<KeyValuePair<string, int>>();
                    bucket.Add(pair);
                    bucketsList[index] = bucket;
                }
                else 
                {
                    bucketsList[index].Add(pair);
                }               
            }

            return bucketsList;
        }
    }
}
