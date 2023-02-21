using PaymentProcessing.Entities;

namespace PaymentProcessing.Json;

public sealed class JsonPayer
{
  public readonly string name;
  public readonly decimal payment;
  public readonly DateOnly date;
  public readonly long account_number;

  private JsonPayer(string name, decimal payment, DateOnly date, long account_number)
  {
    this.name = name;
    this.payment = payment;
    this.date = date;
    this.account_number = account_number;
  }

  public static explicit operator JsonPayer(Payment payer) => new JsonPayer(payer.firstName, payer.payment, payer.date, payer.accountNumber);
}