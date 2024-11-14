using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
            string line;
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

        public static global::System.Double RunBSTTiming(int size)
        {
            // Construct a randomly-generated balanced
            //binary search tree
            global::System.Collections.Generic.SortedSet<int> bst = GenerateTree(size);

            int[] items = GenerateSearchItems(1024);

            // Create a stopwatch
            global::System.Diagnostics.Stopwatch sw = new global::System.Diagnostics.Stopwatch();

            global::System.Random random = new global::System.Random();

            // Keep increasing the number of repetitions until one second elapses.
            global::System.Double elapsed = 0;
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
            global::System.Double totalAverage = elapsed / (global::System.Double)repetitions;

            // Create a stopwatch
            sw = new global::System.Diagnostics.Stopwatch();

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
            global::System.Double overheadAverage = elapsed / repetitions;

            // Return the difference, averaged over size
            return (totalAverage - overheadAverage) / 1024;
        }

        private static int[] GenerateSearchItems(int size)
        {
            global::System.Collections.Generic.HashSet<int> set = new global::System.Collections.Generic.HashSet<int>();
            global::System.Random random = new global::System.Random();
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

        private static global::System.Collections.Generic.SortedSet<int> GenerateTree(int size)
        {
            global::System.Collections.Generic.SortedSet<int> bst = new global::System.Collections.Generic.SortedSet<int>();
            global::System.Random random = new global::System.Random();

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
        public static global::System.Double msecs(global::System.Diagnostics.Stopwatch sw)
        {
            return (((global::System.Double)sw.ElapsedTicks) / global::System.Diagnostics.Stopwatch.Frequency) * 1000;
        }

    }
}