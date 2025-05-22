using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		//Dateiverwaltung: Path, Directory, File
		string folderPath = "Output";

		string filePath = Path.Combine(folderPath, "Test.json");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
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

		//Newtonsoft.Json

		//1. Serialisierung/Deserialisierung
		//2. Settings/Options
		//3. Attribute
		//4. Json per Hand

		//1. Serialisierung/Deserialisierung
		string json = JsonConvert.SerializeObject(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> fzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson);

		//2. Settings/Options
		JsonSerializerSettings settings = new JsonSerializerSettings();
		settings.Formatting = Newtonsoft.Json.Formatting.Indented;
		settings.TypeNameHandling = TypeNameHandling.Objects;

		File.WriteAllText(filePath, JsonConvert.SerializeObject(fahrzeuge, settings));

		//3. Attribute
		//JsonIgnore
		//JsonExtensionData: Fängt alle Felder aus der Json Datei, die in der Klasse kein entsprechendes Property haben
		//Vererbung: Setting

		//4. Json per Hand
		JToken doc = JToken.Parse(readJson);
		foreach (JToken element in doc)
		{
			Console.WriteLine(element["MaxV"].Value<int>());
			Console.WriteLine((FahrzeugMarke) element["Marke"].Value<int>());
			Console.WriteLine("--------------------------------");
		}
	}

	public static void XML()
	{
		//Dateiverwaltung: Path, Directory, File
		string folderPath = "Output";

		string filePath = Path.Combine(folderPath, "Test.xml");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//XML
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
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

		//1. Serialisierung/Deserialisierung
		XmlSerializer xml = new(fahrzeuge.GetType());
		//using (StreamWriter sw = new StreamWriter(filePath))
		//{
		//	xml.Serialize(sw, fahrzeuge);
		//}
		xml.Serialize(filePath, fahrzeuge); //Mit Erweiterungsmethode

		//using (StreamReader sr = new StreamReader(filePath))
		//{
		//	List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(sr);
		//}
		xml.Deserialize<List<Fahrzeug>>(filePath); //Mit Erweiterungsmethode

		//2. Attribute
		//XmlIgnore
		//XmlAttribute: Feld wird in der Attributschreibweise geschrieben
		//XmlInclude: Vererbung

		//3. XML per Hand
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		foreach (XmlNode node in doc.DocumentElement)
		{
			Console.WriteLine(node["MaxV"].InnerText);
			Console.WriteLine(node["Marke"].InnerText);
			Console.WriteLine("------------------------");
		}
	}

	public static void SystemJson()
	{
		////Dateiverwaltung: Path, Directory, File
		//string folderPath = "Output";

		//string filePath = Path.Combine(folderPath, "Test.json");

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		//{
		//	new PKW(251, FahrzeugMarke.BMW),
		//	new Fahrzeug(274, FahrzeugMarke.BMW),
		//	new Fahrzeug(146, FahrzeugMarke.BMW),
		//	new Fahrzeug(208, FahrzeugMarke.Audi),
		//	new Fahrzeug(189, FahrzeugMarke.Audi),
		//	new Fahrzeug(133, FahrzeugMarke.VW),
		//	new Fahrzeug(253, FahrzeugMarke.VW),
		//	new Fahrzeug(304, FahrzeugMarke.BMW),
		//	new Fahrzeug(151, FahrzeugMarke.VW),
		//	new Fahrzeug(250, FahrzeugMarke.VW),
		//	new Fahrzeug(217, FahrzeugMarke.Audi),
		//	new Fahrzeug(125, FahrzeugMarke.Audi)
		//};

		////System.Text.Json

		////1. Serialisierung/Deserialisierung
		////2. Settings/Options
		////3. Attribute
		////4. Json per Hand

		////1. Serialisierung/Deserialisierung
		//string json = JsonSerializer.Serialize(fahrzeuge);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//List<Fahrzeug> fzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson);

		////2. Settings/Options
		//JsonSerializerOptions options = new();
		//options.WriteIndented = true;

		////WICHTIG: Bei Serialisierung/Deserialisierung muss das Objekt mitgegeben werden
		//File.WriteAllText(filePath, JsonSerializer.Serialize(fahrzeuge, options));

		////3. Attribute
		////JsonIgnore
		////JsonDerivedType: Vererbung
		////JsonExtensionData: Fängt alle Felder aus der Json Datei, die in der Klasse kein entsprechendes Property haben
		//string readJson2 = File.ReadAllText(filePath);
		//List<Fahrzeug> fzg2 = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson2); //Zusätzliche Felder werden in Extra hineingeschrieben

		////4. Json per Hand
		//JsonDocument doc = JsonDocument.Parse(readJson2);

		//foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	Console.WriteLine(element.GetProperty("MaxV").GetInt32());
		//	Console.WriteLine((FahrzeugMarke) element.GetProperty("Marke").GetInt32());
		//	Console.WriteLine("--------------------------------");
		//}
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]

//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object> Extra { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public Fahrzeug()
	{
		
	}
}

public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke) { }

	public PKW()
	{
		
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }

public static class XmlExtensions
{
	public static void Serialize(this XmlSerializer xml, string path, object o)
	{
		using StreamWriter sw = new StreamWriter(path);
		xml.Serialize(sw, o);
	}

	public static T Deserialize<T>(this XmlSerializer xml, string path)
	{
		using (StreamReader sr = new StreamReader(path))
		return (T) xml.Deserialize(sr);
	}
}