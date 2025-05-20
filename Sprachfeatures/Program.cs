namespace Sprachfeatures;

internal unsafe class Program
{
	static unsafe void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		object o = 123.45;

		if (o is int i) //Macht automatisch einen Typecast
		{
			int zahl = (int) o; //Wird generell immer nach einem is gemacht
			Console.WriteLine(zahl * 2);
		}

		//Mehrere Werte in einer Variable
		int[] x = { 1, 2, 3 };
		Console.WriteLine(x[0]);
		Console.WriteLine(x[1]);
		Console.WriteLine(x[2]);

		//Tupel
		//Mehrere Werte speichern, aber jeder Wert hat einen Namen
		(int laenge, int breite, int hoehe) = (1, 2, 3);
		Console.WriteLine(laenge);
		Console.WriteLine(breite);
		Console.WriteLine(hoehe);

		void Test()
		{

		}

		long l = 2_189_489_124_901_248;
		Console.WriteLine(l);

		//class und struct

		//class
		//Referenztyp
		//1. Wenn ein Objekt einer Klasse zugewiesen wird (Variable), wird eine Referenz erstellt

		//Person p1 = new Person("Max"); //Objekt erzeugen und in p1 schreiben
		//Person p2 = p1; //Referenz auf p1 erzeugen
		//p2.Name = "Tim"; //Der Name von beiden Objekten wird geändert, weil unter p1 und p2 das gleiche Objekt liegt

		////2. Wenn zwei Objekte von Klassen verglichen werden, werden die Speicheradressen verglichen
		//Console.WriteLine(p1.GetHashCode());
		//Console.WriteLine(p2.GetHashCode());

		//Console.WriteLine(p1 == p2);
		//Console.WriteLine(p1.GetHashCode() == p2.GetHashCode());

		//struct
		//Wertetyp
		//1. Wenn ein Objekt eines Structs zugewiesen wird (Variable), wird eine Kopie erstellt
		int a = 10; //Erzeugt eine Zahl mit dem Wert 10
		int b = a; //Kopiert die Zahl 10 in b
		b = 20; //Ändert b auf 20 (a bleibt unverändert)

		//2. Wenn zwei Objekte von Structs verglichen werden, werden die Inhalte verglichen
		Console.WriteLine(a == b);

		//ref
		//Beliebige Typen referenzierbar machen
		int c = 10;
		ref int d = ref c; //Referenz auf c legen
		d = 20;

		unsafe
		{

		}

		//Ohne switch-Pattern
		string tag2;
		switch (DateTime.Now.DayOfWeek)
		{
			case DayOfWeek.Monday:
				tag2 = "Montag";
				break;
			case DayOfWeek.Tuesday:
				tag2 = "Dienstag";
				break;
			default:
				tag2 = "Anderer Tag";
				break;
		}

		//Mit switch-Pattern
		string tag1 = DateTime.Now.DayOfWeek switch
		{
			DayOfWeek.Monday => "Montag",
			DayOfWeek.Tuesday => "Dienstag",
			_ => "Anderer Tag",
		};

		//using
		//Schließt externe Resourcen automatisch, wenn diese nicht mehr benötigt werden
		StreamWriter sw = new StreamWriter("File.txt");
		//...
		sw.Close(); //Resource wieder freigeben

		using (StreamWriter sw2 = new StreamWriter("File.txt"))
		{
			//...
			//sw2.Close(); //Kann weggelassen werden
		} //Hier wird automatisch .Dispose() ausgeführt

		using StreamWriter sw3 = new StreamWriter("File.txt"); //.Dispose() wird am Ende der Methode gemacht

		//Sollte IMMER bei Dateien, Datenbanken und Webschnittstellen verwendet werden
		using HttpClient client = new HttpClient();

		static void Test2()
		{
			//Console.WriteLine(tag1); //Nicht erlaubt
		}

		int[] ints = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
		Console.WriteLine(ints[^1]);
		foreach (int j in ints[2..6])
		{
			Console.WriteLine(j);
		}

		//Null-Coalescing Operator
		//Nimm die Linke Seite, wenn diese nicht null ist, sonst nimm die rechte Seite
		List<int> zahlen = null;

		if (zahlen == null)
			zahlen = new List<int>();

		zahlen = zahlen == null ? new List<int>() : zahlen;

		zahlen = zahlen ?? new List<int>();

		zahlen ??= new List<int>(); //Erstellt eine neue Liste, wenn diese noch nicht existiert

		//String Interpolation ($-String): Code in einen String einbauen

		//Beispiel: a, b und tag1 in einem Output darstellen
		string kombination = "Der Wert von a ist: " + a + ", der Wert von b ist: " + b + ", heute ist " + tag1;
		Console.WriteLine(kombination);

		string interpolation = $"Der Wert von a ist: {a}, der Wert von b ist: {b}, heute ist {tag1}";
		Console.WriteLine(interpolation);

		Console.WriteLine(kombination == interpolation);

		//Verbatim-String (@-String): Ignoriert Escape-Sequenzen
		string pfad = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\9.0.5\System.Collections.Concurrent.dll";
		Console.WriteLine(pfad);

		Console.WriteLine(@"\n\t\\""");

		var dict1 = new Dictionary<string, int>();
		Dictionary<string, int> dict2 = new();

		Person p = new Person(1, "Max");
		//p.ID = 10;
		Console.WriteLine(p);

		string u1 = $"Das ist ein doppeltes Hochkomma: [\"]";
		string u2 = $"""Das ist ein doppeltes Hochkomma: ["]""";
		string u3 = $" {{ ";

		Point point = new Point(1, 2, 3);
		Console.WriteLine(point.X);

		Summiere(1, 2, 3);
		Summiere(1, 2, 3, 4, 5);
		Summiere(1);
		Summiere();

		DateTime now = DateTime.Now;
		DateTime ka = new DateTime(2000, 01, 01);
		Console.WriteLine(now < ka);

		int r = 10;
		double f = r;

		r = (int) f;

		var t = new { ID = p.ID, Name = p.Name, U1 = u1, U2 = u2, U3 = u3 };
		Console.WriteLine(t.ID);

		//string gesamt = string.Empty;
		//for (int g = 0; g < 100000; g++)
		//	gesamt += g.ToString();

		//6.5s

		StringBuilder sb = new StringBuilder();
		for (int g = 0; g < 100_000; g++)
			sb.Append(g);
		string gesamt = sb.ToString();

		//2ms
	}

	public string TagZuString() => DateTime.Now switch
	{
		{ DayOfWeek: DayOfWeek.Monday } => "Montag",
		{ DayOfWeek: DayOfWeek.Tuesday } => "Dienstag",
		{ DayOfWeek: DayOfWeek.Friday, Day: 13 } => "Freitag der 13.",
		_ => "Anderer Tag",
	};

	public static void Summiere(params List<int> zahlen)
	{
		Console.WriteLine(zahlen.Sum());
	}
}

//public class Person
//{
//	public int ID { get; set; }

//	public string Name { get; set; }

//	public Person(int id, string name)
//	{
//		ID = id;
//		Name = name;
//	}
//}

//Nachteile:
//- Records können nach Instanziierung nicht mehr geändert werden
//- Properties können nicht mit Attributen versehen werden
public record Person(int ID, string Name)
{
	public void Test()
	{

	}
}

public class Point(int x, int y, int z)
{
	public int X = x;

	public int Y = y;

	public int Z = z;
}