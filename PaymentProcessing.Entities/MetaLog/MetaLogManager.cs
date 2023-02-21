using System.Text;
using System.Text.RegularExpressions;

namespace PaymentProcessing.Entities;

public static class MetaLogManager
{
  public const string LOG_PATTERN = "^(\\w*):\\s*(.*)$";
  private const string PROP_PARSED_FILES = "parsed_files";
  private const string PROP_PARSED_LINES = "parsed_lines";
  private const string PROP_FOUND_ERRORS = "found_errors";
  private const string PROP_INVALID_FILES = "invalid_files";

  private const string FORMAT_OUTPUT_LOG = "meta.log";

  public static string GetPathFile(string pathSave)
  {
    string path = Path.Combine(pathSave, FORMAT_OUTPUT_LOG);
    string dir = Path.GetDirectoryName(path) ?? "";

    if (!Directory.Exists(dir))
      Directory.CreateDirectory(dir);

    return path;
  }

  public static MetaLog ReadFromFile(string path)
  {
    string text = File.ReadAllText(path);
    return Read(text);
  }

  private static Dictionary<string, string> ReadAsDictionary(string text)
  {
    var res = new Dictionary<string, string>();

    foreach (Match match in Regex.Matches(text, LOG_PATTERN, RegexOptions.Multiline))
    {
      res[match.Groups[1].Value] = match.Groups[2].Value;
    }

    return res;
  }

  private static MetaLog Read(Dictionary<string, string> dictionary)
  {
    var res = new MetaLog();

    res.parsedFiles = int.Parse(dictionary[PROP_PARSED_FILES]);
    res.parsedLines = int.Parse(dictionary[PROP_PARSED_LINES]);
    res.foundErrors = int.Parse(dictionary[PROP_FOUND_ERRORS]);
    res.invalidFiles.AddRange(dictionary[PROP_INVALID_FILES].Split(',').Select(e => e.Trim(new char[] { ' ', '[', ']' })));

    return res;
  }

  private static MetaLog Read(string text)
  {
    var dictionary = ReadAsDictionary(text);
    return Read(dictionary);
  }

  public static void WriteInFile(string pathSave, MetaLog metaLog)
  {
    string text = ToLogString(metaLog);
    string path = GetPathFile(pathSave);
    File.WriteAllText(path, text);
  }

  public static string ToLogString(MetaLog metaLog)
  {
    StringBuilder sb = new StringBuilder();
    sb.AppendLine($"{PROP_PARSED_FILES}: {metaLog.parsedFiles}");
    sb.AppendLine($"{PROP_PARSED_LINES}: {metaLog.parsedLines}");
    sb.AppendLine($"{PROP_FOUND_ERRORS}: {metaLog.foundErrors}");
    sb.Append($"{PROP_INVALID_FILES}: [");
    sb.AppendJoin(", ", metaLog.invalidFiles.Select(e => $"\"{e}\""));
    sb.Append(']');

    return sb.ToString();
  }
}