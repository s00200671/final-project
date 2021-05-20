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

        public StatGraph()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>()
                }
            };

            Labels = GenerateLabels();
            SeriesCollection[0].Values = GenerateValues();

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
        
        public ChartValues<int> GenerateValues()
        {
            LiteDatabase db = new LiteDatabase(@"C:\db\userData.db");

            using (db)
            {
                ILiteCollection<Day> dbDays = db.GetCollection<Day>("days");
                var query = dbDays.Query().
                            Where(day => Convert.ToDateTime(day.date) >= DateTime.Now.Date.AddDays(-7) && Convert.ToDateTime(day.date) <= DateTime.Now.Date)
                            .Select(x => x)
                            .ToList();

                ChartValues<int> Values = new ChartValues<int>();

                foreach(Day day in query)
                {
                    Values.Add(day.TotalCalories);
                }

                return Values;
            }
        }
    }
}
