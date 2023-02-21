namespace PaymentProcessing
{
  public abstract class CommandInfo
  {
    public readonly string name;

    public readonly string description;

    protected CommandInfo(string name, string description)
    {
      this.name = name;
      this.description = description;
    }

    public abstract void Execute();
  }
}