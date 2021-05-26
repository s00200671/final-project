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
        // Class for the user calorie graph
        public SeriesCollection SeriesCollection { get; set; }
        // Labels of the dates
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        // Constructor function for initalizing the class. Binds with datacontext
        // Takes 1 value, the range. This is the number of past days you want to render in the graph. e.g. 7 days for a week, 30 for a month
        public StatGraph(int range)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int>()
                }
            };

            // Generate the labels and add them to the graph
            Labels = GenerateLabels(range);
            // Generate the values and add them to the graph
            SeriesCollection[0].Values = GenerateValues(range);

            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public string[] GenerateLabels(int range)
        {
            // Create a list of dates. Add dates from the start day to the current day into the list. Return as array
            var dates = new List<string>();
            DateTime currentDay = DateTime.Now;

            // Works by adding a negative amount of days to the current date. This will give the date in range amount of days ago
            for (DateTime dt = currentDay.AddDays(-range); dt <= currentDay; dt = dt.AddDays(1))
            {
                // Format e.g. Mon, 11 Jun
                dates.Add(dt.Date.ToString("ddd, dd MMM"));
            }

            return dates.ToArray();
        }

        public ChartValues<int> GenerateValues(int range)
        {
            // Get the collection from the static class
            ILiteCollection<Day> dbDays = DB.data.GetCollection<Day>("days");
            // Init new chart values
            ChartValues<int> Values = new ChartValues<int>();

            // Get the past range of days
            var days = new List<string>();
            DateTime currentDay = DateTime.Now.Date;
            DateTime startDay = currentDay.AddDays(-range);

            for (DateTime dt = startDay; dt <= currentDay; dt = dt.AddDays(1))
            {
                days.Add(dt.Date.ToString());
            }

            // query LiteDB where the day is between the start and current day (inclusive)
            var query = dbDays.Query()
                        .Where(day => Convert.ToDateTime(day.date) >= startDay && Convert.ToDateTime(day.date) <= currentDay.Date)
                        .Select(x => x)
                        .ToList();


            // Check if the values returned from the query exist in the past range of days
            // If there was no value entered for that day, (and so it does not exist in the db) then add 0 to the values
            for (int i = 0; i < days.Count; i++)
            {
                var res = query.Find(day => day.date == days[i]);

                if (res != null)
                {
                    Values.Add(res.TotalCalories);
                    query.Remove(res);
                }
                else
                {
                    Values.Add(0);
                }
            }

            return Values;
        }
    }
}
