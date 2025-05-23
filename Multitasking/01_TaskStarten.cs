namespace Multitasking;

public class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t1 = new Task(Run); //Parallele Arbeit anlegen
		t1.Start(); //Parallele Arbeit starten

		//t1 und der Rest des Main Threads laufen parallel

		Task t2 = Task.Factory.StartNew(Run); //ab .NET Framework 4.0

		Task t3 = Task.Run(Run); //ab .NET Framework 4.5

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		//Vordergrundthreads und Hintergrundthreads
		//Wenn alle Vordergrundthreads fertig sind, werden alle Hintergrundthreads abgebrochen
		//WICHTIG: Alle Tasks sind immer Hintergrundthreads

		//Lösung: Main Thread blockieren
		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 1000; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}
