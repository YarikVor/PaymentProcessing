namespace PaymentProcessing;

public static class PaymentEventsFile
{
  private static FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

  public static event Action<FileSystemEventArgs> CreateFile;

  public static readonly string[] filters = { "*.txt", "*.csv" };

  static PaymentEventsFile()
  {
    Update();
  }

  private static void Update()
  {
    fileSystemWatcher.Path = Path.GetFullPath(StaticData.PathLoad);

    foreach (var filter in filters)
    {
      fileSystemWatcher.Filters.Add(filter);
    }

    fileSystemWatcher.Created += FileSystemWatcher_Created;

    fileSystemWatcher.EnableRaisingEvents = true;
  }

  private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
  {
    if (StaticData.IsWorking)
    {
      CreateFile?.Invoke(e);
    }
  }
}