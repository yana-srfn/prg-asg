using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_asg
{
    class FoodItem
    {
        public string ItemName { get; }
        public string Description { get; }
        public double Price { get; }

        public FoodItem(string name, string desc, double price)
        {
            ItemName = name;
            Description = desc;
            Price = price;
        }
    }
}
