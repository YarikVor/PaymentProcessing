using PaymentProcessing.Entities;

namespace PaymentProcessing.Json;

public sealed class JsonPayment
{
  public readonly string city;
  public readonly List<JsonService> services = new List<JsonService>();

  internal JsonPayment(string city) => this.city = city;

  public decimal total => services.Sum(e => e.total);

  internal void Add(Payment payment)
  {
    JsonService? jsonService;
    jsonService = services.FirstOrDefault(e => e.name == payment.service);
    if (jsonService == null)
    {
      jsonService = new JsonService(payment.service);
      services.Add(jsonService);
    }
    jsonService.Add(payment);
  }
}