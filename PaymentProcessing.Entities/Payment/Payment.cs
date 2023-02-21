using System.Diagnostics.CodeAnalysis;

namespace PaymentProcessing.Entities;

public class Payment
{
  [AllowNull]
  public string firstName;

  [AllowNull]
  public string lastName;

  [AllowNull]
  public Address address;

  public decimal payment;
  public DateOnly date;
  public long accountNumber;

  [AllowNull]
  public string service;
}