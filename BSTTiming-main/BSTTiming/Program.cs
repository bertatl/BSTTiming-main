using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace BSTTiming
{
    public class Program
    {
        /// <summary>
        /// Duration of one second
        /// </summary>
        public const System.Int32 DURATION = 1000;

        public static System.Int32 SIZE;

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

        public static double RunBSTTiming(System.Int32 size)
        {
            // Construct a randomly-generated balanced
            //binary search tree
            SortedSet<System.Int32> bst = GenerateTree(size);

            System.Int32[] items = generateSearchItems(1024);

            // Create a stopwatch
            Stopwatch sw = new Stopwatch();

            Random random = new Random();

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
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
            double totalAverage = elapsed / repetitions;

            // Create a stopwatch
            sw = new Stopwatch();

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
            double overheadAverage = elapsed / repetitions;
            
            // Return the difference, averaged over size
            return (totalAverage - overheadAverage) / 1024;
        }

        private static System.Int32[] generateSearchItems(System.Int32 size)
        {
            HashSet<System.Int32> set = new HashSet<System.Int32>();
            Random random = new Random();
            System.Int32 num;
            for(System.Int32 i = 0; i < size; i++)
            {
                do
                {
                    num = random.Next(0, size);
                } while (set.Contains(num));

                set.Add(num);
            }
            return set.ToArray();
        }

        private static SortedSet<System.Int32> GenerateTree(System.Int32 size)
        {
            SortedSet<System.Int32> bst = new SortedSet<System.Int32>();
            Random random = new Random();

            System.Int32 number;
            for (System.Int32 i = 0; i < size; i++)
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