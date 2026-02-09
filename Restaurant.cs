//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace PRGAssignment
{
    public class Restaurant
    {
        private string restaurantId;
        private string restaurantName;
        private string restaurantEmail;

        private List<Menu> menus;
        private List<SpecialOffer> specialOffers;

        // Restaurant receives 0..* Orders
        private Queue<Order> orderQueue;

        public Restaurant(string restaurantId, string restaurantName, string restaurantEmail)
        {
            this.restaurantId = restaurantId;
            this.restaurantName = restaurantName;
            this.restaurantEmail = restaurantEmail;

            menus = new List<Menu>();
            specialOffers = new List<SpecialOffer>();
            orderQueue = new Queue<Order>();
        }

        public string RestaurantId
        {
            get { return restaurantId; }
            set { restaurantId = value; }
        }

        public string RestaurantName
        {
            get { return restaurantName; }
            set { restaurantName = value; }
        }

        public string RestaurantEmail
        {
            get { return restaurantEmail; }
            set { restaurantEmail = value; }
        }

        public void AddMenu(Menu menu)
        {
            menus.Add(menu);
        }

        public bool RemoveMenu(Menu menu)
        {
            return menus.Remove(menu);
        }

        public void DisplayMenu()
        {
            Console.WriteLine($"== {restaurantName} Menus ==");

            if (menus.Count == 0)
            {
                Console.WriteLine("No menus available.");
                return;
            }

            foreach (Menu m in menus)
            {
                Console.WriteLine(m);
                Console.WriteLine("-------------------------");
            }
        }

        public void DisplaySpecialOffers()
        {
            Console.WriteLine($"== {restaurantName} Special Offers ==");

            if (specialOffers.Count == 0)
            {
                Console.WriteLine("No special offers available.");
                return;
            }

            foreach (SpecialOffer so in specialOffers)
            {
                Console.WriteLine(so);
            }
        }

        public void DisplayOrders()
        {
            Console.WriteLine($"== {restaurantName} Orders ==");

            if (orderQueue.Count == 0)
            {
                Console.WriteLine("No orders in queue.");
                return;
            }

            foreach (Order o in orderQueue)
            {
                Console.WriteLine(o);
                Console.WriteLine("-------------------------");
            }
        }

        // Extra helpers to match UML relationship
        public void AddSpecialOffer(SpecialOffer offer)
        {
            specialOffers.Add(offer);
        }

        public bool RemoveSpecialOffer(SpecialOffer offer)
        {
            return specialOffers.Remove(offer);
        }

        public void ReceiveOrder(Order order)
        {
            orderQueue.Enqueue(order);
        }

        public override string ToString()
        {
            return $"Restaurant ID: {restaurantId}\n" +
                   $"Restaurant Name: {restaurantName}\n" +
                   $"Restaurant Email: {restaurantEmail}\n" +
                   $"Menus: {menus.Count}\n" +
                   $"Special Offers: {specialOffers.Count}\n" +
                   $"Orders in Queue: {orderQueue.Count}";
        }

        // Optional accessors
        public List<Menu> GetMenus() => menus;
        public List<SpecialOffer> GetSpecialOffers() => specialOffers;
        public Queue<Order> GetOrderQueue() => orderQueue;
    }
}
