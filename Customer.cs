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
    public class Customer
    {
        // ===== attributes =====
        private string emailAddress;
        private string customerName;
        private List<Order> orderList;

        // ===== constructor =====
        public Customer(string customerName, string emailAddress)
        {
            this.customerName = customerName;
            this.emailAddress = emailAddress;
            orderList = new List<Order>();
        }

        // ===== properties =====
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public List<Order> OrderList
        {
            get { return orderList; }
        }

        // ===== methods =====
        public void AddOrder(Order order)
        {
            orderList.Add(order);
        }

        public bool RemoveOrder(Order order)
        {
            return orderList.Remove(order);
        }

        public void DisplayAllOrders()
        {
            foreach (Order order in orderList)
            {
                Console.WriteLine(order);
            }
        }

        public override string ToString()
        {
            return $"Customer Name: {customerName}, Email Address: {emailAddress}";
        }
    }
}
