using BenchmarkDotNet.Running;

namespace MLLibraiesBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<IrisBenchmark>();
            BenchmarkRunner.Run<SkinBenchmark>();
        }
    }



}