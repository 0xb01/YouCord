using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public class ChannelConfig
{
    public string ChannelId { get; set; }
    public string ChannelName { get; set; }
    public string AvatarUrl { get; set; } // 24x24 channel image

    public string VideoTitle { get; set; }
    public string VideoUrl { get; set; }
    public string ThumbnailUrl { get; set; }
    public long UploadTimestamp { get; set; } // Unix Timestamp
    public string VideoId { get; set; }

    [JsonIgnore]
    public Image ChannelAvatarImg { get; set; }

    [JsonIgnore]
    public Image VideoThumbnailImg { get; set; }
}

public static class YouTubeManager
{
    private static readonly HttpClient _client = new HttpClient();

    private static string ConfigPath => Path.Combine(Application.StartupPath, "channels.json");

    static YouTubeManager()
    {
        // Fake a browser to avoid 403 errors
        _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
    }

    public static async Task<string> GetChannelIdFromUrlAsync(string url)
    {
        var directMatch = Regex.Match(url, @"/channel/(UC[\w-]+)");
        if (directMatch.Success) return directMatch.Groups[1].Value;
        try
        {
            string html = await _client.GetStringAsync(url);
            var hrefMatch = Regex.Match(html, @"href=""[^""]*/channel/(UC[\w-]+)""");

            if (hrefMatch.Success)
            {
                return hrefMatch.Groups[1].Value;
            }
        }
        catch
        {
            return null;
        }

        return null;
    }

    public static async Task<ChannelConfig?> GetChannelInfoAsync(string channelId)
    {
        try
        {
            string rssUrl = $"https://www.youtube.com/feeds/videos.xml?channel_id={channelId}";
            string rssContent = await _client.GetStringAsync(rssUrl);
            var doc = XDocument.Parse(rssContent);

            XNamespace ns = "http://www.w3.org/2005/Atom";
            XNamespace media = "http://search.yahoo.com/mrss/";
            XNamespace yt = "http://www.youtube.com/xml/schemas/2015";

            var entry = doc.Root.Elements(ns + "entry").FirstOrDefault();
            if (entry == null) return null;

            string channelPageUrl = $"https://www.youtube.com/channel/{channelId}";
            string pageContent = await _client.GetStringAsync(channelPageUrl);
            var avatarMatch = Regex.Match(pageContent, "<meta property=\"og:image\" content=\"(.*?)\"");
            string avatarUrl = avatarMatch.Success ? avatarMatch.Groups[1].Value : "";

            string vidTitle = entry.Element(ns + "title")?.Value;
            string vidId = entry.Element(yt + "videoId")?.Value;
            string vidUrl = entry.Element(ns + "link")?.Attribute("href")?.Value;
            string thumbUrl = entry.Element(media + "group")?.Element(media + "thumbnail")?.Attribute("url")?.Value;
            var pubDate = DateTimeOffset.Parse(entry.Element(ns + "published")?.Value ?? DateTime.Now.ToString());

            Image avatarImg = null;
            Image thumbImg = null;

            if (!string.IsNullOrEmpty(avatarUrl))
            {
                var avBytes = await _client.GetByteArrayAsync(avatarUrl);
                using (var ms = new MemoryStream(avBytes)) avatarImg = Image.FromStream(ms);
            }

            if (!string.IsNullOrEmpty(thumbUrl))
            {
                var thBytes = await _client.GetByteArrayAsync(thumbUrl);
                using (var ms = new MemoryStream(thBytes)) thumbImg = Image.FromStream(ms);
            }

            return new ChannelConfig
            {
                ChannelId = channelId,
                ChannelName = doc.Root.Element(ns + "title")?.Value,
                AvatarUrl = avatarUrl,
                VideoTitle = vidTitle,
                VideoId = vidId,
                VideoUrl = vidUrl,
                ThumbnailUrl = thumbUrl,
                UploadTimestamp = pubDate.ToUnixTimeSeconds(),

                ChannelAvatarImg = avatarImg,
                VideoThumbnailImg = thumbImg
            };
        }
        catch
        {
            return null;
        }
    }

    public static async Task ReloadImagesAsync(ChannelConfig config)
    {
        try
        {
            if (!string.IsNullOrEmpty(config.AvatarUrl))
            {
                var aBytes = await _client.GetByteArrayAsync(config.AvatarUrl);
                using (var ms = new MemoryStream(aBytes)) config.ChannelAvatarImg = Image.FromStream(ms);
            }

            if (!string.IsNullOrEmpty(config.ThumbnailUrl))
            {
                var tBytes = await _client.GetByteArrayAsync(config.ThumbnailUrl);
                using (var ms = new MemoryStream(tBytes)) config.VideoThumbnailImg = Image.FromStream(ms);
            }
        }
        catch { }
    }

    public static async Task SendDiscordNotificationAsync(ChannelConfig channel)
    {
        string webhookUrl = SettingsManager.LoadWebhook();

        if (string.IsNullOrWhiteSpace(webhookUrl)) return;

        var payload = new
        {
            content = $"**{channel.ChannelName}** just uploaded a new video!",
            embeds = new[]
            {
                new
                {
                    title = channel.VideoTitle,
                    url = channel.VideoUrl,
                    color = 16711680, // red (decimal)
                    image = new { url = channel.ThumbnailUrl },
                    author = new { name = channel.ChannelName, icon_url = channel.AvatarUrl },
                    footer = new { text = "YouCord by Banikaz" },
                    timestamp = DateTime.UtcNow.ToString("o")
                }
            }
        };

        string json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            await _client.PostAsync(webhookUrl, content);
        }
        catch (Exception ex) { }
    }

    public static void SaveConfig(List<ChannelConfig> channels)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(channels, options);
        File.WriteAllText(ConfigPath, json);
    }

    public static List<ChannelConfig> LoadConfig()
    {
        if (!File.Exists(ConfigPath)) return new List<ChannelConfig>();
        string json = File.ReadAllText(ConfigPath);
        return JsonSerializer.Deserialize<List<ChannelConfig>>(json) ?? new List<ChannelConfig>();
    }
}