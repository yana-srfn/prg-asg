using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_asg
{
    class Restaurant
    {
        public string Id { get; }
        public string Name { get; }
        public string Email { get; }

        public List<FoodItem> FoodItems { get; }
        public Queue<Order> OrderQueue { get; }

        public Restaurant(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            FoodItems = new List<FoodItem>();
            OrderQueue = new Queue<Order>();
        }
    }
}
