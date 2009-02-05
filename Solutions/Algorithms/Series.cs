using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Solutions.Algorithms
{
    public static class Series
    {
        /// <summary>
        /// sum(i, i=1..n);
        /// </summary>
        /// <param name="last">n</param>
        public static
        long
        SumOfIntegers(long last)
        {
            if(last < 1)
            {
                return 0;
            }

            return last * (last + 1) / 2;
        }

        /// <summary>
        /// sum(i, i=m..n);
        /// </summary>
        /// <param name="first">m</param>
        /// <param name="last">n</param>
        public static
        long
        SumOfIntegers(long first, long last)
        {
            if(first > last)
            {
                return 0;
            }

            return SumOfIntegers(last) - SumOfIntegers(first - 1);
        }

        /// <summary>
        /// sum(k*i, i=1..n/k);
        /// </summary>
        /// <param name="last">n</param>
        public static
        long
        SumOfEveryKthIntegers(long k, long last)
        {
            if(last < k)
            {
                return 0;
            }

            long rem;
            Math.DivRem(last, k, out rem);
            last -= rem;

            return last * (last + k) / (2 * k);
        }

        /// <summary>
        /// sum(i^2, i=1..n);
        /// </summary>
        /// <param name="last">n</param>
        public static
        long
        SumOfSquares(long last)
        {
            if(last < 1)
            {
                return 0;
            }

            return last * (last + 1) * (last + last + 1) / 6;
        }

        /// <summary>
        /// sum(i^3, i=1..n);
        /// </summary>
        /// <param name="last">n</param>
        public static
        long
        SumOfCubes(long last)
        {
            if(last < 1)
            {
                return 0;
            }

            return (last * last) * ((last + 1) * (last + 1)) / 4;
        }
    }
}
