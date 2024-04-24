using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Time_tracker.Services;

namespace Time_tracker
{
  /// <summary>
  /// Interaction logic for StatisticsWindow.xaml
  /// </summary>
  public partial class StatisticsWindow : Window
  {
    private MainWindow mainWindow;
    private string statisticsDirectoryPath { get; } = $"{AppDomain.CurrentDomain.BaseDirectory}\\Statistics";
    private string path { get; } = $"{AppDomain.CurrentDomain.BaseDirectory}\\Statistics\\TimeStatistic.txt";
    private List<string>? lines { get; set; }
    public StatisticsWindow(MainWindow _mainWindow)
    {
      InitializeComponent();
      this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      mainWindow = _mainWindow;
      InitializeData();
      this.Closed += IsClosed;
    }
    private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
      e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToCells);
    }
    private void InitializeData() 
    {
      if (Directory.Exists(statisticsDirectoryPath))
        if (File.Exists(path))
          GetData();
    }
    private void GetData()
    {
      try 
      {
        lines = File.ReadLines(path).ToList();
        lines.RemoveAll(string.IsNullOrWhiteSpace);
        lines.RemoveAll(line => line.StartsWith("-"));
        lines.RemoveAt(0);
        SetData();
      }
      catch(Exception e)
      {
        Console.WriteLine(e.ToString());
        return;
      }
    }
    private void SetData()
    {
      try
      {
        List<DataTime> dataTime = new List<DataTime> { };
        if (lines == null) return;
        foreach (string line in lines)
        {
          string[] stringItem = line.Split('|');
          string _id = stringItem[1].Trim();
          _ = int.TryParse(_id, out int id);
          string name = stringItem[2];
          string time = stringItem[3];
          string begin = stringItem[5];
          string end = stringItem[6];
          DataTime newData = new DataTime { Id = id, Name = name, Time = time, Begin = begin, End = end };
          dataTime.Add(newData);
        }
        dataGrid.ItemsSource = dataTime;
        dataGrid.ScrollIntoView(dataTime[dataTime.Count - 1]);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
        return;
      }
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
      mainWindow.StatisticsWindow_Exit();
    }
    private void DragBlock(object sender, MouseButtonEventArgs e)
    {
      ButtonsServices.DragMove(this);
    }
    private void IsClosed(object? sender, EventArgs e)
    {
      if (sender == null) return;
      DataContext = null;
    }
  }
}

public class DataTime
{
  public int? Id { get; set; }
  public string? Name { get; set; }
  public string? Time { get; set; }
  public string? Begin { get; set; }
  public string? End { get; set; }
}