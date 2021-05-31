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
    /// 
    
    // uses LiveCharts and LiteDB to create an app which allows users to add or remove days and meals to a database 
    

    public static class DB
    {
        public static LiteDatabase data;

        static DB()
        {
            // Strings for db location
            const string directory = @"C:\DietProjectDB";
            const string db_path = @"C:\DietProjectDB\userData.db";

            // If the folder does not exist, create it
            System.IO.Directory.CreateDirectory(directory);

            // Use the path for the new db in the newly created folder
            data = new LiteDatabase(db_path);
        }
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
            // Get the collection of days, and then ensure that an index exists on the date property
            // If the index already exists, then it won't recreate it
            dbDays = DB.data.GetCollection<Day>("days");
            dbDays.EnsureIndex("date");

            // Initalize the graph class and set the data context of the stat graph element to it
            StatGraph graph = new StatGraph(7);
            Stat_Graph.DataContext = graph;

            // Refresh the days listbox
            RefreshDays();
        }

        // ADDING MEAL SECTION
        private void AddMeal_btn_Click(object sender, RoutedEventArgs e)
        {
            // Attempt to add the day, and throw an error otherwise
            try
            {
                Day selectedDay = Days_lbx.SelectedItem as Day;

                if (selectedDay != null)
                {
                    // Use the DBDays static class method to generate a time for the db when we add it 
                    string newTime = DBDays.generateDBTime(time_cbx.Text, Int16.Parse(hour_tbx.Text), Int16.Parse(minute_tbx.Text));

                    Meal newMeal = new Meal(
                                       MealName_tbx.Text,                            // Name
                                       newTime,                                      // time
                                       Int32.Parse(MealCalories_tbx.Text),           // Calories
                                       Int32.Parse(MealCarb_tbx.Text),               // Carbs
                                       Int32.Parse(MealProtein_tbx.Text),            // Protein
                                       Int32.Parse(MealFat_tbx.Text));               // Fat

                    // Add the newly created meal obj to the db
                    DBDays.AddMealDB(dbDays, selectedDay, newMeal);

                    // Refresh the list of meals
                    RefreshMeals();

                    // Clear the textboxes
                    ClearTblk();

                    // Refresh the stat graph
                    StatGraph graph = new StatGraph(7);
                    Stat_Graph.DataContext = graph;

                    // Refresh the day info, since a new object is added, the calories will increase
                    totalCal_tblk.Text = selectedDay.TotalCalories.ToString();
                    totalCarbs_tblk.Text = selectedDay.TotalCarbs.ToString();
                    totalProtein_tblk.Text = selectedDay.TotalProtein.ToString();
                    totalFat_tblk.Text = selectedDay.TotalFat.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteMeal_btn_Click(object sender, RoutedEventArgs e)
        {
            // Remove selected meal from the selected days

            // Get the day and meal info 
            Day selectedDay = Days_lbx.SelectedItem as Day;
            Meal selectedMeal = Meals_lbx.SelectedItem as Meal;

            if (selectedDay != null && selectedMeal != null)
            {
                DBDays.RemoveMealDB(dbDays, selectedDay, selectedMeal);

                // Refresh the list of meals
                RefreshMeals();

                // Clear the textboxes
                ClearTblk();

                // Refresh the stat graph
                StatGraph graph = new StatGraph(7);
                Stat_Graph.DataContext = graph;

                // Refresh the day info, since a new object is added, the calories will increase
                totalCal_tblk.Text = selectedDay.TotalCalories.ToString();
                totalCarbs_tblk.Text = selectedDay.TotalCarbs.ToString();
                totalProtein_tblk.Text = selectedDay.TotalProtein.ToString();
                totalFat_tblk.Text = selectedDay.TotalFat.ToString();
            }
        }

        private void Day_btn_Click(object sender, RoutedEventArgs e)
        {
            // Add a day to the db
            DateTime selectedDate = (DateTime)day_dp.SelectedDate;
            if (selectedDate != null)
            {
                DBDays.AddDayDB(dbDays, selectedDate);
                RefreshDays();
            }
        }

        private void Meals_lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the meal info 
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
            // Fill the day info in, and get the list of meals for this day
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
                        .OrderByDescending(x => x.date);

            Days_lbx.ItemsSource = getDays;
        }

        private void RefreshMeals()
        {
            // Refresh the meals list box
            if (Days_lbx.SelectedItem != null)
            {
                Day day = Days_lbx.SelectedItem as Day;
                if (day.Meals == null)
                {
                    day.Meals = new List<Meal>();
                }
                Meals_lbx.ItemsSource = null;
                Meals_lbx.ItemsSource = day.Meals;
            }
        }

        public void ClearTblk()
        {
            // Clear all info text boxes
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
        private void PastDays_cbx_DropDownClosed(object sender, EventArgs e)
        {
            string selectedOption = PastDays_cbx.Text.ToLower();

            // Change the graph to the past week or month, depending on the combobox selection
            if (selectedOption != null)
            {
                if (selectedOption == "week")
                {
                    StatGraph graph = new StatGraph(7);
                    Stat_Graph.DataContext = graph;
                }
                else if (selectedOption == "month")
                {
                    StatGraph graph = new StatGraph(30);
                    Stat_Graph.DataContext = graph;
                }
                else
                {
                    return;
                }
            }
        }

        // ON EXIT
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.data.Dispose();
        }
    }
}
