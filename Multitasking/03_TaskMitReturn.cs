namespace Multitasking;

internal class _03_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t1 = new Task<int>(Berechne);
		t1.Start();

		bool hasPrinted = false;

		//Ergebnis angreifen: t1.Result
		//Problem: Blockieren des Main Threads
		//Lösung: ContinueWith, async/await

		//Console.WriteLine(t1.Result); //Hier kommt das Ergebnis immer vor der Schleife
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Main Thread: {i}");

			if (t1.IsCompletedSuccessfully && !hasPrinted) //Schreibe das Ergebnis erst heraus, wenn der Task fertig ist
			{
				Console.WriteLine(t1.Result);
				hasPrinted = true;
			}
		}

		Console.ReadKey();
	}

	static int Berechne()
	{
		Thread.Sleep(1000);
		return Random.Shared.Next();
	}
}
