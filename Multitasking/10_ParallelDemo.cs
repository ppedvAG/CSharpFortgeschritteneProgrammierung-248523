using System.Diagnostics;

namespace Multitasking;

public class _10_ParallelDemo
{
	static void Main(string[] args)
	{
		int[] iterations = [1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000];
		foreach (int d in iterations)
		{
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Interations: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Interations: {d}, {sw2.ElapsedMilliseconds}ms");

			Console.WriteLine("------------------------------------------------------------");
		}

		/*  
			For Interations: 1000, 0ms
			ParallelFor Interations: 1000, 52ms
			------------------------------------------------------------
			For Interations: 10000, 0ms
			ParallelFor Interations: 10000, 4ms
			------------------------------------------------------------
			For Interations: 50000, 1ms
			ParallelFor Interations: 50000, 4ms
			------------------------------------------------------------
			For Interations: 100000, 3ms
			ParallelFor Interations: 100000, 6ms
			------------------------------------------------------------
			For Interations: 250000, 9ms
			ParallelFor Interations: 250000, 2ms
			------------------------------------------------------------
			For Interations: 500000, 18ms
			ParallelFor Interations: 500000, 39ms
			------------------------------------------------------------
			For Interations: 1000000, 45ms
			ParallelFor Interations: 1000000, 32ms
			------------------------------------------------------------
			For Interations: 5000000, 218ms
			ParallelFor Interations: 5000000, 76ms
			------------------------------------------------------------
			For Interations: 10000000, 381ms
			ParallelFor Interations: 10000000, 98ms
			------------------------------------------------------------
			For Interations: 100000000, 4058ms
			ParallelFor Interations: 100000000, 1653ms
			------------------------------------------------------------
		*/
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i =>
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}