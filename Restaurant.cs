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
        public List<Menu> GetMenus() { return menus; }

        // +DisplayMenu() : void
        public void DisplayMenu()
        {
            // Show each menu and its food items
            foreach (Menu m in menus)
            {
                Console.WriteLine($"Menu: {m.ToString()}");
                m.DisplayFoodItems();
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
            Console.WriteLine("DisplayOrders() not implemented yet.");
        }
        public void DisplaySpecialOffers()
        {
            Console.WriteLine("DisplaySpecialOffers() not implemented yet.");
        }

        public override string ToString()
        {
            return { restaurantId }
            ;
        }
    }

}
}
