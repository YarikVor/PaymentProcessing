using PaymentProcessing.Entities;
using PaymentProcessing.Json;

namespace PaymentProcessing;

internal class Program
{
  private static void Main()
  {
    Console.Title = "PaymentProcessing: type command...";

    PaymentEventsDay.NewDay += PaymentTimers_NewDay;
    PaymentEventsFile.CreateFile += PaymentFileEvents_CreateFile;
    AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

    StaticData.UpdateSubDir();
    Commands.Start();

    Commands.StartInputCommand();
  }

  private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
  {
    PaymentTimers_NewDay();
  }

  private static void PaymentFileEvents_CreateFile(FileSystemEventArgs e)
  {
    Console.WriteLine($"Reading file: {e.FullPath}");

    PaymentController.SavePaymants(e.FullPath);
  }

  private static void PaymentTimers_NewDay()
  {
    var text = MetaLogManager.ToLogString(StaticData.MetaLog);
    Console.WriteLine(text);

    PaymentController.SaveMetaLog();

    StaticData.MetaLog.Reset();
    PaymentJson.counter = 1;
    StaticData.UpdateSubDir();
  }
}