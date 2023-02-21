namespace PaymentProcessing
{
  public class StartCommand : CommandInfo
  {
    public StartCommand() : base("start", "starting")
    {
    }

    public override void Execute() => Commands.Start();
  }
}