using System.Diagnostics;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Listentheorie
		//IEnumerable
		//Basis von allen Listen in C#
		//Ermöglicht Linq

		//WICHTIG: Ein IEnumerable ist nur eine Anleitung
		IEnumerable<int> zahlen = Enumerable.Range(0, 1_000_000_000); //1ms, zahlen enthält keine Daten

		IEnumerable<int> where = zahlen.Where(e => e % 2 == 0); //1ms, where enthält auch keine Daten

		//Jede Linq-Funktion die eine Liste als Ergebnis hat (z.B. Where), gibt immer ein IEnumerable zurück
		//Ausnahmen: ToList, ToDictionary, ToArray, ...

		//List<int> x = where.ToList(); //5s, hier wird die Anleitung ausgeführt (Daten werden erzeugt)

		//Wenn eine Anleitung verarbeitet wird (foreach, ToList, ...) wird diese ausgeführt

		//Konzept: Deferred Execution
		#endregion

		#region Einfaches Linq
		List<int> x = Enumerable.Range(1, 20).ToList();

		//Erweiterungsmethoden
		//Methoden, die an beliebige Typen angehängt werden können
		//Jede Linq Methode ist eine Erweiterungsmethode
		//Erkennung: Würfel mit Pfeil, (extension)
		Console.WriteLine(x.Sum());
		Console.WriteLine(x.Min());
		Console.WriteLine(x.Max());
		Console.WriteLine(x.Average());

		Console.WriteLine(x.First()); //Das erste Element der Liste, Exception wenn die Liste leer ist
		Console.WriteLine(x.FirstOrDefault()); //Das erste Element der Liste, default(T) wenn die Liste leer ist

		//Console.WriteLine(x.First(e => e % 50 == 0));
		Console.WriteLine(x.FirstOrDefault(e => e % 50 == 0));
		#endregion

		#region Linq mit Objekten
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//Aufgabe: Finde alle VWs
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW);

		//Liste sortieren
		fahrzeuge.OrderBy(e => e.Marke);
		fahrzeuge.OrderByDescending(e => e.Marke);

		//Subsequente Sortierung
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//All, Any

		//Fahren alle Fahrzeuge über 200km/h?
		if (fahrzeuge.All(e => e.MaxV >= 200))
		{

		}

		//Fährt mindestens ein Fahrzeug über 200km/h?
		if (fahrzeuge.Any(e => e.MaxV >= 200))
		{

		}

		//Beispiel: String auf Buchstaben prüfen
		string name = "Max";
		name.All(char.IsLetter);

		//Wieviele BMWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW); //4
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Count(); //4

		//Average, Min, Max, Sum
		//Bei diesen Funktion kann bei einer Objektliste ein Selektor angegeben werden
		fahrzeuge.Average(e => e.MaxV);
		fahrzeuge.Min(e => e.MaxV);
		fahrzeuge.Max(e => e.MaxV);
		fahrzeuge.Sum(e => e.MaxV);

		//fahrzeuge.Select(e => e.MaxV).Average(); //Unperformante Variante von Average(e => e.MaxV)

		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit
		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit

		//Skip, Take
		//Beispiel: Die 3 schnellsten Fahrzeuge
		fahrzeuge.OrderByDescending(e => e.MaxV).Take(3);

		//Beispiel: Webshop
		int page = 0;
		fahrzeuge.Skip(page * 10).Take(10);

		//Select
		//Transformiert einer Liste

		//Anwendungsfälle:
		//- Einzelnes Feld entnehmen (80%)
		//- Form der Elemente ändern (20%)

		//Welche Marken haben wir?
		fahrzeuge.Select(e => e.Marke); //Liste von allen Marken

		fahrzeuge
			.Select(e => e.Marke)
			.Distinct()
			.Order(); //Marken eindeutig

		fahrzeuge.Select(e => e.MaxV); //Liste von ints

		//Liste aller Dateinamen (ohne Endung und Pfad) aus einem Ordner entnehmen
		List<string> namen = [];
		foreach (string pfad in Directory.GetFiles("C:\\Windows"))
			namen.Add(Path.GetFileNameWithoutExtension(pfad));

		//Mit Select
		Directory.GetFiles("C:\\Windows").Select(Path.GetFileNameWithoutExtension).ToList(); //Gehe die Liste durch, führe die Funktion in Select bei jedem Element aus

		//SelectMany
		//Selbiges wie Select, führt zusätzlich eine Glättung durch
		List<List<int>> ints = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
		ints.SelectMany(e => e);

		//GroupBy
		//Gruppiert nach einem Kriterium
		IEnumerable<IGrouping<FahrzeugMarke, Fahrzeug>> group = fahrzeuge.GroupBy(e => e.Marke);

		//Konvertieren zu einem Dictionary
		Dictionary<FahrzeugMarke, IGrouping<FahrzeugMarke, Fahrzeug>> dict = group.ToDictionary(e => e.Key);

		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict2 = group.ToDictionary(k => k.Key, v => v.ToList());
		#endregion

		#region	Erweiterungsmethoden
		//Erweiterungsmethoden
		//Methoden, die an beliebige Typen angehängt werden können
		//Jede Linq Methode ist eine Erweiterungsmethode
		//Erkennung: Würfel mit Pfeil, (extension)
		int zahl = 10;
		zahl.Quersumme();

		Console.WriteLine(128476.Quersumme());

		//Generiert vom Compiler:
		ExtensionMethods.Quersumme(zahl);

		//Eigene Linq-Funktion
		fahrzeuge.Shuffle();
		#endregion
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

//public class Fahrzeug
//{
//	public int MaxV { get; set; }

//	public FahrzeugMarke Marke { get; set; }

//	public Fahrzeug(int maxV, FahrzeugMarke marke)
//	{
//		MaxV = maxV;
//		Marke = marke;
//	}
//}

public enum FahrzeugMarke { Audi, BMW, VW }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public static class ExtensionMethods
{
	public static int Quersumme(this int x) //this Parameter: Beschreibt den Typen, auf den sich die Methode bezieht
	{
		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> values)
	{
		return values.OrderBy(e => Random.Shared.Next());
	}
}