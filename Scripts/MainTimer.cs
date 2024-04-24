using System.Windows.Controls;
using System.Windows.Threading;
using Time_tracker.Services;

namespace Time_tracker.Scripts
{
  class MainTimer
  {
    private TextBlock ClockText;
    private TextBlock ClockTextDay;
    private DispatcherTimer timer;
    private TextBlock TrayToolTipTextBlock;
    private int _mainTime { get; set; } = 0;
    public int MainTime
    {
      get
      {
        return _mainTime;
      }
      set
      {
        _mainTime = value;
      }
    }
    private bool _isRunning { get; set; } = false;
    public bool IsRunning
    {
      get
      {
        return _isRunning;
      }
      set
      {
        _isRunning = value;
      }
    }
    private long? _beginTime { get; set; } = null;
    public long? BeginTime
    {
      get
      {
        return _beginTime;
      }
      set
      {
        _beginTime = value;
      }
    }
    private long? _endTime { get; set; } = null;
    public long? EndTime
    {
      get
      {
        return _endTime;
      }
      set
      {
        _endTime = value;
      }
    }
    public MainTimer(TextBlock textBlock, TextBlock clockTextDay, TextBlock trayToolTipTextBlock)
    {
      ClockText = textBlock;
      ClockTextDay = clockTextDay;
      TrayToolTipTextBlock = trayToolTipTextBlock;
      timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromSeconds(1);
      timer.Tick += Timer_Tick;
    }

    public void StartTimer()
    {
      IsRunning = true;
      timer.Start();
      if (MainTime == 0)
      {
        DateTime dateTime = DateTime.UtcNow;
        long unixTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        BeginTime = unixTime;
      }
    }
    public void StopTimer()
    {
      IsRunning = false;
      timer.Stop();
      DateTime dateTime = DateTime.UtcNow;
      long unixTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
      EndTime = unixTime;
    }
    private void Timer_Tick(object? sender, EventArgs e)
    {
      MainTime++; 
      TrayToolTipTextBlock.Text = ViewMainTimer.FormattedTrayTime(MainTime);
      ClockTextDay.Text = ViewMainTimer.FormattedDay(MainTime);
      ClockText.Text = ViewMainTimer.FormattedTime(MainTime);
    }
    public void RefreshTimer()
    {
      if (IsRunning)
      {
        IsRunning = false;
        timer.Stop();
      }
      MainTime = 0;
      BeginTime = null;
      EndTime = null;
      ClockTextDay.Text = ViewMainTimer.FormattedDay(MainTime);
      ClockText.Text = ViewMainTimer.FormattedTime(MainTime);
    }
  }
}
