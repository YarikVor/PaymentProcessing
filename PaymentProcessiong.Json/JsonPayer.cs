using PaymentProcessing.Entities;

namespace PaymentProcessing.Json;

public class JsonPayer
{
  public string name;
  public decimal payment;
  public DateOnly date;
  public long account_number;

  private JsonPayer(string name, decimal payment, DateOnly date, long account_number)
  {
    this.name = name;
    this.payment = payment;
    this.date = date;
    this.account_number = account_number;
  }

  public static explicit operator JsonPayer(Payment payer) => new JsonPayer(payer.firstName, payer.payment, payer.date, payer.accountNumber);
}