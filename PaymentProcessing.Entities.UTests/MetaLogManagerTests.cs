using System.Text.RegularExpressions;

namespace PaymentProcessing.Entities.UTests
{
  [TestClass]
  public class MetaLogManagerTests
  {
    [TestMethod]
    public void ToLogString_ValidArgs_ValidConvert()
    {
      MetaLog metaLog = new MetaLog();
      metaLog.AddInvalidFiles("file");

      string text = MetaLogManager.ToLogString(metaLog);
      Assert.IsTrue(Regex.IsMatch(text, @"(\s*\w+:\s*\d){3}\s*\w+: \[[^]]+]", RegexOptions.Multiline), text);
    }
  }
}