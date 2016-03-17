    BenchmarkDotNet=v0.9.2.0
    OS=Microsoft Windows NT 6.2.9200.0
    Processor=Intel(R) Core(TM) i5-4200U CPU @ 1.60GHz, ProcessorCount=4
    Frequency=2240908 ticks, Resolution=446.2477 ns
    HostCLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE [AttachedDebugger]
    
    Type=AccordTest  Mode=Throughput  
    
               Method |        Median |      StdDev |
    ----------------- |-------------- |------------ |
     DecisionTreeTest | 1,545.6974 us | 211.9197 us |
              KNNTest |   177.4697 us |  72.3656 us |
       NaiveBayesTest |   183.8306 us |   5.9074 us |
