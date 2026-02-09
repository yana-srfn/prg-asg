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

        // Relationship
        private List<Menu> menu = new List<MenuCommand>();
        private Queue<Order> orderQueue;
        internal static readonly int Count;

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
        public List<Menu> Menus
        {
            get { return menus; }
            set { Menus = value; }
        }

        public Queue<Order> orderQueue
        {
            get { return orderQueue; }
            set { orderQueue = value; }
        }
        // default
        public Restaurant()
        {
            menus = new List<Menu>();
            orderQueue = new Queue<Order>();
        }
        //parameterized
        public Restaurant(string id, string name, string email)
        {
            restaurantId = id;
            restaurantName = name;
            restaurantEmail = email;
        }

        // +DisplayMenu() : void
        public void DisplayMenu()
        {
            // Show each menu and its food items
            foreach (Menu menu in menus)
            {
                Console.WriteLine(menu);
                menu.DisplayFoodItems();
            }
        }
        // +AddMenu(Menu) : void
        public void AddMenu(Menu menu)
        {
            menus.Add(menu);
        }

        // +RemoveMenu(Menu) : bool
        public bool RemoveMenu(Menu menu)
        {
            return menus.Remove(menu);
        }

        // (These are in diagram but you can implement later)
        public void DisplayOrders()
        {
            foreach (Order order in orderQueue)
            {
                Console.WriteLine(order);
            }
        }
        public void DisplaySpecialOffers()
        {
            foreach (Order order in orderQueue)
            {
                if (Order.SpecialOffer != null)
                {
                    Console.WriteLine(order.SpecialOffer);
                }
            }
        }

        public override string ToString()
        {
            return "RestaurantID: " + restaurantId + "\n"
                + "Restaurant Name: " + restaurantName + "\n"
                + "Restaurant Email" + restaurantEmail + "\n";
        }
    }

}
}
