Task.Run(() =>
{
	Task t1 = Task.Run(() => GeneriereLabel(ConfigManager.CurrentConfig.ID));
	Task t2 = Task.Run(() => GeneriereLabel(ConfigManager.CurrentConfig.Name));
	Task t3 = Task.Run(() => GeneriereLabel(ConfigManager.CurrentConfig.Bezahlt));

	Task.WaitAll(t1, t2, t3);
});

void GeneriereLabel(object o)
{
	//Erzeuge Label
	//Positionieren in der GUI
}

//////////////////////////////////////////////////////////////

int id = ConfigManager.CurrentConfig.ID;

public static class ConfigManager
{
	private static Config currentConfig;

	public static Config CurrentConfig => currentConfig;
}

public record Config(int ID, string Name, bool Bezahlt);