using System.Diagnostics;

namespace TestProject1;

[TestClass]
public class PaymentProcessingJsonTests
{
  [TestMethod]
  public void TestMethod1()
  {
    string text = @"John, Doe, ""Lviv, Kleparivska 35, 4"", 500.0, 2022-2701, 1234567, Water
Mike, Wiksen, ""Lviv, Kleparivska 40, 1"", 720.0, 2022-27-05, 7654321, Water
Nick, Potter, ""Lviv, Gorodotska 120, 3"", 880.0, 2022-25-03, ""3334444"", Water
Luke Pan,, ""Lviv, Gorodotska 120, 5"", 40.0, 2022-12-07, 2222111, Water";

    Debugger.Break();
  }

  [TestMethod]
  public void TestMethod2()
  {
    Debugger.Break();
  }
}