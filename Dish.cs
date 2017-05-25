using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympFoodClient
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Energy_value { get; set; }
        public int Price { get; set; }

        public override bool Equals(object obj)
        {
            Dish dish = obj as Dish;
            return this.Id == dish.Id;
        }
    }

    public class TextDish
    {
        public string Name { get; set; }
        public string Energy_value { get; set; }
        public string Price { get; set; }
    }
}
