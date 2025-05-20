namespace DelegatesEvents;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action und Func
		//Vordefinierte Delegate Typen
		//Werden in C# an vielen Stellen verwendet
		//U.a. TPL, Linq, Reflection, ...

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//Action
		//Methode mit void als Rückgabewert, und bis zu 16 Parametern

		//Action ohne Parameter
		Action a = Test;
		a?.Invoke();

		//Action mit Parameter
		Action<int, int> addiere = Addiere;
		addiere?.Invoke(3, 5);

		//Anwendung
		//In der Realität werden Delegates fast ausschließlich als Parameter von Methoden verwendet

		//Beispiel: List.ForEach
		List<int> zahlen = Enumerable.Range(0, 10).ToList();
		zahlen.ForEach(PrintElement); //Geht die Liste durch, und führt die gegebene Action für jedes Element aus
		zahlen.ForEach(PrintElementMal2);
	
		//Innerhalb der ForEach-Funktion:
		foreach (int i in zahlen)
		{
			PrintElement(i); //Hier kann bei der ForEach Funktion selbst ein beliebiges Delegate übergeben werden
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//Func
		//Methode mit T als Rückgabewert, und bis zu 16 Parametern
		//WICHTIG: Der letzte Generic-Parameter einer Func ist immer der Rückgabetyp

		//Func ohne Parameter
		Func<DateTime> f = Heute;
		DateTime dt1 = f.Invoke(); //Hier wird bei Invoke ein Wert zurückgegeben
		DateTime dt2 = f?.Invoke() ?? DateTime.MinValue; //Bei ?.Invoke kann null zurückkommen, wenn f null ist

		//Func mit Parameter
		Func<int, int, double> dividiere = Dividiere;
		double d1 = dividiere.Invoke(8, 5);
		double d2 = dividiere?.Invoke(8, 5) ?? double.NaN;

		//Anwendung
		//In der Realität werden Delegates fast ausschließlich als Parameter von Methoden verwendet

		//Beispiel: List.Where
		zahlen.Where(TeilbarDurch2);

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//Anonyme Methoden
		//Methoden, welche keine dedizierte Methode definiert haben, sondern nur einmalig verwendet werden
		//Können auch in Delegates gespeichert werden

		//Schreibweisen
		Func<int, int, double> div;
		
		div = delegate (int x, int y)
		{
			return (double) x / y;
		};

		div += (int x, int y) =>
		{
			return (double) x / y;
		};

		div += (int x, int y) => (double) x / y;

		div += (x, y) => (double) x / y;

		//Anonyme Methoden können bei beliebigen Delegates eingesetzt werden
		zahlen.ForEach(e => Console.WriteLine(e));
		zahlen.ForEach(Console.WriteLine); //Normale Anwendung von Delegates
		zahlen.Where(e => e % 2 == 0);
	}

	#region Action
	static void Test() => Console.WriteLine("Test ausgeführt");

	static void Addiere(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");

	static void PrintElement(int element) => Console.WriteLine($"Zahl: {element}");

	static void PrintElementMal2(int element) => Console.WriteLine($"Zahl: {element * 2}");
	#endregion

	#region Func
	static DateTime Heute() => DateTime.Now;

	static double Dividiere(int x, int y) => (double) x / y;

	static bool TeilbarDurch2(int x) => x % 2 == 0;
	#endregion
}