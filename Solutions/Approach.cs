using System;
using System.Diagnostics;

namespace ProjectEuler.Solutions
{
    public class Approach
    {
        public string Title { get; set; }
        public Func<long> Algorithm { get; set; }
        public int WarmupRounds { get; set; }
        public int BenchmarkRounds { get; set; }

        public Approach()
        {
            Title = "Unnamed";
            WarmupRounds = 1;
            BenchmarkRounds = 5;
        }

        public void Run(Stopwatch watch)
        {
            Run(watch, WarmupRounds, BenchmarkRounds);
        }

        public void Run(Stopwatch watch, int warmupRounds, int benchmarkRounds)
        {
            Func<long> algorithm = Algorithm;
            long result = 0;

            // dummy variable trying to ensure nothing is optimized away.
            long dummy = long.MaxValue;

            // JIT/Warmup
            for(int i = 0; i < warmupRounds; i++)
            {
                result = algorithm();
                dummy ^= result;
            }

            // Try to prevent GC run during execution
            GC.Collect();
            GC.Collect();

            // Benchmark
            watch.Reset();
            watch.Start();
            for(int i = 0; i < benchmarkRounds; i++)
            {
                result = algorithm();
                dummy ^= result;
            }
            watch.Stop();

            Console.WriteLine("{0}: {1}", Title, result);
            Console.Write("Elapsed Time: ");
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} ms ({1} ticks)", watch.ElapsedMilliseconds / benchmarkRounds, watch.ElapsedTicks / benchmarkRounds);
            Console.ForegroundColor = previousColor;
            Console.WriteLine(" on {0} rounds average. D={1}", benchmarkRounds, dummy & 0x1);
            Console.WriteLine();
        }
    }
}
