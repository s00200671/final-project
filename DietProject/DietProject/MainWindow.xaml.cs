using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiteDB;
using LiveCharts;
using LiveCharts.Wpf;

namespace DietProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public static class DB
    {
        public static LiteDatabase data = new LiteDatabase(@"C:\db\userData.db");
    }
    public partial class MainWindow : Window
    {
        ILiteCollection<Day> dbDays;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dbDays = DB.data.GetCollection<Day>("days");
            dbDays.EnsureIndex("date");

            RefreshDays();
        }

        // ADDING MEAL SECTION
        private void AddMeal_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Day selectedDay = Days_lbx.SelectedItem as Day;

                if (selectedDay != null)
                {
                    string newTime = generateDBTime(time_cbx.Text, Int16.Parse(hour_tbx.Text), Int16.Parse(minute_tbx.Text));

                    MessageBox.Show(newTime);

                    Meal newMeal = new Meal(
                                       MealName_tbx.Text,                            // Name
                                       newTime,                                      // time
                                       Int32.Parse(MealCalories_tbx.Text),           // Calories
                                       Int32.Parse(MealCarb_tbx.Text),               // Carbs
                                       Int32.Parse(MealProtein_tbx.Text),            // Protein
                                       Int32.Parse(MealFat_tbx.Text));               // Fat

                    DBDays.AddMealDB(dbDays, selectedDay, newMeal);

                    RefreshMeals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Day_btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedDate = day_dp.SelectedDate.ToString();
            DBDays.AddDayDB(dbDays, selectedDate);
            RefreshDays();
        }

        private void Meals_lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Meal selectedMeal = Meals_lbx.SelectedItem as Meal;

            if (selectedMeal != null)
            {
                Cal_tblk.Text = selectedMeal.Calories.ToString();
                Carbs_tblk.Text = selectedMeal.Carbs.ToString();
                Protein_tblk.Text = selectedMeal.Protein.ToString();
                Fat_tblk.Text = selectedMeal.Fat.ToString();
            }
        }

        private void Calender_lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Day selectedDay = Days_lbx.SelectedItem as Day;
         
            if(selectedDay != null)
            {
                ClearTblk();

                totalCal_tblk.Text = selectedDay.TotalCalories.ToString();
                totalCarbs_tblk.Text = selectedDay.TotalCarbs.ToString();
                totalProtein_tblk.Text = selectedDay.TotalProtein.ToString();
                totalFat_tblk.Text = selectedDay.TotalFat.ToString();
            }
            RefreshMeals();            
        }

        private void RefreshDays()
        {
            // Get the days of data, and order it by descending (most recent) by using the date
            var getDays = dbDays.Query()
                        .Select(x => x)
                        .ToList()
                        .OrderByDescending(x => Convert.ToDateTime(x.date));

            Days_lbx.ItemsSource = getDays;
        }

        private void RefreshMeals()
        {
            if (Days_lbx.SelectedItem != null)
            {
                Day day = Days_lbx.SelectedItem as Day;
                if (day.Meals == null)
                {
                    day.Meals = new List<Meal>();
                }
                Meals_lbx.ItemsSource = day.Meals;
            }
        }

        public string generateDBTime(string DayNight, int hour, int minute)
        {
            if (hour > 12 || hour < 0 || minute > 59 || minute < 0)
            {
                throw new Exception("Hour must be between 1 and 12, minute must be between 0 and 60");
            }
            if (DayNight.ToLower() == "am")
            {
                return hour.ToString("00") + minute.ToString("00");
            }
            else if (DayNight.ToLower() == "pm")
            {
                if (hour < 12)
                {
                    return (hour + 12).ToString("00") + minute.ToString("00");
                }
                else
                {
                    return "00" + minute.ToString("00");
                }
            }
            else
            {
                throw new Exception("Error in choosing Day or night");
            }
        }

        public void ClearTblk()
        {
            totalCal_tblk.Text = "";
            totalCarbs_tblk.Text = "";
            totalProtein_tblk.Text = "";
            totalFat_tblk.Text = "";

            Cal_tblk.Text = "";
            Carbs_tblk.Text = "";
            Protein_tblk.Text = "";
            Fat_tblk.Text = "";
        }

        // GRAPH SECTION
        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            StatGraph graph = new StatGraph(7);
            DataContext = graph;
        }

        private void PastDays_cbx_DropDownClosed(object sender, EventArgs e)
        {
            string selectedOption = PastDays_cbx.Text.ToLower();

            if (selectedOption != null)
            {
                if (selectedOption == "week")
                {
                    StatGraph graph = new StatGraph(7);
                    DataContext = graph;
                }
                else if (selectedOption == "month")
                {
                    StatGraph graph = new StatGraph(30);
                    DataContext = graph;
                }
                else
                {
                    throw new Exception("Not a valid drop down selection");
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.data.Dispose();
        }
    }
}
