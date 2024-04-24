using System.Windows.Controls;
using System.Windows;
using Time_tracker.Scripts;

namespace Time_tracker.Services
{
  internal class ButtonsServices
  {
    private MainWindow mainWindow;
    private MainTimer mainTimer;
    public ButtonsServices(MainWindow _mainWindow, MainTimer _mainTimer)
    {
      mainWindow = _mainWindow;
      mainTimer = _mainTimer;
    }
    public static void Exit()
    {
      Application.Current.Shutdown();
    }
    public void Hide()
    {
      mainWindow.Hide();
      mainWindow.taskbarIcon.Visibility = Visibility.Visible;
    }
    public void DragMove()
    {
      mainWindow.DragMove();
    }
    public static void DragMove(StatisticsWindow statisticsWindow)
    {
      statisticsWindow.DragMove();
    }
    public void ClosePopup_MainWindow(object sender)
    {
      Button? button = sender as Button;
      if (button == null) return;
      switch (button.Name)
      {
        case "RefreshClosePopup":
          mainWindow.refreshPopup.IsOpen = false;
          break;
        case "SaveClosePopup":
          mainWindow.savePopup.IsOpen = false;
          break;
        default:
          mainWindow.refreshPopup.IsOpen = false;
          mainWindow.savePopup.IsOpen = false;
          break;
      }
    }
    public void PlayPouse_MainTimer()
    {
      mainWindow.PlayPouseButton.Source = ViewPlayer.ChangeIconPlayPouse(mainWindow.PlayPouseButton.Source);
      ViewPlayer.StartStopTimer(mainWindow, mainTimer);
    }
    public void Refresh_MainTimer()
    {
      mainWindow.refreshPopup.IsOpen = false;
      if (mainTimer.IsRunning)
        mainWindow.PlayPouseButton.Source = ViewPlayer.ChangeIconPlayPouse(mainWindow.PlayPouseButton.Source);
      mainTimer.RefreshTimer();
      mainWindow.TaskbarIconContextMenuItem_1.Header = "Play";
      mainWindow.TrayToolTipTextBlock.Text = "00:00";
    }
    public void Save_MainTimer()
    {
      mainWindow.savePopup.IsOpen = false;
      //if (mainTimer.BeginTime == null)
      //  return;
      long? _endTime = mainTimer.EndTime;
      if (mainTimer.EndTime == null)
      {
        DateTime dateTime = DateTime.UtcNow;
        _endTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
      }
      if (mainTimer.IsRunning)
      {
        DateTime dateTime = DateTime.UtcNow;
        _endTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
      }
      bool success = SaveServices.SaveStatistic(mainTimer.MainTime, mainTimer.BeginTime, _endTime);
      if (success)
        if (mainTimer.IsRunning)
          mainWindow.PlayPouseButton.Source = ViewPlayer.ChangeIconPlayPouse(mainWindow.PlayPouseButton.Source);
      mainTimer.RefreshTimer();
      mainWindow.TaskbarIconContextMenuItem_1.Header = "Play";
      mainWindow.TrayToolTipTextBlock.Text = "00:00";
    }

  }
}