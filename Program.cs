//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Partner Name : Ng Sook Min Calista
//==========================================================
using System;
using System.Collections.Generic;
using System.IO;
using PRGAssignment;


//Lists (from master)
public static List<Customer> CustomerName = new List<Customer>();
public static List<Restaurant> restaurants = new List<Restaurant>();
public static List<FoodItem> foodItems = new List<FoodItem>();
public static List<Order> orders = new List<Order>();
public static List<SpecialOffer> specialOffers = new List<SpecialOffer>();

static void Main(string[] args)
{
    LoadCustomers();
    LoadOrders();

    Console.WriteLine("Data loaded successfully.");
}


//Feature 2: Load CSVs
static void LoadCustomers()
        {
            string[] lines = File.ReadAllLines("Data-Files/customers.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] cols = lines[i].Split(',');

                if (cols.Length < 2)
                    continue;

                Customer c = new Customer
                {
                    CustomerName = cols[0],
                    Email = cols[1]
                };

        CustomerName.Add(c);
            }
        }

static void LoadOrders()
{
    string[] lines = File.ReadAllLines("Data-Files/orders.csv");

    for (int i = 1; i < lines.Length; i++)
    {
        string[] cols = lines[i].Split(',');

        if (cols.Length < 10)
            continue;

        Order o = new Order
        {
            OrderId = cols[0],
            DeliveryAddress = cols[5],
            Status = cols[8]
        };

        if (decimal.TryParse(cols[7], out decimal total))
        {
            o.TotalAmount = total;
        }
        else
        {
            o.TotalAmount = 0m;
        }

        orders.Add(o);
    }
}


//Feature 3: List Restaurants & Menu
static void ListRestaurants()
{
    foreach (var r in restaurants)
    {
        Console.WriteLine($"Restaurant: {r.Name} ({r.RestaurantId})");

        foreach (var f in r.MenuItems)
        {
            Console.WriteLine($"- {f.ItemName}: {f.Description} - ${f.Price:F2}");
        }

        Console.WriteLine();
    }
}

//Feature 5: Create New Order 
static void CreateNewOrder(Customer customer, Restaurant restaurant, List<FoodItem> items, string deliveryAddress)
{
    Order newOrder = new Order
    {
        OrderId = Guid.NewGuid().ToString(), //WHATS GUID?
        CustomerName = customer,
        Restaurant = restaurant,
        Items = items,
        DeliveryAddress = deliveryAddress,
        Status = "Pending",
        TotalAmount = 0m
    };
    foreach (var item in foodItems)
    {
        newOrder.TotalAmount += item.Price;
    }
    orders.Add(newOrder);
    Console.WriteLine($"New order created with ID: {newOrder.OrderId}, Total Amount: ${newOrder.TotalAmount:F2}");
}

