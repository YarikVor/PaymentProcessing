using PaymentProcessing.Entities;

namespace PaymentProcessing.Json;

public sealed class JsonService
{
  public readonly string name;
  public List<JsonPayer> payers = new List<JsonPayer>();

  internal JsonService(string name) => this.name = name;

  public decimal total => payers.Sum(e => e.payment);

  internal void Add(Payment payer) => payers.Add((JsonPayer)payer);
}