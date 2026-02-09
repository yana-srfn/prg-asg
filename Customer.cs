using System.Collections.Generic;
using PRGAssignment;
using System;

public class Customer
{
    private string emailAddress;
    private string customerName;


    public string CustomerName { get { return customerName; } set { customerName = value; } }
    public string EmailAddress { get { return emailAddress; } set { emailAddress = value; } }
    public List<Order> OrderList { get { return Order; } set { Order = value; } } = new List<Order>();

    public void AddOrder(Order order)
    {
        OrderList.Add(order);
    }

    public void DisplayAllOrders()
    {
        foreach (Order order in OrderList)
        {
            System.Console.WriteLine(order);
        }
    }

    public bool RemoveOrder(Order order) { return OrderList.Remove(order); }

    public override string ToString()
    {
        return $"Customer Name: {CustomerName}, Email Address: {EmailAddress}";
    }
}