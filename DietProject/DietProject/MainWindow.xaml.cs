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

namespace DietProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Meals = new List<Meal>();
            try
            {
                using (StreamReader sw = new StreamReader(@"C:\Users\vkqwn\Documents\college\y2\ood\project\DietApp\DietApp\data\Meals.json", Encoding.UTF8))
                {
                    string jsonData = sw.ReadToEnd().ToString();

                    MessageBox.Show(jsonData);
                    Days = JsonSerializer.Deserialize<DaysData>(jsonData);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

            MealsDate_tbk.Text = String.Format($"Today - {DateTime.Now.ToString("dd/MM/yyyy")}");

            foreach (Day day in Days.Days)
            {
                Days_cbx.Items.Add(day);
            }


        }

        private void AddMeal_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Days.Days[Days_cbx.SelectedIndex].Meals.Add(new Meal(MealName_tbx.Text,      // Name
                                   Int32.Parse(MealCalories_tbx.Text),                       // Calories
                                   Int32.Parse(MealCarb_tbx.Text),                           // Carbs
                                   Int32.Parse(MealProtein_tbx.Text),                        // Protein
                                   Int32.Parse(MealFat_tbx.Text)));                          // Fat

                Meals_lbx.ItemsSource = null;
                Meals_lbx.ItemsSource = Days.Days[Days_cbx.SelectedIndex].Meals;
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void Meals_lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Meal meal = Meals_lbx.SelectedItem as Meal;
            if (meal != null)
            {
                MealName_tbx.Text = meal.Name;
                MealCalories_tbx.Text = meal.Calories.ToString();
                MealCarb_tbx.Text = meal.Carbs.ToString();
                MealProtein_tbx.Text = meal.Protein.ToString();
                MealFat_tbx.Text = meal.Fat.ToString();
            }
        }

        private void Days_cbx_DropDownClosed(object sender, EventArgs e)
        {
            Meals_lbx.ItemsSource = Days.Days[Days_cbx.SelectedIndex].Meals;
        }
    }
}
