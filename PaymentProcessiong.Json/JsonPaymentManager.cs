using PaymentProcessing.Entities;

namespace PaymentProcessing.Json
{
  public static class JsonPaymentManager
  {
    public static IEnumerable<JsonPayment> Create(IEnumerable<Payment> payments)
    {
      Dictionary<string, JsonPayment> elems = new Dictionary<string, JsonPayment>();

      foreach (Payment pay in payments)
      {
        string city = pay.address.city;
        if (!elems.ContainsKey(city))
        {
          elems[city] = new JsonPayment(city);
        }

        elems[city].Add(pay);
      }

      return elems.Values;
    }
  }
}