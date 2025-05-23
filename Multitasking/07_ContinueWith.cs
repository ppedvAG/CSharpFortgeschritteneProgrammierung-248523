namespace Multitasking;

internal class _07_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith: Tasks verketten; wenn der erste Task fertig ist, können beliebig viele Folgetasks gestartet werden
		Task<int> t1 = new Task<int>(Berechne);

		//Vor dem Start kann der Task konfiguriert werden
		//U.a. ContinueWith
		t1.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Bei ContinueWith ist immer der Zugriff auf den vorherigen Task möglich
		//Result blockiert hier nicht mehr, da der Folgetask auch als Task gestartet wird

		t1.Start();

		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(10);
			Console.WriteLine($"Main Thread: {i}");
		}

		//////////////////////////////////////////////////////////////

		//Fehlerbehandlung mit ContinueWith
		Task t2 = new Task(Run);

		t2.ContinueWith(x => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion); //Nur wenn der Task Erfolgreich abgelaufen ist
		t2.ContinueWith(x => Console.WriteLine(x.Exception.Message), TaskContinuationOptions.OnlyOnFaulted); //Nur wenn der Task Fehlerhaft abgelaufen ist

		t2.Start();

		Console.ReadKey();
	}

	static int Berechne()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}

	static void Run()
	{
		if (Random.Shared.Next() % 2 == 0)
			throw new Exception("50%");
	}
}
