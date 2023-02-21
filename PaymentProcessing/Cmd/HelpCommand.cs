namespace PaymentProcessing
{
  public class HelpCommand : CommandInfo
  {
    public HelpCommand() : base("help", "helped")
    {
    }

    public override void Execute() => Commands.Help();
  }
}