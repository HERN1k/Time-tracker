using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Time_tracker.Scripts;

namespace Time_tracker.Services
{
    class ViewPlayer
    {
      public static ImageSource ChangeIconPlayPouse(ImageSource source)
      {
        string uri = source.ToString().Split('/').Last();
        string newUri = (uri == "Play.png")
          ? "Pause.png"
          : "Play.png";
        BitmapImage bitmapImage = new BitmapImage(new Uri($"pack://application:,,,/Src/Icons/{newUri}"));
        return bitmapImage;
      }
      public static void StartStopTimer(MainWindow mainWindow, MainTimer timer)
      {
        if (!timer.IsRunning)
        {
          timer.StartTimer();
          mainWindow.TaskbarIconContextMenuItem_1.Header = "Pause";
        }
        else
        {
          timer.StopTimer();
          mainWindow.TaskbarIconContextMenuItem_1.Header = "Play";
        }
      }
  }
}
