using System.Globalization;
using System.Text.RegularExpressions;

namespace PaymentProcessing.Entities
{
  public static class PaymentManager
  {
    public const string PATTERN = @"(?<=^|,)\s*((?:"".*?"")|(?:[\s\w\-\.]+)|(?:))";

    private static readonly Action<Payment, string>[] actionForConvert = new Action<Payment, string>[] {
            (p, s) => p.firstName = s,
            (p, s) => p.lastName = s,
            (p, s) => p.address = AddressManager.ConvertString(s),
            (p, s) => p.payment = Convert.ToDecimal(s, CultureInfo.InvariantCulture),
            (p, s) => p.date = DateOnly.ParseExact(s, "yyyy-dd-mm"),
            (p, s) => p.accountNumber = long.Parse(s),
            (p, s) => p.service = s
        };

    public static Payment ConvertRowCsv(string csv)
    {
      var match = Regex.Match(csv, PATTERN);
      if (!match.Success) throw new FormatException();

      var res = new Payment();
      foreach (var act in actionForConvert)
      {
        string value = match.Groups[1].Value.Trim('"');
        act(res, value);

        match = match.NextMatch();
      }

      return res;
    }

    /*    public static PaymentInfo ConvertText(string csv, MetaLog log)
        {
          PaymentInfo info = new();
          int beforeCountLineError = log.foundErrors;
          log.IncFiles();

          foreach (var str in csv.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
          {
            log.IncLines();

            try
            {
              Payment payment = ConvertRowCsv(str);
              info.Add(payment);
            }
            catch
            {
              log.IncFoundError();
            }
          }

          if (beforeCountLineError != log.foundErrors)
          {
            log.AddInvalidFiles("text");
          }

          return info;
        }*/

    public static PaymentInfo ConvertFromStreamReader(StreamReader reader, MetaLog log, PaymentInfo info)
    {
      log.IncFiles();

      while (reader.Peek() > 0)
      {
        string line = reader.ReadLine();
        log.IncLines();

        if (string.IsNullOrEmpty(line)) continue;

        try
        {
          Payment payment = ConvertRowCsv(line);
          info.Add(payment);
        }
        catch
        {
          log.IncFoundError();
        }
      }

      return info;
    }
  }
}