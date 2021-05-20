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
    public partial class MainWindow : Window
    {

        LiteDatabase db = new LiteDatabase(@"C:\db\userData.db");
        ILiteCollection<Day> dbDays;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dbDays = db.GetCollection<Day>("days");

            RefreshDays();
        }

        private void AddMeal_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Day selectedDay = Days_lbx.SelectedItem as Day;

                if (selectedDay != null)
                {
                    string newTime = generateDBTime(time_cbx.SelectedItem.ToString(), hour_tbx.Text, minute_tbx.Text);

                    MessageBox.Show(newTime);

                    Meal newMeal = new Meal(
                                       MealName_tbx.Text,                            // Name
                                       newTime,                                      // time
                                       Int32.Parse(MealCalories_tbx.Text),           // Calories
                                       Int32.Parse(MealCarb_tbx.Text),               // Carbs
                                       Int32.Parse(MealProtein_tbx.Text),            // Protein
                                       Int32.Parse(MealFat_tbx.Text));               // Fat

                    AddMealDB(selectedDay, newMeal);

                    RefreshMeals();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("error");
            }
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
            var getDays = dbDays.Query()
                        .Select(x => x)
                        .ToList();

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

        public string generateDBTime(string DayNight, string hour, string minute)
        {
            return DayNight + hour + ":" + minute;
        }

        public void AddMealDB(Day day, Meal meal)
        {
            day.TotalCalories += meal.Calories;
            day.TotalCarbs += meal.Carbs;
            day.TotalProtein += meal.Protein;
            day.TotalFat += meal.Fat;
            day.Meals.Add(meal);
            dbDays.Update(day);
        }

        private void Day_btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedDate = day_dp.SelectedDate.ToString();
            MessageBox.Show(selectedDate);
            AddDayDB(selectedDate);
            RefreshDays();
        }

        public void AddDayDB(string iDate)
        {
            dbDays.Insert(new Day
            {
                date = iDate,
                Meals = new List<Meal>()
            });
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

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            StatGraph graph = new StatGraph();
            DataContext = graph;
        }
    }
}
