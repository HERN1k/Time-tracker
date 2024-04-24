using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace Time_tracker.Scripts
{
  internal class Tray
  {
    public TaskbarIcon taskbarIcon;
    private MainWindow mainWindow;
    public Tray(MainWindow mainWindow, TaskbarIcon taskbarIcon)
    {
      this.taskbarIcon = taskbarIcon;
      this.mainWindow = mainWindow;
      taskbarIcon.ToolTipText = "00:00";
    }

    public void TrayLeftMouseDownClick(object sender, RoutedEventArgs e, TaskbarIcon tray)
    {
      mainWindow.Show();
      tray.Visibility = Visibility.Hidden;
    }
  }
}
