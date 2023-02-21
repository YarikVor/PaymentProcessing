using System.Text;

namespace PaymentProcessing.Entities.UTests;

[TestClass]
public class PaymentManagerTests
{
  [TestMethod]
  public void ConvertRowCsv_ValidArg_ReturnedPayment()
  {
    string text = "John, Doe, \"Lviv, Kleparivska 35, 4\", 500.0, 2022-27-01, 1234567, Wate";

    var res = PaymentManager.ConvertRowCsv(text);
    var address = AddressManager.ConvertString("Lviv, Kleparivska 35, 4");
    Assert.AreEqual("John", res.firstName);
    Assert.AreEqual("Doe", res.lastName);
    Assert.AreEqual(address, res.address);
    Assert.AreEqual(500.0m, res.payment);
    Assert.AreEqual(new DateOnly(2022, 01, 27), res.date);
    Assert.AreEqual(1234567L, res.accountNumber);
    Assert.AreEqual("Wate", res.service);
  }

  [TestMethod]
  [ExpectedException(typeof(FormatException))]
  public void ConvertRowCsv_ValidArgAndInvalidAccountNumber_ThrowFormatException()
  {
    string text = "John, Doe, \"Lviv, Kleparivska 35, 4\", 500.0, 2022-27-01, 2ew, Wate";

    var res = PaymentManager.ConvertRowCsv(text);
  }

  [TestMethod]
  [ExpectedException(typeof(FormatException))]
  public void ConvertRowCsv_InvalidArg_ThrowFormatException()
  {
    string text = "3re w eweq, \" qeqwe";

    var res = PaymentManager.ConvertRowCsv(text);
  }

  [TestMethod]
  public void ConvertFromStreamReader_ValidArg_ReturnedTwoEPayments()
  {
    var text = "John, Doe, \"Lviv, Kleparivska 35, 4\", 500.0, 2022-27-01, 22, Water\nDama, Doe, \"Lviv, Kleparivska 35, 4\", 500.0, 2022-27-01, 222, Water";

    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text));

    var reader = new StreamReader(stream);

    MetaLog metaLog = new MetaLog();
    PaymentInfo info = new PaymentInfo();
    PaymentManager.ConvertFromStreamReader(reader, metaLog, info);

    Assert.AreEqual(2, metaLog.parsedLines);
    Assert.AreEqual(1, metaLog.parsedFiles);
    Assert.AreEqual(0, metaLog.foundErrors);

    Assert.AreEqual("John", info.payments[0].firstName);
    Assert.AreEqual("Dama", info.payments[1].firstName);

    reader.Dispose();
    stream.Dispose();
  }

  [TestMethod]
  public void ConvertFromStreamReader_InvalidArg_ReturnedTwoEPayments()
  {
    var text = "John, Doe, \"Lviv, Kleparivska 35, 4\", 500.0, 2022-27-01, 22e, Water\nDama, Doe, \"Lviv, Kleparivska 35, 4\", 500.0, 2022-27-01, 2e2, Water";

    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text));

    var reader = new StreamReader(stream);

    MetaLog metaLog = new MetaLog();
    PaymentInfo info = new PaymentInfo();
    PaymentManager.ConvertFromStreamReader(reader, metaLog, info);

    Assert.AreEqual(2, metaLog.parsedLines);
    Assert.AreEqual(1, metaLog.parsedFiles);
    Assert.AreEqual(2, metaLog.foundErrors);

    reader.Dispose();
    stream.Dispose();
  }
}