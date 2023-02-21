using PaymentProcessing.Entities;

namespace PaymentProcessing.Json
{
  public static class PaymentJson
  {
    public static int counter = 1;

    public const string FORMAT_OUTPUT_FILE = "output{0}.json";

    public static string GetPathFile(string path)
    {
      string combPath = Path.Combine(path, string.Format(FORMAT_OUTPUT_FILE, counter));
      string dir = Path.GetDirectoryName(combPath)!;

      if (!Directory.Exists(dir))
        Directory.CreateDirectory(dir);

      counter++;

      return combPath;
    }

    public static void SaveOutput(string path, List<Payment> payments)
    {
      var res = JsonPaymentManager.Create(payments).ToArray();

      var json = Newtonsoft.Json.JsonConvert.SerializeObject(res);

      string filePath = GetPathFile(path);

      File.WriteAllText(filePath, json);
    }

    public static void SaveOutput(string path, PaymentInfo info) => SaveOutput(path, info.payments);
  }
}