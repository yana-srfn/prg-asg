using System;
using System.Collections.Generic;
using System.IO;

namespace PRGAssignment
{
    class Program
    {
        //Lists (from master)
        public static List<Customer> customers = new List<Customer>();
        public static List<Restaurant> restaurants = new List<Restaurant>();
        public static List<FoodItem> foodItems = new List<FoodItem>();
        public static List<Order> orders = new List<Order>();
        public static List<SpecialOffer> specialOffers = new List<SpecialOffer>();

        static void Main(string[] args)
        {
            LoadCustomers();
            LoadOrders();

            Console.WriteLine("Data loaded successfully.");
        }

        //Feature 2: Load CSVs
        static void LoadCustomers()
        {
            string[] lines = File.ReadAllLines("Data-Files/customers.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] cols = lines[i].Split(',');

                Customer c = new Customer
                {
                    Name = cols[1],
                    Email = cols[2]
                };

                customers.Add(c);
            }
        }

        static void LoadOrders()
        {
            string[] lines = File.ReadAllLines("Data/orders.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] cols = lines[i].Split(',');

                Order o = new Order
                {
                    OrderId = cols[0],
                    DeliveryAddress = cols[4],
                    TotalAmount = decimal.Parse(cols[6]),
                    Status = cols[7]
                };

                orders.Add(o);
            }
        }


        //Feature 3: List Restaurants & Menu
        static void ListRestaurants()
        {
            foreach (var r in restaurants)
            {
                Console.WriteLine($"Restaurant: {r.Name} ({r.RestaurantId})");

                foreach (var f in r.MenuItems)
                {
                    Console.WriteLine($"- {f.ItemName}: {f.Description} - ${f.Price:F2}");
                }

                Console.WriteLine();
            }
        }
    }
}
