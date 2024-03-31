namespace OhMyDog_API.Controllers
{
    internal static class Environment
        {
            public static void LoadEnvironment()
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ConexaoOdbc.env";

                if (!File.Exists(filePath))
                    File.Create(filePath).Dispose();

                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                        continue;
                    System.Environment.SetEnvironmentVariable(parts[0], parts[1]);
                }
            }
        }
}
