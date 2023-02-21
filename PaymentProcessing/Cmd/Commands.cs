namespace PaymentProcessing;

public static class Commands
{
  public static readonly CommandInfo[] commandInfos = new CommandInfo[] {
    new ClearCommand(),
    new ResetCommand(),
    new StartCommand(),
    new StopCommand(),
    new HelpCommand(),
  };

  public static void Start()
  {
    StaticData.IsWorking = true;
    if (!StaticData.IsWorking)
    {
      Console.WriteLine($"Error: config.json is not avaible file or not write properties '{StaticData.PROP_DIR_LOAD}', '{StaticData.PROP_DIR_SAVE}', '{StaticData.PROP_DIR_SUBDIR}'");
    }
  }

  public static void Stop()
  {
    StaticData.IsWorking = false;
  }

  public static void Reset()
  {
    Stop();

    StaticData.MetaLog.Reset();

    Start();
  }

  public static void Clear()
  {
    Console.Clear();
  }

  public static void Help()
  {
    foreach (var info in commandInfos)
    {
      Console.WriteLine($"{info.name,-7}  {info.description}");
    }
  }

  public static void StartInputCommand()
  {
    Console.WriteLine($"Please enter command ({string.Join(", ", commandInfos.Select(e => e.name))})");
    while (true)
    {
      string text = Console.ReadLine().Trim().ToLower();

      foreach (var info in commandInfos)
      {
        if (info.name == text)
        {
          info.Execute();
          break;
        }
      }
    }
  }
}