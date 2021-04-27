using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietProject
{
    public class Day
    {
        public int Id { get; set; }
        public string date { get; set; }
        public List<Meal> Meals { get; set; }
    }

    public class Meal
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }

        public Meal(string name, string time, int calories, int protein, int carbs, int fat)
        {
            Name = name;
            Time = time;
            Calories = calories;
            Protein = protein;
            Carbs = carbs;
            Fat = fat;
        }
    }
}
