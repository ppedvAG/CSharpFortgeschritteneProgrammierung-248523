namespace Multitasking;

internal class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t1 = new Task(Run);
		t1.Start();

		Task t2 = Task.Run(Run);
		Task t3 = Task.Run(Run);

		t1.Wait(); //Halte den Main Thread auf, bis der Task fertig ist

		Task.WaitAll(t1, t2, t3); //Halte den Main Thread auf, bis alle Tasks fertig ist

		Task.WaitAny(t1, t2, t3); //Halte den Main Thread auf, bis einer der Tasks fertig ist

		//Problem: Blockieren des Main Threads
		//Wenn in einer GUI Anwendung (WinForms, WPF, MAUI, ASP, ...) der Main Thread blockiert wird, friert die UI ein
		//Lösung: await

		for (int i = 0; i < 1000; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 1000; i++)
		{
			Console.WriteLine($"Task: {i}");
			Thread.Sleep(10);
		}
	}
}
