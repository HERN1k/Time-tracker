using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_tracker.Scripts;

namespace Time_tracker.Services
{
    class ViewMainTimer
    {
      public static string FormattedTime(int time)
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string formattedTime = "Error";
        if (time < 60)
          formattedTime = timeSpan.Seconds.ToString();
        else if (time < 3600)
          formattedTime = timeSpan.ToString(@"mm\:ss");
        else if (time < 86400)
          formattedTime = timeSpan.ToString(@"hh\:mm\:ss");
        else if (time >= 86400)
        {
          int module = time - (86400 * timeSpan.Days);
          if (module < 60)
            formattedTime = timeSpan.Seconds.ToString();
          else if (module < 3600)
            formattedTime = timeSpan.ToString(@"mm\:ss");
          else if (module < 86400)
            formattedTime = timeSpan.ToString(@"hh\:mm\:ss");
        }
        return formattedTime;
      }
      public static string FormattedDay(int time)
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string formattedDay = (time < 86400)
          ? ""
          : timeSpan.Days + " Day";
        return formattedDay;
      }
    public static string FormattedTrayTime(long mainTime)
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds((long)mainTime);
      string formattedDateTime;
      if (mainTime < 3600)
        formattedDateTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
      else if (mainTime < 86400)
        formattedDateTime = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
      else
        formattedDateTime = string.Format("{0}d {1:00}:{2:00}:{3:00}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

      return formattedDateTime;
    }
  }
}
