    BenchmarkDotNet=v0.9.2.0
    OS=Microsoft Windows NT 6.2.9200.0
    Processor=Intel(R) Core(TM) i5-4200U CPU @ 1.60GHz, ProcessorCount=4
    Frequency=2240909 ticks, Resolution=446.2475 ns
    HostCLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
    
    Type=AccordTest  Mode=Throughput  
    
               Method |        Median |     StdDev |
    ----------------- |-------------- |----------- |
     DecisionTreeTest | 1,096.7456 us | 63.8799 us |
              KNNTest |   164.5729 us | 17.8187 us |
       NaiveBayesTest |   123.4386 us |  2.2288 us |
