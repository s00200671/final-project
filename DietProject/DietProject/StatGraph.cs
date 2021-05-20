using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace DietProject
{
    class StatGraph : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public StatGraph()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int> { 1000, 2500, 3000, 1500, 1200, 1700, 2000 }
                }
            };

            Labels = GenerateLabels();
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public string[] GenerateLabels()
        {
            var dates = new List<string>();
            DateTime currentDay = DateTime.Now;

            Console.WriteLine(currentDay.ToString());

            for (DateTime dt = currentDay.AddDays(-7); dt <= currentDay; dt = dt.AddDays(1))
            {
                Console.WriteLine(dt.ToString());
                dates.Add(dt.Date.ToString("dd-MM-yy"));
            }

            return dates.ToArray();
        }
    }
}
