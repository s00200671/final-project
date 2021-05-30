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
            // Adds a meal to a collection given in params
            // Adds meal nutrition to day total
            // Adds meal to list of meals in day
            // Throws error if any of the nutrient values are below 0
            if (meal.Calories >= 0 && meal.Carbs >= 0 && meal.Protein >= 0 && meal.Fat >= 0)
            {
                day.TotalCalories += meal.Calories;
                day.TotalCarbs += meal.Carbs;
                day.TotalProtein += meal.Protein;
                day.TotalFat += meal.Fat;
                day.Meals.Add(meal);
                days.Update(day);
            }
            else
            {
                throw new Exception("Nutrients in meal must be above or equal to 0");
            }
        }

        public static void RemoveMealDB(ILiteCollection<Day> days, Day day, Meal meal)
        {
            day.Meals.Remove(meal);
            day.TotalCalories -= meal.Calories;
            day.TotalCarbs -= meal.Carbs;
            day.TotalProtein -= meal.Protein;
            day.TotalFat -= meal.Fat;
            days.Update(day);
        }

        public static void AddDayDB(ILiteCollection<Day> days, DateTime iDate)
        {
            // Inserts a new day into the given collection
            days.Insert(new Day
            {
                date = iDate,
                Meals = new List<Meal>()
            });
        }

        public static string generateDBTime(string DayNight, int hour, int minute)
        {
            // Generates the time format for the DB. Time is stored in 24 hour string format
            if (hour > 12 || hour < 0 || minute > 59 || minute < 0)
            {
                throw new Exception("Hour must be between 1 and 12, minute must be between 0 and 60");
            }
            if (DayNight.ToLower() == "am")
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
            else if (DayNight.ToLower() == "pm")
            {
                if (hour < 12)
                {
                    return (hour + 12).ToString("00") + minute.ToString("00");
                }
                else
                {
                    return hour.ToString("00") + minute.ToString("00");
                }
            }
            else
            {
                throw new Exception("Error in choosing Day or night");
            }
        }
    }
}
