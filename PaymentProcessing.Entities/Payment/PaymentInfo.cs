namespace PaymentProcessing.Entities
{
  public class PaymentInfo
  {
    public List<Payment> payments = new List<Payment>();

    public void Add(Payment payment) => payments.Add(payment);
  }
}