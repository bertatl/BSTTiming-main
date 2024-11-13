using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BSTTiming
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Value;
            public Node Left, Right;
            public Node(T value) { Value = value; }
        }

        private Node root;

        public void Add(T value)
        {
            if (root == null)
                root = new Node(value);
            else
                AddTo(root, value);
        }

        private void AddTo(Node node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                    node.Left = new Node(value);
                else
                    AddTo(node.Left, value);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new Node(value);
                else
                    AddTo(node.Right, value);
            }
        }

        public bool Contains(T value)
        {
            return Contains(root, value);
        }

        private bool Contains(Node node, T value)
        {
            if (node == null)
                return false;
            int compare = value.CompareTo(node.Value);
            if (compare == 0)
                return true;
            if (compare < 0)
                return Contains(node.Left, value);
            return Contains(node.Right, value);
        }
    }

    public class Program : System.Object
    {
        /// <summary>
        /// Duration of one second
        /// </summary>
        public const int DURATION = 1000;

        public static int SIZE;

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

        public static System.Double RunBSTTiming(int size)
        {
            // Construct a randomly-generated binary search tree
            BinarySearchTree<int> bst = GenerateTree(size);

            int[] items = GenerateSearchItems(1024);

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

        private static BinarySearchTree<int> GenerateTree(int size)
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
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