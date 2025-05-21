using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks;

public class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<Benchmarks>();
	}
}

[MemoryDiagnoser(false)]
public class Benchmarks
{
	private IEnumerable<int> zahlen = Enumerable.Range(0, 100_000_000);

	[Benchmark]
	[IterationCount(20)]
	public void TestLinq()
	{
		List<int> w1 = zahlen.Where(e => e % 2 == 0).ToList();
	}

	[Benchmark]
	[IterationCount(20)]
	public void TestNoLinq()
	{
		List<int> w2 = new List<int>(100_000_000);
		foreach (int y in zahlen)
			if (y % 2 == 0)
				w2.Add(y);
	}
}