using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace DietProject
{
    public static class DBDays
    {
        public static void AddMealDB(ILiteCollection<Day> days, Day day, Meal meal)
        {
            day.TotalCalories += meal.Calories;
            day.TotalCarbs += meal.Carbs;
            day.TotalProtein += meal.Protein;
            day.TotalFat += meal.Fat;
            day.Meals.Add(meal);
            days.Update(day);
        }

        public static void AddDayDB(ILiteCollection<Day> days, string iDate)
        {
            days.Insert(new Day
            {
                date = iDate,
                Meals = new List<Meal>()
            });
        }
    }
}
