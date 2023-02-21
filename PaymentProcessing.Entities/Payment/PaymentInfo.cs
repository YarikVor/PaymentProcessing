namespace PaymentProcessing.Entities
{
  public sealed  class PaymentInfo
  {
    public readonly List<Payment> payments = new List<Payment>();

    public void Add(Payment payment) => payments.Add(payment);
  }
}