namespace Multitasking;

internal class _08_Lock
{
	private static int Counter;

	private static readonly object Lock = new object();

	static void Main(string[] args)
	{
		List<Task> tasks = [];
		for (int i = 0; i < 50; i++)
		{
			Task t = new Task(Increment);
			t.Start();
			tasks.Add(t);
		}
		Console.ReadKey();
	}

	static void Increment()
	{
		for (int i = 0; i < 100; i++)
		{
			//Lock-Block
			//Sperrt den Codeblock, sobald ein Task den Block ausführen möchte
			//Alle anderen Tasks warten vor dem Block, bis dieser wieder freigegeben ist
			lock (Lock)
			{
				Counter++;
				Console.WriteLine(Counter);
			}

			//Monitor
			//Selber Effekt wie Lock-Block, aber mit Methoden
			Monitor.Enter(Lock);
			Counter++;
			Console.WriteLine(Counter);
			Monitor.Exit(Lock);

			Interlocked.Increment(ref Counter); //Erhöht eine int-Variable um 1, aber mit Lock herum
		}
	}
}
