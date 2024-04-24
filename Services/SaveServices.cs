using System.IO;

namespace Time_tracker.Services
{
  class SaveServices
  {
    private static string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
    private static string path { get; set; } = @$"{directoryPath}\Statistics\TimeStatistic.txt";

    public static bool SaveStatistic(int time, long? beginTime, long? endTime)
    {
      try
      {
        string statisticsPath = $"{directoryPath}\\Statistics";
        if (!Directory.Exists(statisticsPath))
          Directory.CreateDirectory(statisticsPath);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
        return false;
      }
      if (File.Exists(path))
      {
        try
        {
          string _id = "null";
          var lines = File.ReadLines(path).ToList();
          lines.RemoveAll(string.IsNullOrWhiteSpace);
          string secondLastLine = lines[lines.Count - 2];
          string lastIdStr = secondLastLine.Split('|')[1].Trim();
          int lastId;
          bool success = int.TryParse(lastIdStr, out lastId);
          if (success)
            _id = (lastId + 1).ToString();
          using (StreamWriter sw = new StreamWriter(path, true))
          {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            string _name = Environment.UserName;
            string _date = timeSpan.ToString();
            string _time = time.ToString();
            string? _beginTimeLong = (beginTime != null) ? beginTime.ToString() : "null";
            string? _endTimeLong = (endTime != null) ? endTime.ToString() : "null";
            string _beginTime;
            string _endTime;
            if (beginTime != null)
            {
              DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
              DateTime dateTimeBegin = epoch.AddSeconds((long)beginTime).ToLocalTime();
              string formattedDateTimeBegin = string.Format("{0:00}.{1:00}.{2:0000} {3:00}:{4:00}:{5:00}",
                dateTimeBegin.Day, dateTimeBegin.Month, dateTimeBegin.Year, dateTimeBegin.Hour, dateTimeBegin.Minute, dateTimeBegin.Second);
              
              _beginTime = formattedDateTimeBegin; 
            }
            else
              _beginTime = "null";
            if (endTime != null)
            {
              DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
              DateTime dateTimeEnd = epoch.AddSeconds((long)endTime).ToLocalTime();
              string formattedDateTimeEnd = string.Format("{0:00}.{1:00}.{2:0000} {3:00}:{4:00}:{5:00}",
                dateTimeEnd.Day, dateTimeEnd.Month, dateTimeEnd.Year, dateTimeEnd.Hour, dateTimeEnd.Minute, dateTimeEnd.Second);

              _endTime = formattedDateTimeEnd;
            }
            else
              _endTime = "null";
            string format = "| {0,-5} | {1,-20} | {2,-13} | {3,-11} | {4,-19} | {5,-19} | {6,-11} | {7,-11} |";
            string[] newLines = new string[]
            {
            String.Format(format, _id, _name, _date, _time, _beginTime, _endTime, _beginTimeLong, _endTimeLong),
            "--------------------------------------------------------------------------------------------------------------------------------------",
            };
            foreach (string line in newLines)
            {
              sw.WriteLine(line);
            }
          }
          return true;
        }
        catch (Exception e)
        {
          Console.WriteLine(e.ToString());
          return false;
        }
      }
      else
      {
        try
        {
          using (StreamWriter sw = File.CreateText(path))
          {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            string _name = Environment.UserName;
            string _date = timeSpan.ToString();
            string _time = time.ToString();
            string? _beginTimeLong = (beginTime != null) ? beginTime.ToString() : "null";
            string? _endTimeLong = (endTime != null) ? endTime.ToString() : "null";
            string _beginTime;
            string _endTime;
            if (beginTime != null)
            {
              DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
              DateTime dateTimeBegin = epoch.AddSeconds((long)beginTime).ToLocalTime();
              string formattedDateTimeBegin = string.Format("{0:00}.{1:00}.{2:0000} {3:00}:{4:00}:{5:00}",
                dateTimeBegin.Day, dateTimeBegin.Month, dateTimeBegin.Year, dateTimeBegin.Hour, dateTimeBegin.Minute, dateTimeBegin.Second);

              _beginTime = formattedDateTimeBegin;
            }
            else
              _beginTime = "null";
            if (endTime != null)
            {
              DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
              DateTime dateTimeEnd = epoch.AddSeconds((long)endTime).ToLocalTime();
              string formattedDateTimeEnd = string.Format("{0:00}.{1:00}.{2:0000} {3:00}:{4:00}:{5:00}",
              dateTimeEnd.Day, dateTimeEnd.Month, dateTimeEnd.Year, dateTimeEnd.Hour, dateTimeEnd.Minute, dateTimeEnd.Second);

              _endTime = formattedDateTimeEnd;
            }
            else
              _endTime = "null";
            string format = "| {0,-5} | {1,-20} | {2,-13} | {3,-11} | {4,-19} | {5,-19} | {6,-11} | {7,-11} |";
            string[] lines = new string[]
            {
            "--------------------------------------------------------------------------------------------------------------------------------------",
            "|  Id   |        Name          |     Time      |  Time(int)  |        Begin        |         End         | Begin(long) |  End(long)  |",
            "--------------------------------------------------------------------------------------------------------------------------------------",
            String.Format(format, "1", _name, _date, _time, _beginTime, _endTime, _beginTimeLong, _endTimeLong),
            "--------------------------------------------------------------------------------------------------------------------------------------",
            };
            foreach (string line in lines)
            {
              sw.WriteLine(line);
            }
          }
          return true;
        }
        catch(Exception e)
        {
          Console.WriteLine(e.ToString()); 
          return false;  
        }
      }
    }

  }
}