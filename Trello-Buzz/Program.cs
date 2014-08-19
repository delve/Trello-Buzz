using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trello_Buzz
{
    class Program
    {
        const string FIZZLETTERS = "acdegilmnoprstuw";

        static void Main(string[] args)
        {
            Int64 targetHash = 956446786872726;
            string candidate;
            //verify hashing function matches requirements
            Console.WriteLine("Hash of leepadg is: {0}", hashBuzz("leepadg"));
            Console.WriteLine("Expected hash was-: 680131659347 -> {0}", hashBuzz("leepadg") == 680131659347 ? "Matched" : "Failed!");
            //verifying my math dissection from math-notes.xlsx using some easily-SUM()-ed values
            Console.WriteLine("Hash of wwwwwwwww is: {0}", hashBuzz("wwwwwwwww"));
            Console.WriteLine("Expected hash was---: 963882903480154 -> {0}", hashBuzz("wwwwwwwww") == 963882903480154 ? "Matched" : "Failed!");
            Console.WriteLine("Hash of wwwwwwwwa is: {0}", hashBuzz("wwwwwwwwa"));
            Console.WriteLine("Expected hash was---: 963882903480139 -> {0}", hashBuzz("wwwwwwwwa") == 963882903480139 ? "Matched" : "Failed!");
            //the work
            Console.WriteLine("================================================");
            Console.WriteLine("Final hash value desired is {0}", targetHash);

            if (crackHash(targetHash, out candidate)) 
            { 
                Console.WriteLine("Match found"); 
                Console.WriteLine("Candidate string '{0}'; length: {1}; hash value: {2}", candidate, candidate.Length, hashBuzz(candidate));
                Console.WriteLine("                                  Expected hash was: {0}", targetHash);
            }
            else
            { 
                Console.WriteLine("No Match :("); 
            }
                       
            Console.ReadLine();
        }

        static double hashBuzz(string s)
        {
            Int64 h = 7;
            for (int i = 0; i < s.Length; i++)
            {
                h = (h * 37 + FIZZLETTERS.IndexOf(s[i]));
            }
            return h;
        }

        static bool crackHash(double target, out string candidate)
        {
            // 'a's always hash to 0 which means they don't affect the returned hash value.
            //   therefore a string of 7 'a's generates the output that results solely from the initial value of 7
            //   if we subtract that from the expected hash what's left is the portion of the hash represented by the actual letters used.
            //   taking advantage of the associative properties of addition and of multiplication it this way makes the algorithm simpler.
            Console.WriteLine("Minimum caused by initial '7' in hash is {0}", hashBuzz("aaaaaaaaa"));
            double hashRemains = target - hashBuzz("aaaaaaaaa");
            Console.WriteLine("Remainder value caused by string values that we're looking for is {0}", hashRemains);
            candidate = "";
            //I originally thought this algorithm would fail and I'd have to try something more interesting, perhaps recursion. But it didn't.
            //   I was a bit surprised at how easy it ended up being.
            for (int pos = 1; pos < 10; pos++)
            {
                for (int val = 15; val >= 0; val--)
                {
                    if (val * Math.Pow(37, 9 - pos) <= hashRemains)
                    {
                        candidate += FIZZLETTERS[val];
                        hashRemains = hashRemains - val * Math.Pow(37, 9 - pos);
                        break;
                    }
                }
            }
            return (hashBuzz(candidate) == target);
        }

    }
}
