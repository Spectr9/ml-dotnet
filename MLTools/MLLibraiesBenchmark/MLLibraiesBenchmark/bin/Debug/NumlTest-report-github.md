```ini
BenchmarkDotNet=v0.9.2.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i5-4200U CPU @ 1.60GHz, ProcessorCount=4
Frequency=2240909 ticks, Resolution=446.2475 ns
HostCLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE

Type=NumlTest  Mode=Throughput  

```
           Method |    Median |    StdDev |
----------------- |---------- |---------- |
 DecisionTreeTest | 3.0074 ms | 0.5719 ms |
          KNNTest | 6.6210 ms | 0.3223 ms |
   NaiveBayesTest | 1.9585 ms | 0.1935 ms |
