using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConfigJson
{
  public static class ConfigManager
  {
    public const string FILE_NAME = "config.json";

    private static string GetText()
    {
      if (!File.Exists(FILE_NAME))
      {
        File.Create(FILE_NAME).Dispose();
        return "";
      }
      else
      {
        return File.ReadAllText(FILE_NAME);
      }
    }

    public static string GetValue(string key)
    {
      string text = GetText();

      JObject? jObject = JsonConvert.DeserializeObject(text) as JObject;
      return jObject?[key]?.Value<string>() ?? "";
    }

    public static void SetValue(string key, string value)
    {
      string text = GetText();

      JObject jObject = JsonConvert.DeserializeObject(text) as JObject;
      jObject[key] = value;
      File.WriteAllText(FILE_NAME, JsonConvert.SerializeObject(jObject, Formatting.Indented));
    }
  }
}