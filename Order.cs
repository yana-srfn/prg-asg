using System;
using System.Collections.Generic;

namespace PRGAssignment
{
    public class Order
    {
        private int orderId;
        private DateTime orderDateTime;
        private double orderTotal;
        private string orderStatus;
        private DateTime deliveryDateTime;
        private string deliveryAddress;
        private string orderPaymentMethod;
        private bool orderPaid;


        public int OrderId { get; set; }
        public double CalculateOrderTotal() { return orderTotal; }
        public void AddOrderedFoodItem(FoodItem item, int quantity) { }

        public bool RemoveOrderedFoodItem(FoodItem item) { return true; }
        public void DisplayOrderedFoodItems() { }

        public override string ToString()
        {
            return $"Order ID: {OrderId}, Total Amount: {orderTotal}, Status: {orderStatus}";
        }

    }
}
