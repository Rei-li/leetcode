using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonFreshPromotion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start1");
            var shoppingCart = new List<string> { "orange", "apple", "apple", "banana", "orange", "banana" };
            var codeList = new List<List<string>>();
            codeList.Add(new List<string> { "apple", "apple" });
            codeList.Add(new List<string> { "banana", "anything", "banana" });

            foreach (var prod in shoppingCart)
            {
                Console.WriteLine(prod);
            }

            foreach (var combo in codeList)
            {
                foreach (var prod in combo)
                {
                    Console.Write(prod + " ");
                }
                Console.WriteLine();
            }

            var result = CheckWinner(codeList, shoppingCart);

            Console.WriteLine();
            Console.WriteLine("Result: " + result);


            Console.WriteLine("Start2");
            shoppingCart = new List<string> { "banana", "orange", "banana", "apple", "apple" };
            codeList = new List<List<string>>();
            codeList.Add(new List<string> { "apple", "apple" });
            codeList.Add(new List<string> { "banana", "anything", "banana" });

            foreach (var prod in shoppingCart)
            {
                Console.WriteLine(prod);
            }

            foreach (var combo in codeList)
            {
                foreach (var prod in combo)
                {
                    Console.Write(prod + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Result: " + CheckWinner(codeList, shoppingCart));


            Console.WriteLine("Start3");
            shoppingCart = new List<string> { "apple", "banana", "apple", "banana", "orange", "banana" };
            codeList = new List<List<string>>();
            codeList.Add(new List<string> { "apple", "apple" });
            codeList.Add(new List<string> { "banana", "anything", "banana" });

            foreach (var prod in shoppingCart)
            {
                Console.WriteLine(prod);
            }

            foreach (var combo in codeList)
            {
                foreach (var prod in combo)
                {
                    Console.Write(prod + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Result: " + CheckWinner(codeList, shoppingCart));


            Console.WriteLine("Start4");
            shoppingCart = new List<string> { "apple", "apple", "apple", "banana" };
            codeList = new List<List<string>>();
            codeList.Add(new List<string> { "apple", "apple" });
            codeList.Add(new List<string> { "banana", "apple", "banana" });

            foreach (var prod in shoppingCart)
            {
                Console.WriteLine(prod);
            }

            foreach (var combo in codeList)
            {
                foreach (var prod in combo)
                {
                    Console.Write(prod + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Result: " + CheckWinner(codeList, shoppingCart));

            Console.ReadKey();
        }

        public static int CheckWinner(List<List<string>> codeList, IList<string> shoppingCart)
        {
            if (!codeList.Any())
            {
                return 1;
            }

            if (!shoppingCart.Any())
            {
                return 0;
            }

            int codeListCombinationPositin = 0;
            int codeListLevelPositin = 0;

            foreach (var prod in shoppingCart)
            {
                var isEqual = CheckIfProdFitPosition(prod, codeList[codeListLevelPositin][codeListCombinationPositin]);
                if (isEqual)
                {
                    var positions = MoveCodeListCounters(codeList, codeListCombinationPositin, codeListLevelPositin);
                    codeListCombinationPositin = positions.codeListCombinationPositin;
                    codeListLevelPositin = positions.codeListLevelPositin;

                    if (codeListLevelPositin >= codeList.Count)
                    {
                        break;
                    }
                }
                else
                {
                    codeListCombinationPositin = 0;
                    if (CheckIfProdFitPosition(prod, codeList[codeListLevelPositin][codeListCombinationPositin]))
                    {
                        var positions = MoveCodeListCounters(codeList, codeListCombinationPositin, codeListLevelPositin);
                        codeListCombinationPositin = positions.codeListCombinationPositin;
                        codeListLevelPositin = positions.codeListLevelPositin;
                    }

                }
            }

            if (codeListLevelPositin + 1 == codeList.Count && codeListCombinationPositin + 1 == codeList[codeListLevelPositin].Count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static bool CheckIfProdFitPosition(string prod, string codeListProd)
        {
            if (string.Equals(codeListProd, "anything"))
            {
                return true;
            }

            return string.Equals(prod, codeListProd);
        }

        public static (int codeListCombinationPositin, int codeListLevelPositin) MoveCodeListCounters(
            List<List<string>> codeList,
            int codeListCombinationPositin,
            int codeListLevelPositin)
        {
            if (codeListCombinationPositin + 1 < codeList[codeListLevelPositin].Count)
            {
                codeListCombinationPositin++;
            }
            else if (codeListLevelPositin + 1 < codeList.Count)
            {
                codeListCombinationPositin = 0;
                codeListLevelPositin++;

            }

            return (codeListCombinationPositin, codeListLevelPositin);
        }
    }
}
