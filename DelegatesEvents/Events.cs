namespace DelegatesEvents;

/// <summary>
/// Events
/// 
/// Statischer Punkt, an den Methoden angehängt werden können
/// Unterschied zu Variable: Es können nur per +=/-= Methoden an-/abgehängt werden (kein =)
/// 
/// Zweiseitige Programmierung:
/// - Entwicklerseite
/// - Anwenderseite
/// 
/// Entwicklerseite: 
/// - Definiert das Event
/// - Feuert das Event
/// - Prüft, wann/ob das Event ausgeführt werden soll
/// 
/// Anwenderseite:
/// - Hängt eine/mehrere Methode(n) an das Event an
/// - Definiert, was das Event tun soll
/// </summary>
internal class Events
{
	//Definition von einem Event mit einem Delegate (kann ein beliebiges Delegate sein)
	public event EventHandler TestEvent; //Entwicklerseite

	public event EventHandler<TestEventArgs> ArgsEvent;

	public event EventHandler<int> IntEvent;

	//Event Accessoren
	private event EventHandler accessorEvent;

	public event EventHandler AccessorEvent
	{
		add //Code ausführen, wenn eine Methode angehängt wird
		{
			if (accessorEvent?.GetInvocationList().Length == 0)
				accessorEvent += value;
		}
		remove //Code ausführen, wenn eine Methode abgehängt wird
		{
			accessorEvent -= value;
		}
	}

	static void Main(string[] args) => new Events().Run();

	public void Run()
	{
		//Anhängen der Methode an das Event
		TestEvent += Events_TestEvent; //Anwenderseite

		//Ausführen des Events
		//Über die EventArgs können noch Daten an die Methode vom User weitergegeben werden
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		/////////////////////////////////////////////////////////////////////

		//Event mit Argumenten
		ArgsEvent += Events_ArgsEvent;

		ArgsEvent?.Invoke(this, new TestEventArgs() { Value = "Hallo" });

		/////////////////////////////////////////////////////////////////////

		IntEvent += Events_IntEvent;

		IntEvent?.Invoke(this, 10);

		/////////////////////////////////////////////////////////////////////

		AccessorEvent += Events_AccessorEvent;

		accessorEvent?.Invoke(this, EventArgs.Empty); //Ausführung muss jetzt auf dem private event passieren
	}

	private void Events_TestEvent(object sender, EventArgs e) => Console.WriteLine("TestEvent ausgeführt");

	private void Events_ArgsEvent(object sender, TestEventArgs e) => Console.WriteLine(e.Value);

	private void Events_IntEvent(object sender, int e) => Console.WriteLine(e);

	private void Events_AccessorEvent(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}
}

public class TestEventArgs : EventArgs
{
	public string Value { get; set; }
}