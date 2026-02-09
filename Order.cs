using PRGAssignment;
using System;
using System.Collections.Generic;

public class Order
{
    //===== attributes =====
    private int orderId;
    private DateTime orderDateTime;
    private double orderTotal;
    private string orderStatus;
    private DateTime deliveryDateTime;
    private string deliveryAddress;
    private string orderPaymentMethod;
    private bool orderPaid;

    private List<OrderedFoodItem> orderedFoodItems;

    //===== constructor =====
    public Order(int orderId, DateTime orderDateTime, DateTime deliveryDateTime, string deliveryAddress)
    {
        this.orderId = orderId;
        this.orderDateTime = orderDateTime;
        this.deliveryDateTime = deliveryDateTime;
        this.deliveryAddress = deliveryAddress;

        orderStatus = "Pending";
        orderPaid = false;
        orderedFoodItems = new List<OrderedFoodItem>();
    }

    //===== property =====
    public int OrderId
    {
        get { return orderId; }
    }

    //===== methods  =====
    public double CalculateOrderTotal()
    {
        orderTotal = 0;

        foreach (OrderedFoodItem item in orderedFoodItems)
        {
            orderTotal += item.CalculateSubtotal();
        }

        return orderTotal;
    }

    public void AddOrderedFoodItem(OrderedFoodItem item)
    {
        orderedFoodItems.Add(item);
    }

    public bool RemoveOrderedFoodItem(OrderedFoodItem item)
    {
        return orderedFoodItems.Remove(item);
    }

    public void DisplayOrderedFoodItems()
    {
        foreach (OrderedFoodItem item in orderedFoodItems)
        {
            Console.WriteLine(item);
        }
    }

    public override string ToString()
    {
        return $"Order ID: {orderId}, Total: ${CalculateOrderTotal():0.00}, Status: {orderStatus}";
    }
}