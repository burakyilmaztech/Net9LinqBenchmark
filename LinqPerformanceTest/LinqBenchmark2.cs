using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LinqPerformanceTest;

[MemoryDiagnoser(false)]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class LinqBenchmark2
{
   private readonly IEnumerable<int> _distinctNumbers = Enumerable.Range(1, 1000).ToArray().Distinct();
    private readonly IEnumerable<int> _appendedNumbers = Enumerable.Range(1, 1000).ToArray().Append(42).Select(x => x * 2);
    private readonly IEnumerable<int> _reversedNumbers = Enumerable.Range(1, 1000).Reverse();
    private readonly IEnumerable<int> _defaultIfEmptyList = Enumerable.Range(1, 1000).ToList().DefaultIfEmpty(-1);
    private readonly IEnumerable<int> _skippedAndTakenNumbers = Enumerable.Range(1, 1000).ToList().Skip(300).Take(200);
    private readonly IEnumerable<int> _unionNumbers = Enumerable.Range(1, 1000).Union(Enumerable.Range(500, 500));

    private readonly List<int> _largeList = Enumerable.Range(1, 1000).ToList();

    [Benchmark]
    public int DistinctFirst() => _distinctNumbers.First();

    [Benchmark]
    public int AppendSelectLast() => _appendedNumbers.Last();

    [Benchmark]
    public int ReverseCount() => _reversedNumbers.Count();

    [Benchmark]
    public int DefaultIfEmptySelectElementAt() => _defaultIfEmptyList.ElementAt(500);

    [Benchmark]
    public int ListSkipTakeElementAt() => _skippedAndTakenNumbers.ElementAt(50);

    [Benchmark]
    public int RangeUnionFirst() => _unionNumbers.First();

    [Benchmark]
    public int ListSum() => _largeList.Sum();

    [Benchmark]
    public int ListMax() => _largeList.Max();

    [Benchmark]
    public int ListMin() => _largeList.Min();
    
}