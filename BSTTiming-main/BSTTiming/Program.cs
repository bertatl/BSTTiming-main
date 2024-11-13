using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTiming
{
    class Program
    {
        /// <summary>
        /// Duration of one second
        /// </summary>
        public const int DURATION = 1000;

        public static int SIZE;

        static void Main(string[] args)
        {
            
            String line;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Jesus Zarate\Desktop\timingResults.txt"))
            {
                line = "Time";
                Console.WriteLine(line);
                file.WriteLine(line);

                for (int i = 10; i <= 20; i++)
                {
                    SIZE = (int)Math.Pow(2, i);
                    line = RunBSTTiming(SIZE) + "";

                    // Uncomment me
                    Console.WriteLine(line);
                    file.WriteLine(line);
                }
            }
            Console.WriteLine("Finished");
            Console.Read();
        }

        public static double RunBSTTiming(int size)
        {
            // Construct a randomly-generated balanced 
            //binary search tree
            SortedSet<int> bst = generateTree(size);

            int[] items = generateSearchItems(1024);

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

        private static int[] generateSearchItems(int size)
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

        private static SortedSet<int> generateTree(int size)
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
