using ConfigJson;
using PaymentProcessing.Entities;
using System.Text.RegularExpressions;

namespace PaymentProcessing;

public static class StaticData
{
  public const string PROP_DIR_LOAD = "pathLoading"; // A
  public const string PROP_DIR_SAVE = "pathSaved";   // B
  public const string PROP_DIR_SUBDIR = "pathSubDir";// C
  //public const string PROP_DIR_SUBDIR = "pathSubDir";//

  public static string PathLoad
  {
    get => ConfigManager.GetValue(PROP_DIR_LOAD);
    set => ConfigManager.SetValue(PROP_DIR_LOAD, value);
  }

  public static string PathSave
  {
    get => ConfigManager.GetValue(PROP_DIR_SAVE);
    set => ConfigManager.SetValue(PROP_DIR_SAVE, value);
  }

  public static string PathSubDir
  {
    get
    {
      string res = ConfigManager.GetValue(PROP_DIR_SUBDIR);
      if (!Regex.IsMatch(res, @"(\d{1,2}-){2}\d{4}"))
      {
        return UpdateSubDir();
      }
      return ConfigManager.GetValue(PROP_DIR_SUBDIR);
    }
    set => ConfigManager.SetValue(PROP_DIR_SUBDIR, value);
  }

  public static string UpdateSubDir()
  {
    return PathSubDir = DateTime.Today.ToString("MM-dd-yyyy");
  }

  public static readonly MetaLog MetaLog = new MetaLog();

  private static bool isWorking = false;

  public static bool IsWorking
  {
    get => isWorking;
    set
    {
      if (isWorking == false && value == true)
      {
        if (!string.IsNullOrEmpty(PathLoad) && !string.IsNullOrEmpty(PathSave) && !string.IsNullOrEmpty(PathSubDir))
        {
          isWorking = true;
        }
      }
      else
      {
        isWorking = value;
      }
    }
  }
}