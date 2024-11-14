using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime;
using System.Threading.Tasks;

// Explicit import of System namespace
using System;

namespace BSTTiming
{

    public class Program : System.Object
    {
        /// <summary>
        /// Duration of one second
        /// </summary>
        public const int DURATION = 1000;

        public static int SIZE;

        // Explicitly declare string type
        public static string line;

        public static void Main(string[] args)
        {
using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Jesus Zarate\Desktop\timingResults.txt"))
            {
                line = "Time";
                Console.WriteLine(line);
                file.WriteLine(line);

                for (int i = 10; i <= 20; i++)
                {
                    SIZE = (int)Math.Pow(2, i);
                    line = RunBSTTiming(SIZE).ToString();

                    Console.WriteLine(line);
                    file.WriteLine(line);
                }
            }
            Console.WriteLine("Finished");
            Console.Read();
        }

        public static System.Double RunBSTTiming(int size)
        {
            // Construct a randomly-generated balanced
            //binary search tree
            System.Collections.Generic.SortedSet<int> bst = GenerateTree(size);

            int[] items = GenerateSearchItems(1024);

            // Create a stopwatch
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            System.Random random = new System.Random();

            // Keep increasing the number of repetitions until one second elapses.
            System.Double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (int i = 0; i < repetitions; i++)
                {
                    for (int elt = 0; elt < 1024; elt++)
                    {
                        bst.Contains(items[elt]);
                    }
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            System.Double totalAverage = elapsed / (System.Double)repetitions;

            // Create a stopwatch
            sw = new System.Diagnostics.Stopwatch();

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (int i = 0; i < repetitions; i++)
                {
                    for (int elt = 0; elt < 1024; elt++)
                    {
                    }
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            System.Double overheadAverage = elapsed / repetitions;

            // Return the difference, averaged over size
            return (totalAverage - overheadAverage) / 1024;
        }

        private static int[] GenerateSearchItems(int size)
        {
            HashSet<int> set = new HashSet<int>();
            Random random = new Random();
            int num;
            for(int i = 0; i < size; i++)
            {
                do
                {
                    num = random.Next(0, size);
                } while (set.Contains(num));

                set.Add(num);
            }
            return set.ToArray();
        }

        private static SortedSet<int> GenerateTree(int size)
        {
            SortedSet<int> bst = new SortedSet<int>();
            Random random = new Random();

            int number;
            for (int i = 0; i < size; i++)
            {
                do
                {
                    number = random.Next(0, size);
                } while (bst.Contains(number));

                bst.Add(number);
            }
            
            return bst;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed on the Stopwatch.
        /// </summary>
        public static double msecs(Stopwatch sw)
        {
            return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
        }

    }
}