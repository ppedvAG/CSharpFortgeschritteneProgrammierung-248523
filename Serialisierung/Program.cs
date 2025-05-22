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
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			xml.Serialize(sw, fahrzeuge);
		}

		using (StreamReader sr = new StreamReader(filePath))
		{
			List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(sr);
		}

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
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

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