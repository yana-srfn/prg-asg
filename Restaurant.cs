//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using System.Collections.Generic;

namespace PRGAssignment
{
    public class Restaurant
    {
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<FoodItem> MenuItems { get; set; } = new List<FoodItem>();
        public Queue<Order> OrderQueue { get; set; } = new Queue<Order>();
    }
}
