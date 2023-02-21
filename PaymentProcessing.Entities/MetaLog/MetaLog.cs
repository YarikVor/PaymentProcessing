namespace PaymentProcessing.Entities;

public class MetaLog
{
  public int parsedFiles = 0;
  public int parsedLines = 0;
  public int foundErrors = 0;
  public List<string> invalidFiles = new List<string>();

  public void IncFiles() => parsedFiles++;

  public void IncLines() => parsedLines++;

  public void IncFoundError() => foundErrors++;

  public void AddInvalidFiles(string path) => invalidFiles.Add(path);

  public void Reset()
  {
    parsedFiles = 0;
    parsedLines = 0;
    foundErrors = 0;
    invalidFiles.Clear();
  }
}