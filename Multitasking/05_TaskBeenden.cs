namespace Multitasking;

internal class _05_TaskBeenden
{
	static void Main(string[] args)
	{
		//Manchmal sollen Tasks vorher beendet werden
		//CancellationToken
		//Mechanismus, welcher an vielen Stellen in C# verwendet wird

		CancellationTokenSource cts = new(); //Source, welche die Tokens erzeugt
		CancellationToken ct = cts.Token; //Token aus der Source generieren (struct)

		Task t1 = new Task(Run, ct);
		t1.Start();

		Thread.Sleep(500);

		//Über die Source wird die Cancellation durchgeführt
		cts.Cancel(); //Sendet an alle Tokens das Cancel-Signal

		Console.ReadKey();
	}

	public static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				//if (ct.IsCancellationRequested)
				//{
				//	Console.WriteLine("Task abgebrochen");
				//	break;
				//}

				ct.ThrowIfCancellationRequested(); //Wenn ein Task eine Exception wirft, stürzt das Programm nicht ab

				Thread.Sleep(25);
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}
