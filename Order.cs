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
        public string OrderId { get; set; }
        public Customer Customer { get; set; }
        public Restaurant Restaurant { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public List<(FoodItem Item, int Quantity)> Items { get; set; } = new List<(FoodItem, int)>();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
