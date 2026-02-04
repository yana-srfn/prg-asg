using System;
using System.Collections.Generic;

namespace PRGAssignment
{
    class Program
    {
        // ==================== Shared Lists ====================
        public static List<Customer> customers = new List<Customer>();
        public static List<Restaurant> restaurants = new List<Restaurant>();
        public static List<FoodItem> foodItems = new List<FoodItem>();
        public static List<Order> orders = new List<Order>();
        public static List<SpecialOffer> specialOffers = new List<SpecialOffer>();

        static void Main(string[] args)
        {
            // You can optionally call features here for testing
            Console.WriteLine("Master branch loaded with shared lists.");
        }
    }
}
