//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using System;
using System.Collections.Generic;

namespace PRGAssignment
{
    class Program
    {
        //Shared Lists
        public static List<Customer> customers = new List<Customer>();
        public static List<Restaurant> restaurants = new List<Restaurant>();
        public static List<FoodItem> foodItems = new List<FoodItem>();
        public static List<Order> orders = new List<Order>();
        public static List<SpecialOffer> specialOffers = new List<SpecialOffer>();

        static void Main(string[] args)
        {
            // Load all CSVs into master lists
            LoadCustomers();
            LoadRestaurants();
            LoadFoodItems();
            LoadOrders();
            LoadSpecialOffers();

            // Interactive menu for features
            while (true)
            {
                Console.WriteLine("\nWelcome to the Gruberoo Food Delivery System");
                Console.WriteLine("  restaurants loaded!");
                Console.WriteLine("  food items loaded!");
                Console.WriteLine("  customers loaded!");
                Console.WriteLine("  orders loaded!");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();
            }
        }
    }
}

