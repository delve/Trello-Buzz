using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trello_Buzz
{
    class Program
    {

        static void Main(string[] args)
        {
            Int64 targetHash = 956446786872726;
            string candidate;
            //verify hashing function matches requirements
            Console.WriteLine("Hash of leepadg is: {0}", TrelloBuzz.hash("leepadg"));
            Console.WriteLine("Expected hash was-: 680131659347 -> {0}", TrelloBuzz.hash("leepadg") == 680131659347 ? "Matched" : "Failed!");
            //verifying my math dissection from math-notes.xlsx using some easily-SUM()-ed values
            Console.WriteLine("Hash of wwwwwwwww is: {0}", TrelloBuzz.hash("wwwwwwwww"));
            Console.WriteLine("Expected hash was---: 963882903480154 -> {0}", TrelloBuzz.hash("wwwwwwwww") == 963882903480154 ? "Matched" : "Failed!");
            Console.WriteLine("Hash of wwwwwwwwa is: {0}", TrelloBuzz.hash("wwwwwwwwa"));
            Console.WriteLine("Expected hash was---: 963882903480139 -> {0}", TrelloBuzz.hash("wwwwwwwwa") == 963882903480139 ? "Matched" : "Failed!");
            //the work
            Console.WriteLine("================================================");
            Console.WriteLine("Final hash value desired is {0}", targetHash);

            if (TrelloBuzz.crackHash(targetHash, out candidate)) 
            {
                Console.WriteLine("Match found");
                Console.WriteLine("Candidate string '{0}'; length: {1}; hash value: {2}", candidate, candidate.Length, TrelloBuzz.hash(candidate));
                Console.WriteLine("                                  Expected hash was: {0}", targetHash);
            }
            else
            { 
                Console.WriteLine("No Match :("); 
            }
            Console.WriteLine();
            Console.WriteLine("Next we're going to perform an exhaustive search for strings that won't crack under the above algorithm. Because I'm curious and have spare processor cycles.");
            Console.ReadLine();


        }


    }

    /// <summary>
    /// Utility class; implements the hashing algorithm and the related cracking algorithm
    /// </summary>
    class TrelloBuzz
    {
        const string FIZZLETTERS = "acdegilmnoprstuw";
        
        //TODO: Implement sanity check to ensure input contains only the values in FIZZLETTERS
        /// <summary>
        /// An implementation of the hashing algorithm
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>Output hash</returns>
        static public double hash(string s)
        {   
            Int64 h = 7;
            for (int i = 0; i < s.Length; i++)
            {
                h = (h * 37 + FIZZLETTERS.IndexOf(s[i]));
            }
            return h;
        }

        /// <summary>
        /// Breaks a given hash. Returns true/false for success. Result string is in the output parameter.
        /// </summary>
        /// <param name="target">The hash to crack</param>
        /// <param name="candidate">Output parameter; on method exit  this contains the original string value or the closest match obtained by the algorithm.</param>
        /// <returns>true = hash of output string matches target hash
        /// false = hash of output string does NOT match target hash</returns>
        static public bool crackHash(double target, out string candidate)
        {
            // 'a's always hash to 0 which means they don't affect the returned hash value.
            //   therefore a string of 7 'a's generates the output that results solely from the initial value of 7
            //   if we subtract that from the expected hash what's left is the portion of the hash represented by the actual letters used.
            //   taking advantage of the associative properties of addition and of multiplication it this way makes the algorithm simpler.
            //
            double hashRemains = target - hash("aaaaaaaaa");
            candidate = "";

            #region deprecated bits
            /*hiding this output as part of making the TrelloBuzz utility class; it doesn't make sense to generate output from here
            Console.WriteLine("Minimum caused by initial '7' in hash is {0}", hash("aaaaaaaaa"));
            Console.WriteLine("Remainder value caused by string values that we're looking for is {0}", hashRemains);
            */
            #endregion

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
            return (hash(candidate) == target);
        }

    }


}

