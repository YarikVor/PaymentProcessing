using System.Timers;

namespace PaymentProcessing;

public static class PaymentEventsDay
{
  public static event Action NewDay;

  private static System.Timers.Timer timer = new System.Timers.Timer();

  static PaymentEventsDay()
  {
    timer.Elapsed += Timer_Elapsed;
    UpdateTimer();
  }

  private static void Timer_Elapsed(object? sender, ElapsedEventArgs e)
  {
    if (StaticData.IsWorking)
    {
      NewDay?.Invoke();
    }

    UpdateTimer();
  }

  private static void UpdateTimer()
  {
    timer.Interval = (DateTime.Today.AddDays(1) - DateTime.Now).TotalMilliseconds;
    timer.AutoReset = false;
    timer.Start();
  }
}