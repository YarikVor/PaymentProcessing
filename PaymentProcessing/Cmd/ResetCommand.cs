namespace PaymentProcessing
{
  public class ResetCommand : CommandInfo
  {
    public ResetCommand() : base("reset", "reset work")
    {
    }

    public override void Execute() => Commands.Start();
  }
}