using PaymentProcessing.Entities;

namespace PaymentProcessing.Json.UTests
{
  internal static class Methods
  {
    public static Payment Clone(this Payment obj)
    {
      Payment clone = new Payment()
      {
        payment = obj.payment,
        accountNumber = obj.accountNumber,
        address = obj.address,
        date = obj.date,
        firstName = obj.firstName,
        lastName = obj.lastName,
        service = obj.service,
      };

      return clone;
    }

    public static Payment SetPayment(this Payment obj, decimal payment)
    {
      obj.payment = payment;
      return obj;
    }

    public static Payment SetAddress(this Payment obj, Address address)
    {
      obj.address = address;
      return obj;
    }

    public static Payment SetName(this Payment obj, string name)
    {
      obj.firstName = name;
      return obj;
    }

    public static Payment SetService(this Payment obj, string service)
    {
      obj.service = service;
      return obj;
    }
  }
}