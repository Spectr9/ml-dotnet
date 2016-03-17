using BenchmarkDotNet.Running;
using MLLibraiesBenchmark.AccordBenchmark;
using MLLibraiesBenchmark.NumlBenchmark;

namespace MLLibraiesBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<AccordTest>();
            BenchmarkRunner.Run<NumlTest>();
        }
    }



}