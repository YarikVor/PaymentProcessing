using PaymentProcessing.Entities;

namespace PaymentProcessing.Json.UTests;

[TestClass]
public class JsonPaymentManagerTests
{
  public static Address[] Addresses = new Address[] {
    new Address("Lviv", "street", 2, 2),
    new Address("Kyiv", "street", 2, 2),
  };

  public static Address FirstAddress => Addresses[0];
  public static Address LastAddress => Addresses[1];

  public static Payment[] Payments = new Payment[]
  {
    new(){accountNumber = 1, firstName = "Yarik", payment = 50, service = "Eating", address = FirstAddress},
    new(){accountNumber = 2, firstName = "George", payment = 120, service = "Eating", address = FirstAddress},
  };

  public static Payment FirstPayment => Payments[0].Clone();
  public static Payment LastPayment => Payments[1].Clone();

  [TestMethod]
  public void Create_AddingPrice_IsValid()
  {
    decimal[] price = { 50m, 100m };
    var arr = new Payment[]
    {
      FirstPayment.SetPayment(price[0]),
      LastPayment.SetPayment(price[1])
    };

    var res = JsonPaymentManager.Create(arr);

    Assert.AreEqual(price.Sum(), res.Sum(e => e.total));
  }

  [TestMethod]
  public void Create_SameAddressAndSameServise_IsInOneCityAndServise()
  {
    var arr = new Payment[]
    {
      FirstPayment,
      LastPayment
    };

    var res = JsonPaymentManager.Create(arr).ToArray();

    Assert.AreEqual(1, res.Length);
    Assert.AreEqual(2, res[0].services[0].payers.Count);
  }

  [TestMethod]
  public void Create_SameAddressAndDiffServise_IsInOneCityAndAnotherServise()
  {
    var arr = new Payment[]
    {
      FirstPayment.SetService("1"),
      LastPayment.SetService("2")
    };

    var res = JsonPaymentManager.Create(arr).ToArray();

    Assert.AreEqual(1, res.Length);
    Assert.AreEqual(2, res[0].services.Count);
  }

  [TestMethod]
  public void Create_DiffAddress_IsInAnotherCity()
  {
    var arr = new Payment[]
    {
      FirstPayment.SetAddress(FirstAddress),
      LastPayment.SetAddress(LastAddress)
    };

    var res = JsonPaymentManager.Create(arr).ToArray();

    Assert.AreEqual(2, res.Length);
  }
}