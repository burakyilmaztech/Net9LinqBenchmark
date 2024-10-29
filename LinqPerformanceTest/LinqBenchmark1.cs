using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LinqPerformanceTest;



[MemoryDiagnoser(false)]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class LinqBenchmark1
{
    private static readonly List<Person> dataSet = Enumerable.Range(1, 1000)
        .Select(id => new Person { Id = id, Age = id % 100 })
        .ToList();

    [Benchmark]
    public bool Any() => dataSet.Any(x => x.Id == 1000);

    [Benchmark]
    public bool All() => dataSet.All(x => x.Age >= 0);

    [Benchmark]
    public int Count() => dataSet.Count(x => x.Age == 25);

    [Benchmark]
    public Person First() => dataSet.First(x => x.Id == 999);

    [Benchmark]
    public Person Single() => dataSet.Single(x => x.Id == 1);
}