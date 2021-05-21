using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DietProject;
using LiteDB;

/*  The purpose of this program to to initialize the DB with some dummy data. This is to showcase and test the functions of the other parts of the DietProject program
 *  This console app creates meals of various calories and names for the past month. 
 *  THIS SHOULD ONLY BE RAN IF THERE IS NO DB EXISTING IN THE PATH GIVEN BELOW IN db_path
 */


namespace DBDataInit
{
    class Program
    {
        static void Main(string[] args)
        {

            const string db_path = @"C:\db\userData.db";
            // Construst DB if it doesn't exist.
            Console.WriteLine("Constructing DB");

            LiteDatabase db = new LiteDatabase(db_path);

            using (db)
            {
                ILiteCollection<Day> dbDays = db.GetCollection<Day>("days");

                Console.WriteLine("DB constructed");

                Console.WriteLine("Initializing data");

                DateTime currentDate = DateTime.Now.Date;
                List<Day> days = new List<Day>();

                for (DateTime dt = currentDate.AddDays(-8); dt < currentDate; dt = dt.AddDays(1))
                {
                    days.Add(new Day()
                    {
                        date = dt.ToString(),
                        Meals = new List<Meal>()
                    });
                }

                // Day 1
                AddMeal(days[0], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[0], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[0], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                // Day 2
                AddMeal(days[1], new Meal("Avocado toast", "0900", 650, 34, 33, 22));
                AddMeal(days[1], new Meal("Tuna Sandwhich", "1200", 800, 50, 30, 10));
                AddMeal(days[1], new Meal("Chicken Pasta", "1900", 800, 40, 60, 20));

                // Day 3
                AddMeal(days[2], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[2], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[2], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                // Day 4
                AddMeal(days[3], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[3], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[3], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                // Day 5
                AddMeal(days[4], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[4], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[4], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                // Day 6
                AddMeal(days[5], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[5], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[5], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));
                
                // Day 7
                AddMeal(days[6], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[6], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[6], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                // Day 8
                AddMeal(days[7], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[7], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[7], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                Console.WriteLine("Adding data to db");

                foreach(Day day in days)
                {
                    dbDays.Insert(day);
                }

                Console.WriteLine("Data has been added!\nPress any key to close...");

                Console.ReadLine();
            }
        }

        private static void AddMeal(Day day, Meal meal)
        {
            day.TotalCalories += meal.Calories;
            day.TotalCarbs += meal.Carbs;
            day.TotalProtein += meal.Protein;
            day.TotalFat += meal.Fat;
            day.Meals.Add(meal);
        }
    }
}
