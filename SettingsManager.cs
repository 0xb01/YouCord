using System.Text.Json;

public class AppSettings
{
    public string GlobalWebhookUrl { get; set; } = "";
}

public static class SettingsManager
{
    private static string SettingsPath => Path.Combine(Application.StartupPath, "discord.json");

    public static void SaveWebhook(string url)
    {
        var settings = new AppSettings { GlobalWebhookUrl = url };
        string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsPath, json);
    }

    public static string LoadWebhook()
    {
        if (!File.Exists(SettingsPath)) return "";
        try
        {
            string json = File.ReadAllText(SettingsPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(json);
            return settings?.GlobalWebhookUrl ?? "";
        }
        catch
        {
            return "";
        }
    }
}