namespace PaymentProcessing
{
  public class StopCommand : CommandInfo
  {
    public StopCommand() : base("stop", "stoped")
    {
    }

    public override void Execute() => Commands.Stop();
  }
}