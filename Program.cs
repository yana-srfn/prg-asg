using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PRGAssignment
{
    class Program
    {
        //Classes

        public class Customer
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public List<Order> OrderList { get; set; } = new List<Order>();
        }

        public class FoodItem
        {
            public string RestaurantId { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }

        public class Restaurant
        {
            public string RestaurantId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public List<FoodItem> MenuItems { get; set; } = new List<FoodItem>();
            public Queue<Order> OrderQueue { get; set; } = new Queue<Order>();
        }

        public class Order
        {
            public string OrderId { get; set; }
            public Customer Customer { get; set; }
            public Restaurant Restaurant { get; set; }
            public DateTime DeliveryDateTime { get; set; }
            public string DeliveryAddress { get; set; }
            public List<(FoodItem Item, int Quantity)> Items { get; set; } = new List<(FoodItem, int)>();
            public decimal TotalAmount { get; set; }
            public string Status { get; set; }
        }

        public class SpecialOffer
        {
            public string RestaurantId { get; set; }
            public string OfferCode { get; set; }
            public string Description { get; set; }
            public decimal DiscountAmount { get; set; }
        }

        // ==================== Lists ====================
        static List<Customer> customers = new List<Customer>();
        static List<Restaurant> restaurants = new List<Restaurant>();
        static List<FoodItem> foodItems = new List<FoodItem>();
        static List<Order> orders = new List<Order>();
        static List<SpecialOffer> specialOffers = new List<SpecialOffer>();