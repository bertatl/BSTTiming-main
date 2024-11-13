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

            global::System.String line;
using (global::System.IO.StreamWriter file = new global::System.IO.StreamWriter(@"C:\Users\Jesus Zarate\Desktop\timingResults.txt"))
            {
                line = "Time";
                global::System.Console.WriteLine(line);
                file.WriteLine(line);

                for (int i = 10; i <= 20; i++)
                {
                    SIZE = (int)global::System.Math.Pow(2, i);
                    line = RunBSTTiming(SIZE) + "";

                    // Uncomment me
                    global::System.Console.WriteLine(line);
                    file.WriteLine(line);
                }
            }
            global::System.Console.WriteLine("Finished");
            global::System.Console.Read();
        }

        public static double RunBSTTiming(int size)
        {
            // Construct a randomly-generated balanced
            //binary search tree
            global::System.Collections.Generic.SortedSet<int> bst = generateTree(size);

            int[] items = generateSearchItems(1024);

            // Create a stopwatch
            global::System.Diagnostics.Stopwatch sw = new global::System.Diagnostics.Stopwatch();

            global::System.Random random = new global::System.Random();

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
            double overheadAverage = elapsed / repetitions;

            // Return the difference, averaged over size
            return (totalAverage - overheadAverage) / 1024;
        }

        private static int[] generateSearchItems(int size)
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

        private static global::System.Collections.Generic.SortedSet<int> generateTree(int size)
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
        public static double msecs(global::System.Diagnostics.Stopwatch sw)
        {
            return (((double)sw.ElapsedTicks) / global::System.Diagnostics.Stopwatch.Frequency) * 1000;
        }

    }
}