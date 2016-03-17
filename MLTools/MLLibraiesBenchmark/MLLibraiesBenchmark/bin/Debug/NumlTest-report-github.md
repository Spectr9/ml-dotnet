```ini
BenchmarkDotNet=v0.9.2.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i5-4200U CPU @ 1.60GHz, ProcessorCount=4
Frequency=2240908 ticks, Resolution=446.2477 ns
HostCLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE [AttachedDebugger]

Type=NumlTest  Mode=Throughput  

```
           Method |     Median |    StdDev |
----------------- |----------- |---------- |
 DecisionTreeTest |  3.3179 ms | 1.0769 ms |
          KNNTest | 11.9718 ms | 1.3346 ms |
   NaiveBayesTest |  2.3710 ms | 0.7887 ms |
