namespace PaymentProcessing
{
  public class ClearCommand : CommandInfo
  {
    public ClearCommand() : base("clr", "clear log")
    {
    }

    public override void Execute() => Commands.Clear();
  }
}