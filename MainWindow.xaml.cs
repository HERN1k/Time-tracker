using System.Windows;
using System.Windows.Input;
using Time_tracker.Services;
using Time_tracker.Scripts;

namespace Time_tracker
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MainTimer mainTimer;
    private Tray trayWindow;
    private ButtonsServices buttonsServices;
    private StatisticsWindow? statisticsWindow;
    public MainWindow()
    {
      InitializeComponent();
      this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      trayWindow = new Tray(this, taskbarIcon);
      mainTimer = new MainTimer(ClockText, ClockTextDay, TrayToolTipTextBlock);
      buttonsServices = new ButtonsServices(this, mainTimer);
      ClockTextDay.Text = ViewMainTimer.FormattedDay(mainTimer.MainTime);
      ClockText.Text = ViewMainTimer.FormattedTime(mainTimer.MainTime);
      TrayToolTipTextBlock.Text = ViewMainTimer.FormattedTrayTime(mainTimer.MainTime);
      buttonsServices.Hide();
    }
    // Tray buttons
    private void TrayLeftMouseDown_Click(object sender, RoutedEventArgs e)
    {
      trayWindow.TrayLeftMouseDownClick(sender, e, taskbarIcon);
    }
    private void PlayPouseTray_Click(object sender, RoutedEventArgs e)
    {
      buttonsServices.PlayPouse_MainTimer();
    }
    private void RefreshTray_Click(object sender, RoutedEventArgs e)
    {
      buttonsServices.Refresh_MainTimer();
    }
    private void SaveTray_Click(object sender, RoutedEventArgs e)
    {
      buttonsServices.Save_MainTimer();
    }
    private void ExitTray_Click(object sender, RoutedEventArgs e)
    {
      ButtonsServices.Exit();
    }
    // MainWindow buttons
    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
      ButtonsServices.Exit();
    }
    private void HideButton_Click(object sender, RoutedEventArgs e)
    {
      buttonsServices.Hide();
    }
    private void DragBlock(object sender, MouseButtonEventArgs e)
    {
      buttonsServices.DragMove();
    }
    private void HandlePlayPouse(object sender, MouseButtonEventArgs e)
    {
      buttonsServices.PlayPouse_MainTimer();
    }
    private void HandleRefresh(object sender, RoutedEventArgs e)
    {
      buttonsServices.Refresh_MainTimer();
    }
    private void HandleSave(object sender, RoutedEventArgs e)
    {
      buttonsServices.Save_MainTimer();
    }
    // Close popup
    private void ClosePopup(object sender, RoutedEventArgs e)
    {
      buttonsServices.ClosePopup_MainWindow(sender); 
    }
    // Statistics button
    private void StatisticsWindow(object sender, MouseButtonEventArgs e)
    {
      HamburgerPopup.IsOpen = false;
      if (statisticsWindow != null)
        if (statisticsWindow.IsLoaded) return;
      statisticsWindow = new StatisticsWindow(this);
      statisticsWindow.Show();
    }
    private void StatisticsWindow_Click(object sender, RoutedEventArgs e)
    {
      if (statisticsWindow != null)
        if (statisticsWindow.IsLoaded) return;
      statisticsWindow = new StatisticsWindow(this);
      statisticsWindow.Show();
    }
    public void StatisticsWindow_Exit()
    {
      if (statisticsWindow == null) return;
      statisticsWindow.Close();
    }
    // GitHub
    private void GitHub(object sender, MouseButtonEventArgs e)
    {
      HamburgerPopup.IsOpen = false;
      var process = new System.Diagnostics.ProcessStartInfo
      {
        FileName = "https://github.com/HERN1k",
        UseShellExecute = true
      };
      System.Diagnostics.Process.Start(process);
    }
  }
}