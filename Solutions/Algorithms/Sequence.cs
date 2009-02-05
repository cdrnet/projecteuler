using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Solutions.Algorithms
{
    public static class Sequence
    {
        public static IEnumerable<long> EnumerateFibonacci(long maxValue)
        {
            long a = 1;
            long b = 2;
            long c = 3;

            yield return 1;
            yield return 2;

            while((c = a + b) <= maxValue)
            {
                yield return c;
                a = b;
                b = c;
            }
        }

        public static IEnumerable<long> EnumerateFibonacci(long maxValue, Func<long, bool> predicate)
        {
            long a = 1;
            long b = 2;
            long c = 3;

            if(predicate(1))
            {
                yield return 1;
            }

            if(predicate(2))
            {
                yield return 2;
            }

            while((c = a + b) <= maxValue)
            {
                if(predicate(c))
                {
                    yield return c;
                }

                a = b;
                b = c;
            }
        }

        public static IEnumerable<long> EnumerateFibonacciEven(long maxValue)
        {
            long a = 2;
            long b = 3;
            long c = 5;

            do
            {
                yield return a;

                a = b + c;
                b = a + c;
                c = b + a;
            }
            while(a <= maxValue);
        }
    }
}
