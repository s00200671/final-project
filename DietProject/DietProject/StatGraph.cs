using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using LiteDB;

namespace DietProject
{
    class StatGraph : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public StatGraph(int range)
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>()
                }
            };

            Labels = GenerateLabels(range);
            SeriesCollection[0].Values = GenerateValues(range);

            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public string[] GenerateLabels(int range)
        {
            var dates = new List<string>();
            DateTime currentDay = DateTime.Now;

            for (DateTime dt = currentDay.AddDays(-range); dt <= currentDay; dt = dt.AddDays(1))
            {
                dates.Add(dt.Date.ToString("ddd MMM"));
            }

            Console.WriteLine("hi");
            Console.WriteLine(range);
            foreach (string s in dates)
            {
                Console.WriteLine(s);
            }
            return dates.ToArray();
        }

        public ChartValues<int> GenerateValues(int range)
        {
            ILiteCollection<Day> dbDays = DB.data.GetCollection<Day>("days");

            int selectedRange = range;
            var query = dbDays.Query()
                        .Where(day => Convert.ToDateTime(day.date) >= DateTime.Now.Date.AddDays(-selectedRange) && Convert.ToDateTime(day.date) <= DateTime.Now.Date)
                        .Select(x => x)
                        .ToList();

            ChartValues<int> Values = new ChartValues<int>();

            foreach (Day day in query)
            {
                Values.Add(day.TotalCalories);
                Console.Write(day.TotalCalories + " , ");
            }

            return Values;
        }
    }
}
