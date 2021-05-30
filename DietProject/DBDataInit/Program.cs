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
            // Path and db name. If changed, also change in DietProject
            const string directory = @"C:\DietProjectDB";
            const string db_path = @"C:\DietProjectDB\userData.db";

            Console.WriteLine($"Creating directory in location {directory}");

            // Construst DB if it doesn't exist.
            System.IO.Directory.CreateDirectory(directory);

            Console.WriteLine($"Constructing DB {db_path}");

            // Get the new db, create if it doesn't exist
            LiteDatabase db = new LiteDatabase(db_path);

            using (db)
            {
                // Get/create collection
                ILiteCollection<Day> dbDays = db.GetCollection<Day>("days");

                Console.WriteLine("DB constructed");

                Console.WriteLine("Initializing data");

                // Get the current date
                DateTime currentDate = DateTime.Now.Date;
                List<Day> days = new List<Day>();

                // Get the past 8 days until today (not inclusive)
                for (DateTime dt = currentDate.AddDays(-8); dt < currentDate; dt = dt.AddDays(1))
                {
                    days.Add(new Day()
                    {
                        date = dt,
                        Meals = new List<Meal>()
                    });
                }

                // Day 1
                AddMeal(days[0], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[0], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[0], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                // Day 2
                AddMeal(days[1], new Meal("Avocado toast", "0900", 650, 34, 33, 22));
                AddMeal(days[1], new Meal("Chicken Salad", "1200", 800, 50, 30, 10));
                AddMeal(days[1], new Meal("Chicken Pasta", "1900", 800, 40, 60, 20));

                // Day 3
                AddMeal(days[2], new Meal("Cereal", "1000", 400, 20, 30, 20));
                AddMeal(days[2], new Meal("Vegetable soup", "1400", 800, 50, 30, 10));
                AddMeal(days[2], new Meal("Spaghetti and meatballs", "1700", 800, 40, 60, 20));

                // Day 4
                AddMeal(days[3], new Meal("Eggs and toast", "0700", 600, 30, 30, 20));
                AddMeal(days[3], new Meal("Roast Dinner", "1800", 1200, 40, 60, 20));

                // Day 5
                AddMeal(days[4], new Meal("Avocado toast", "0900", 650, 34, 33, 22));
                AddMeal(days[4], new Meal("Chicken Salad", "1200", 800, 50, 30, 10));
                AddMeal(days[4], new Meal("Chicken Pasta", "1900", 800, 40, 60, 20));

                // Day 6
                AddMeal(days[5], new Meal("Tuna Sandwhich", "1300", 800, 50, 30, 10));
                AddMeal(days[5], new Meal("Roast Dinner", "1800", 800, 40, 60, 20));

                // Day 7
                AddMeal(days[6], new Meal("Cereal", "1000", 400, 20, 30, 20));
                AddMeal(days[6], new Meal("Vegetable soup", "1400", 800, 50, 30, 10));
                AddMeal(days[6], new Meal("Spaghetti and meatballs", "1700", 800, 40, 60, 20));


                // Day 8
                AddMeal(days[7], new Meal("Avocado toast", "0900", 650, 34, 33, 22));
                AddMeal(days[7], new Meal("Chicken Salad", "1200", 800, 50, 30, 10));
                AddMeal(days[7], new Meal("Chicken Pasta", "1900", 800, 40, 60, 20));

                Console.WriteLine("Adding data to db");

                // Insert each day into the db
                foreach(Day day in days)
                {
                    dbDays.Insert(day);
                }

                Console.WriteLine("Data has been added!");

                Console.WriteLine("Creating index on dates");

                // Create an index on the date property if one doesn't already exist
                dbDays.EnsureIndex(x => x.date);

                Console.WriteLine("Index created\nData initialization complete! Enter any key to close...");


                Console.ReadLine();
            }
        }

        private static void AddMeal(Day day, Meal meal)
        {
            // Simple method to add meals quickly while incrementing the daily total
            day.TotalCalories += meal.Calories;
            day.TotalCarbs += meal.Carbs;
            day.TotalProtein += meal.Protein;
            day.TotalFat += meal.Fat;
            day.Meals.Add(meal);
        }
    }
}
