using System.Collections;

namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		//Generics
		//Platzhalter für Typen
		//Muss bei Verwendung der Klasse/Methode ersetzt werden

		List<int> ints = new List<int>();
		ints.Add(1); //T wird durch int ersetzt

		List<string> strings = new List<string>();
		strings.Add("a"); //T wird durch string ersetzt
	}

	/// <summary>
	/// WICHTIG: Wenn eine Generische Methode in einer Klasse definiert wird, hat diese Methode Zugriff auf das Generic von der Klasse selbst
	/// D.h., hier muss kein Generic neu definiert werden
	/// </summary>
	static T Test<T>(T parameter) //Hier kann jetzt überall T verwendet werden, wo ein Typ stehen kann
	{
		T result = parameter;
		return result;
	}
}

public class DataStore<T> : IEnumerable<T> //T bei Vererbung
{
	private T[] _data; //Variablentyp

	public List<T> Data => _data.ToList(); //Als Generic in einer generische Klasse (List)

	public T Get(int index) //Rückgabewert
	{
		return _data[index];
	}

	public void Set(int index, T value) //Parameter
	{
		_data[index] = value;
	}

	public IEnumerator<T> GetEnumerator() //Vererbte Methode hat auch den Typparameter des vererbten Interfaces
	{
		return Data.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public T this[int index]
	{ 
		get => _data[index];
		set => _data[index] = value;
	}
}