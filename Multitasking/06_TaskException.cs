namespace Multitasking;

internal class _06_TaskException
{
	static void Main(string[] args)
	{
		Task t1 = Task.Run(Run);
		Task t2 = Task.Run(Run);
		Task t3 = Task.Run(Run);

		//Problem: Wenn ein Task abstürzt, bekommen wir davon nichts mit
		//Lösung: AggregateException, ContinueWith

		//AggregateException über t.Wait(), Task.WaitAll(...), t.Result
		try
		{
			Task.WaitAll(t1, t2, t3);
		}
		catch (AggregateException ex)
		{
			foreach (Exception x in ex.InnerExceptions)
				Console.WriteLine(x.Message);
		}

		Console.ReadKey();
	}

	static void Run()
	{
		throw new Exception();
	}
}
