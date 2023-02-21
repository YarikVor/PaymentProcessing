using PaymentProcessing.Entities;
using PaymentProcessing.Json;

namespace PaymentProcessing
{
  public static class PaymentController
  {
    public static void SaveMetaLog()
    {
      if (StaticData.MetaLog.parsedFiles != 0)
        MetaLogManager.WriteInFile(Path.Combine(StaticData.PathSave, StaticData.PathSubDir), StaticData.MetaLog);
    }

    public static void SavePaymants(string basePath)
    {
      StreamReader reader = new StreamReader(basePath);

      if (Path.GetExtension(basePath).ToLower() == ".csv")
      {
        reader.ReadLine();
      }

      PaymentInfo info = new PaymentInfo();

      MetaLog metaLog = StaticData.MetaLog;

      int beforeError = metaLog.foundErrors;
      PaymentManager.ConvertFromStreamReader(reader, metaLog, info);

      if (beforeError != metaLog.foundErrors)
      {
        metaLog.AddInvalidFiles(Path.GetFileName(basePath));
      }

      PaymentJson.SaveOutput(Path.Combine(StaticData.PathSave, StaticData.PathSubDir), info);

      reader.Dispose();
    }
  }
}