namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellung(string name); //Definition eines Delegates

	static void Main(string[] args)
	{
		//Delegates
		//Eigener Typ, der einen Methodenaufbau vorgibt (Rückgabewert, Generics, Parameter)
		//An dieses Delegate können Methoden angehängt werden
		//Das Delegate kann im Anschluss ausgeführt werden

		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung des Delegates mit Initialmethode
		v("Max"); //Hier werden alle Methoden, die an dem Delegate angehängt sind, ausgeführt

		v += new Vorstellung(VorstellungEN); //Neue Methode an das bestehende Delegate anhängen
		v += VorstellungEN; //Kurzform
		v("Udo"); //3 Outputs (1x DE, 2x EN)

		v -= VorstellungDE; //Delegates abziehen
		v("Tim");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v("Max"); //Wenn alle Methoden abgezogen werden, ist das Delegate null

		if (v != null)
			v("Max");

		v?.Invoke("Max"); //Null propagation: Führe den Code nach dem Fragezeichen nur aus, wenn die Variable nicht null ist

		foreach (Delegate dg in v.GetInvocationList()) //Welche Methoden hängen an dem Delegate?
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}
