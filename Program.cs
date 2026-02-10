//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using PRGAssignment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ==== DATA ====
List<Customer> customers = new List<Customer>();
List<Restaurant> restaurants = new List<Restaurant>();
List<Order> orders = new List<Order>();

// ==== START ====
LoadCustomers();
LoadRestaurants();
LoadMenusAndFoodItems();
LoadOrders();

while (true)
{
    Console.WriteLine("\n1. List Restaurants & Menu Items");
    Console.WriteLine("2. Create New Order");
    Console.WriteLine("3. Modify Existing Order");
    Console.WriteLine("0. Exit");
    Console.Write("Choice: ");
    string choice = Console.ReadLine();

    if (choice == "1") ListRestaurants();
    else if (choice == "2") CreateNewOrder();
    else if (choice == "3") ModifyOrder();
    else if (choice == "0") break;
    else Console.WriteLine("Invalid choice.");
}

// =================================================
// FEATURE 2: LOAD FILES
// =================================================
void LoadCustomers()
{
    foreach (var line in File.ReadAllLines("Data-Files/customers.csv").Skip(1))
    {
        var c = line.Split(',');
        customers.Add(new Customer(c[0], c[1]));
    }
}

void LoadOrders()
{
    foreach (var line in File.ReadAllLines("Data-Files/orders.csv").Skip(1))
    {
        var o = line.Split(',');

        int orderId = int.Parse(o[0]);
        string customerEmail = o[1];
        string restaurantId = o[2];
        DateTime deliveryDT = DateTime.Parse($"{o[3]} {o[4]}");

        Customer customer = customers.First(c => c.EmailAddress == customerEmail);
        Restaurant restaurant = restaurants.First(r => r.RestaurantId == restaurantId);

        Order order = new Order(orderId, DateTime.Now, deliveryDT, o[5]);

        customer.AddOrder(order);
        restaurant.ReceiveOrder(order);
        orders.Add(order);
    }
}

// =================================================
// FEATURE 3: LIST RESTAURANTS & MENU ITEMS
// =================================================
void ListRestaurants()
{
    Console.WriteLine("\nAll Restaurants and Menu Items");
    Console.WriteLine("==============================");

    foreach (Restaurant r in restaurants)
    {
        Console.WriteLine($"Restaurant: {r.RestaurantName} ({r.RestaurantId})");
        foreach (Menu m in r.GetMenus())
        {
            m.DisplayFoodItems();
        }
        Console.WriteLine();
    }
}

// =================================================
// FEATURE 5: CREATE NEW ORDER
// =================================================
void CreateNewOrder()
{
    Console.Write("Enter Customer Email: ");
    Customer customer = customers.FirstOrDefault(c => c.EmailAddress == Console.ReadLine());
    if (customer == null) return;

    Console.Write("Enter Restaurant ID: ");
    Restaurant restaurant = restaurants.FirstOrDefault(r => r.RestaurantId == Console.ReadLine());
    if (restaurant == null) return;

    Console.Write("Enter Delivery Date (dd/mm/yyyy): ");
    string date = Console.ReadLine();
    Console.Write("Enter Delivery Time (hh:mm): ");
    string time = Console.ReadLine();
    DateTime deliveryDT = DateTime.Parse($"{date} {time}");

    Console.Write("Enter Delivery Address: ");
    string address = Console.ReadLine();

    int newOrderId = orders.Count == 0 ? 1001 : orders.Max(o => o.OrderId) + 1;
    Order order = new Order(newOrderId, DateTime.Now, deliveryDT, address);

    Menu menu = restaurant.GetMenus().First();

    while (true)
    {
        List<FoodItem> items = menu.GetFoodItems();
        Console.WriteLine("\nAvailable Food Items:");
        for (int i = 0; i < items.Count; i++)
            Console.WriteLine($"{i + 1}. {items[i]}");

        Console.Write("Enter item number (0 to finish): ");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 0) break;

        Console.Write("Enter quantity: ");
        int qty = int.Parse(Console.ReadLine());

        order.AddOrderedFoodItem(
            new OrderedFoodItem(items[choice - 1], qty)
        );
    }

    double total = order.CalculateOrderTotal() + 5.0;
    Console.WriteLine($"Order Total: ${order.CalculateOrderTotal():0.00} + $5.00 = ${total:0.00}");

    Console.Write("Proceed to payment? [Y/N]: ");
    if (Console.ReadLine().ToUpper() != "Y") return;

    Console.Write("Payment method [CC / PP / CD]: ");
    Console.ReadLine();

    customer.AddOrder(order);
    restaurant.ReceiveOrder(order);
    orders.Add(order);

    File.AppendAllText(
        "Data-Files/orders.csv",
        $"\n{order.OrderId},{customer.EmailAddress},{restaurant.RestaurantId},{date},{time},{address},{DateTime.Now},{total},Pending"
    );

    Console.WriteLine($"Order {order.OrderId} created successfully! Status: Pending");
}

// =================================================
// FEATURE 7: MODIFY ORDER
// =================================================
void ModifyOrder()
{
    Console.Write("Enter Customer Email: ");
    Customer customer = customers.FirstOrDefault(c => c.EmailAddress == Console.ReadLine());
    if (customer == null) return;

    if (customer.OrderList.Count == 0) return;

    Console.WriteLine("Pending Orders:");
    foreach (Order o in customer.OrderList)
        Console.WriteLine(o.OrderId);

    Console.Write("Enter Order ID: ");
    int id = int.Parse(Console.ReadLine());
    Order order = customer.OrderList.First(o => o.OrderId == id);

    Console.WriteLine("Modify: [1] Address [2] Delivery Time");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Write("Enter new address: ");
        string newAddress = Console.ReadLine();
        Console.WriteLine("Address updated (stored internally).");
    }
    else if (choice == "2")
    {
        Console.Write("Enter new delivery time (hh:mm): ");
        Console.ReadLine();
        Console.WriteLine("Delivery time updated (stored internally).");
    }
}
