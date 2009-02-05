using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics;
using ProjectEuler.Solutions.Algorithms;

namespace ProjectEuler.Solutions
{
    public static class Solution
    {
        #region Problem 2

        public static double[] GetFibonacciSequence(double maxTermValue)
        {
            List<double> fibSeq = new List<double>();
            fibSeq.Add(1);
            fibSeq.Add(2);

            double term = fibSeq[0] + fibSeq[1];
            while (term <= maxTermValue)
            {
                fibSeq.Add(term);
                term = fibSeq[fibSeq.Count - 1] + fibSeq[fibSeq.Count - 2];
            }

            return fibSeq.ToArray();
        }

        #endregion Problem 2

        #region Problem 9

        public static long Problem9()
        {
            Triangle answer = P9_calculation();

            //ANSWER
            Console.WriteLine(string.Format("a = {1}{0}b = {2}{0}c = {3}", 
                Environment.NewLine, answer.SideA, answer.SideB, answer.SideC));
            Console.WriteLine(string.Format("a^2 + b^2 = {1}{0}      c^2 = {2}",
                Environment.NewLine, Math.Pow(answer.SideA, 2) + Math.Pow(answer.SideB, 2), Math.Pow(answer.SideC, 2)));
            Console.WriteLine(string.Format("a + b + c = {1}{0}a * b * c = {2}",
                Environment.NewLine, answer.SumOfSides(), answer.ProductOfSides()));

            return Convert.ToInt64(answer.ProductOfSides());
        }
        private static Triangle P9_calculation()
        {
            List<Triangle> possibleTriangles = new List<Triangle>();
            Triangle answer = new Triangle();
            for (int a = 1; a < 995; a++)
            {
                for (int b = a + 1; b < 996; b++)
                {
                    for (int c = b + 1; c < 997; c++)
                    {
                        if (a + b + c == 1000)
                            possibleTriangles.Add(new Triangle(a, b, c));
                    }
                }
            }

            foreach (Triangle triange in possibleTriangles)
            {
                if (triange.IsPythagoreanTriplet())
                {
                    answer = triange;
                    break;
                }
            }

            return answer;
        }

        #endregion

        #region Problem 10

        public static List<bool> EratosthenesSieve(List<bool> list, int currentMultiple)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                if (list[i])
                {
                    if (currentMultiple > Math.Sqrt(i))
                        return list;
                    break;
                }
            }

            for (int i = currentMultiple * 2; i < list.Count; i += currentMultiple)
            {
                list[i] = false;
            }

            int nextMultiple=0;
            for (int i = currentMultiple + 1; i < list.Count; i++)
            {
                if (list[i])
                {
                    nextMultiple = i;
                    break;
                }
            }

