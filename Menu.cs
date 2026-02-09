//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using System;

namespace PRGAssignment
{
    public class Menu
    {
        private string menuId;
        private string menuName;
        private List<FoodItem> foodItems;
       
        public Menu(string menuId, string menuName)
        {
            this.menuId = menuId;
            this.menuName = menuName;
            foodItems = new List<FoodItem>();
        }
       
        public string MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            foodItems.Add(foodItem); 
        }

        public bool RemoveFoodItem(FoodItem foodItem)
        {
            return foodItems.Remove(foodItem);
        }

        public void DisplayFoodItems()
        {
            Console.WriteLine($"== Menu: {menuName} ({menuId}) ==");
            if (foodItems.Count == 0)
            {
                Console.WriteLine("No food items in this menu.");
                return;
            }

            foreach (FoodItem fi in foodItems)
            {
                Console.WriteLine(fi);
            }
        }

        public override string ToString()
        {
            return $"Menu ID: {menuId}\nMenu Name: {menuName}\nFood Items: {foodItems.Count}";
        }

        public List<FoodItem> GetFoodItems()
        {
            return foodItems;
        }
    }
}
