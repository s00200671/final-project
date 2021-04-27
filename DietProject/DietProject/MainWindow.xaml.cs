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
            //if()
            //try
            //{
            //    dbDays.Insert[Days_cbx.SelectedIndex].Meals.Add(new Meal(
            //        { MealName_tbx.Text,      // Name
            //                       Int32.Parse(MealCalories_tbx.Text),                       // Calories
            //                       Int32.Parse(MealCarb_tbx.Text),                           // Carbs
            //                       Int32.Parse(MealProtein_tbx.Text),                        // Protein
            //                       Int32.Parse(MealFat_tbx.Text)));                          // Fat

            //    Meals_lbx.ItemsSource = null;
            //    Meals_lbx.ItemsSource = Days.Days[Days_cbx.SelectedIndex].Meals;
            //}
            //catch (Exception)
            //{
        //    messagebox.show("error");
        //}
    }

        private void Meals_lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Calender_lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Days_lbx.SelectedItem != null)
            {
                Day day = Days_lbx.SelectedItem as Day;
                Meals_lbx.ItemsSource = day.Meals; 
            }
        }

        private void RefreshDays()
        {
            var getDays = dbDays.Query()
                        .Select(x => x.date)
                        .ToList();

            Days_lbx.ItemsSource = getDays;
        }
    }
}