            return EratosthenesSieve(list, nextMultiple);
        }

        #endregion

        #region Problem 11

        static double[,] table11 = new double[,] { { 08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08 }, { 49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00 }, { 81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65 }, { 52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91 }, { 22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80 }, { 24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50 }, { 32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70 }, { 67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21 }, { 24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72 }, { 21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95 }, { 78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92 }, { 16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57 }, { 86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58 }, { 19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40 }, { 04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66 }, { 88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69 }, { 04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36 }, { 20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16 }, { 20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54 }, { 01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48 } };
        enum Directions { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
        private static void _printGrid(double[,] table)
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (table[x, y] < 10)
                        Console.Write("0");
                    Console.Write(table[x, y].ToString() + " ");
                }
                Console.WriteLine();
            }
        }
        public static long Problem11()
        {
            double[,] table = table11;

            int consecutiveTerms = 4;
            double product = 0;
            double temp = 0;
            
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    //Console.WriteLine("[{0},{1}] ", x, y);
                    foreach (Directions direction in Enum.GetValues(typeof(Directions)))
                    {
                        //Console.Write("{0} -> ", direction.ToString().ToUpper());
                        switch (direction)
                        {
                            case Directions.North:
                                if (x - (consecutiveTerms - 1) >= 0)
                                    temp = _productNorth(table, x, y, consecutiveTerms);
                                break;
                            case Directions.NorthEast:
                                if ((x - (consecutiveTerms - 1) >= 0) &&
                                   (y + (consecutiveTerms - 1) < 20))
                                    temp = _productNorthEast(table, x, y, consecutiveTerms);
                                break;
                            case Directions.East:
                                if (y + (consecutiveTerms - 1) < 20)
                                    temp = _productEast(table, x, y, consecutiveTerms);
                                break;
                            case Directions.SouthEast:
                                if ((x + (consecutiveTerms - 1) < 20) &&
                                    (y + (consecutiveTerms - 1) < 20))
                                    temp = _productSouthEast(table, x, y, consecutiveTerms);
                                break;
                            case Directions.South:
                                if (x + (consecutiveTerms - 1) < 20)
                                    temp = _productSouth(table, x, y, consecutiveTerms);
                                break;
                            case Directions.SouthWest:
                                if ((x + (consecutiveTerms - 1) < 20) &&
                                    (y - (consecutiveTerms - 1) >= 0))
                                    temp = _productSouthWest(table, x, y, consecutiveTerms);
                                break;
                            case Directions.West:
                                if (y - (consecutiveTerms - 1) >= 0)
                                    temp = _productWest(table, x, y, consecutiveTerms);
                                break;
                            case Directions.NorthWest:
                                if ((x - (consecutiveTerms - 1) >= 0) &&
                                    (y - (consecutiveTerms - 1) >= 0))
                                    temp = _productNorthWest(table, x, y, consecutiveTerms);
                                break;
                            default:
                                break;
                        }

                        //if (temp != 0) 
                        //    Console.WriteLine(" = {0}", temp); 
                        //else
                        //    Console.WriteLine("INVALID");

                        if (product < temp) product = temp;
                        temp = 0;
                    }
                }
            }
            return Convert.ToInt64(product);
        }

        private static double _productNorth(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productNorth(table, x - 1, y, consecutiveTerms - 1);
        }
        private static double _productNorthEast(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productNorthEast(table, x - 1, y + 1, consecutiveTerms - 1);
        }
        private static double _productEast(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productEast(table, x, y + 1, consecutiveTerms - 1);
        }
        private static double _productSouthEast(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productSouthEast(table, x + 1, y + 1, consecutiveTerms - 1);
        }
        private static double _productSouth(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productSouth(table, x + 1, y , consecutiveTerms - 1);
        }
        private static double _productSouthWest(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productSouthWest(table, x + 1, y - 1, consecutiveTerms - 1);
        }
        private static double _productWest(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productWest(table, x, y - 1, consecutiveTerms - 1);
        }
        private static double _productNorthWest(double[,] table, int x, int y, int consecutiveTerms)
        {
            //Console.Write(table[x, y]);
            if (consecutiveTerms == 1)
                return table[x, y];

            //Console.Write(" * ");
            return table[x, y] * _productNorthWest(table, x - 1, y - 1, consecutiveTerms - 1);
        }

        #endregion

        #region Problem 14

        public static long Problem14()
        {
            KeyValuePair<double, double> answer = P14_calculation();
            return Convert.ToInt64(answer.Key);
        }
        private static KeyValuePair<double, double> P14_calculation()
        {
            Dictionary<double, double> dict = new Dictionary<double, double>();
            dict.Add(1, 1); 
            KeyValuePair<double, double> answer = new KeyValuePair<double, double>(1, 1);
            double terms = 1;

            for (double startNum = 2; startNum <= 1000000; startNum++)
            {
                if (!dict.ContainsKey(startNum))
                {
                    terms = Solution._recursiveSequence(dict, startNum);
                    if (terms > answer.Value)
                        answer = new KeyValuePair<double, double>(startNum, terms);
                }
            }
            return answer;
        }
        private static double _recursiveSequence(Dictionary<double, double> dict, double startNum)
        {
            double nextTerm = 1;
            double steps = 0;
            //if the key exists, return the value
            if (dict.ContainsKey(startNum))
                return dict[startNum];

            if (startNum % 2 != 0)
                nextTerm = 3 * startNum + 1;
            else
                nextTerm = startNum / 2;

            steps = 1 + _recursiveSequence(dict, nextTerm);
            dict.Add(startNum, steps);

            return steps;
        }

        #endregion
    }

    public static class Tests
    {
        public static void GetHashCodes_Test()
        {
            string a = "(0,2)->(1,2)";
            string b = "(0,2)->(1,2)";
            string c = "(0,2)->(0,1)";

            //string a,b should have the same HashCode [1346283806]
            //string c should have a different HashCode [219800134]
            Console.WriteLine("a={1}{0}b={2}{0}c={3}{0}", Environment.NewLine,
                a.GetHashCode(), b.GetHashCode(), c.GetHashCode());

            Queue<string> q1 = new Queue<string>();
            q1.Enqueue(a);
            q1.Enqueue(c);

            Queue<string> q2 = new Queue<string>();
            q2.Enqueue(b);
            q2.Enqueue(c);

            //I guessed that both q1,q2 should have the same HashCode.  I was WRONG.
            //The HashCodes for these two change every instantiation.  There must be a piece of the 
            // data structure that changes upon each instantiation.  
            Console.WriteLine("q1={1}{0}q2={2}{0}", Environment.NewLine,
                q1.GetHashCode(), q2.GetHashCode());

            Console.Read();
        }
    }
}