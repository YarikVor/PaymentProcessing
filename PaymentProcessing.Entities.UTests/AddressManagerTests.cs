namespace PaymentProcessing.Entities.UTests
{
  [TestClass]
  public class AddressManagerTests
  {
    [TestMethod]
    public void ConvertString_ValidArg_ValidReturnAddress()
    {
      string input = "Lviv, Street 1, 2";

      var actual = AddressManager.ConvertString(input);

      Assert.AreEqual("Lviv", actual.city);
      Assert.AreEqual("Street", actual.street);
      Assert.AreEqual(1, actual.buildingNum);
      Assert.AreEqual(2, actual.apartmentNum);
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void ConvertString_ValidArgAndInvalidNumber_ThrowFormatException()
    {
      string input = "Lviv, Street e, q";

      AddressManager.ConvertString(input);
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void ConvertString_InvalidArg_ThrowFormatException()
    {
      string input = "333";

      AddressManager.ConvertString(input);
    }
  }
}