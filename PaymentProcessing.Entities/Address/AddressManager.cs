using System.Text.RegularExpressions;

namespace PaymentProcessing.Entities;

public static class AddressManager
{
  public const string PATTERN = @"^(?: *([a-zA-Z ]+?)\s*(?=,)),(?:\s*([a-zA-Z ]+?)\s+?)(\d+),\s*(\d+)$";

  public static Address ConvertString(string address)
  {
    var match = Regex.Match(address, PATTERN);

    if (!match.Success)
    {
      throw new FormatException();
    }

    var res = new Address(match.Groups[1].Value, match.Groups[2].Value, int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

    return res;
  }
}