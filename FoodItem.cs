//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

namespace PRGAssignment
{
    public class FoodItem
    {
        private string itemName;
        private string itemDesc;
        private double itemPrice;
        private string customise;

        // Constructor
        public FoodItem(string name, string desc, double price, string custom)
        {
            itemName = name;
            itemDesc = desc;
            itemPrice = price;
            customise = custom;
        }

        // Get
        public string GetItemName()
        { 
            return itemName; 
        }
        public string GetItemDesc() 
        {
            return itemDesc;
        }
        public double GetItemPrice() 
        { 
            return itemPrice; 
        }
        public string GetCustomise() 
        { 
            return customise; 
        }
        public override string ToString()
        {
            // If customise is empty, don’t show it
            if (string.IsNullOrWhiteSpace(customise))
                return $"{itemName}: {itemDesc} (${itemPrice:0.00})";

            return $"{itemName}: {itemDesc} (${itemPrice:0.00}) | Customise: {customise}";
        }
    }
}
