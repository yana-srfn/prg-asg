//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using System;

namespace PRGAssignment
{
    public class OrderedFoodItem
    {
        // ===== attributes =====
        private FoodItem foodItem;
        private int qtyOrdered;
        private double subTotal;

        // ===== constructor =====
        public OrderedFoodItem(FoodItem foodItem, int qtyOrdered)
        {
            this.foodItem = foodItem;
            this.qtyOrdered = qtyOrdered;
        }

        // ===== methods =====
        public double CalculateSubtotal()
        {
            subTotal = foodItem.GetItemPrice() * qtyOrdered;
            return subTotal;
        }

        public override string ToString()
        {
            return $"{foodItem.GetItemName()} - {qtyOrdered}";
        }
    }
}