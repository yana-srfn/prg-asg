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
    public class Order
    {
        // ===== properties (public) =====
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderPaymentMethod { get; set; }
        public bool OrderPaid { get; set; }

        public List<OrderedFoodItem> OrderedFoodItems { get; set; }

        // ===== constructor =====
        public Order(int orderId, DateTime orderDateTime, DateTime deliveryDateTime, string deliveryAddress)
        {
            OrderId = orderId;
            OrderDateTime = orderDateTime;
            DeliveryDateTime = deliveryDateTime;
            DeliveryAddress = deliveryAddress;

            OrderStatus = "Pending";
            OrderPaid = false;
            OrderedFoodItems = new List<OrderedFoodItem>();
        }

        // ===== methods =====
        public double CalculateOrderTotal()
        {
            OrderTotal = 0;

            foreach (OrderedFoodItem item in OrderedFoodItems)
            {
                OrderTotal += item.CalculateSubtotal();
            }

            return OrderTotal;
        }

        public void AddOrderedFoodItem(OrderedFoodItem item)
        {
            OrderedFoodItems.Add(item);
        }

        public bool RemoveOrderedFoodItem(OrderedFoodItem item)
        {
            return OrderedFoodItems.Remove(item);
        }

        public void DisplayOrderedFoodItems()
        {
            foreach (OrderedFoodItem item in OrderedFoodItems)
            {
                Console.WriteLine(item);
            }
        }

        public override string ToString()
        {
            return $"Order ID: {OrderId}, Total: ${CalculateOrderTotal():0.00}, Status: {OrderStatus}";
        }
    }
}
