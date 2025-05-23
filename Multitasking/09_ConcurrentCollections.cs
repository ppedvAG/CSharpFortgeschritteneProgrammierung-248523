using System.Collections.Concurrent;

namespace Multitasking;

internal class _09_ConcurrentCollections
{
	static void Main(string[] args)
	{
		//ConcurrentCollections
		//Listentypen, welche automatisch locken
		ConcurrentDictionary<string, int> dict = [];
		if (dict.TryAdd("Eins", 1))
		{
			//Kein normales Add, da ein anderer Task bereits das Element hinzugefügt haben könnte
		}

		//SynchronizedCollection
		//Bag == Liste (außer Index)
		//SC hat einen Index
		ConcurrentBag<int> zahlen = [];
		zahlen.Add(1);

		//Benötigt externes Paket: System.ServiceModel.Primitives
		SynchronizedCollection<int> ints = [];
		ints.Add(1);
		Console.WriteLine(ints[0]);
	}
}
