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
List<Customer> customers = new();
List<Restaurant> restaurants = new();
List<Order> orders = new();

// ==== START ====
LoadCustomers();
LoadRestaurants();
LoadFoodItems();
LoadOrders();

while (true)
{
    Console.WriteLine("\n1. List Restaurants & Menu");
    Console.WriteLine("2. Create New Order");
    Console.WriteLine("3. Modify Existing Order");
    Console.WriteLine("0. Exit");
    Console.Write("Choice: ");
    string choice = Console.ReadLine();

    if (choice == "1") ListRestaurants();
    else if (choice == "2") CreateNewOrder();
    else if (choice == "3") ModifyOrder();
    else if (choice == "0") break;
}

// =================================================
// FEATURE 2: LOAD FILES (customers & orders)
// =================================================
void LoadCustomers()
{
    foreach (string line in File.ReadAllLines("Data-Files/customers.csv").Skip(1))
    {
        string[] c = line.Split(',');
        customers.Add(new Customer(c[0], c[1]));
    }
}

void LoadRestaurants()
{
    foreach (string line in File.ReadAllLines("Data-Files/restaurants.csv").Skip(1))
    {
        string[] r = line.Split(',');
        restaurants.Add(new Restaurant(r[0], r[1], r[2]));
    }
    else
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }

void LoadFoodItems()
{
    foreach (string line in File.ReadAllLines("Data-Files/fooditems.csv").Skip(1))
    {
        string[] f = line.Split(',');
        Restaurant r = restaurants.First(x => x.RestaurantId == f[0]);
        r.AddFoodItem(new FoodItem(f[1], f[2], double.Parse(f[3])));
    }
}

void LoadOrders()
{
    foreach (string line in File.ReadAllLines("Data-Files/orders.csv").Skip(1))
    {
        string[] o = line.Split(',');

        int orderId = int.Parse(o[0]);
        string custEmail = o[1];
        string restId = o[2];
        DateTime deliveryDT = DateTime.Parse($"{o[3]} {o[4]}");

        Customer customer = customers.First(c => c.EmailAddress == custEmail);
        Restaurant restaurant = restaurants.First(r => r.RestaurantId == restId);

        Order order = new Order(orderId, DateTime.Now, deliveryDT, o[5]);

        customer.AddOrder(order);
        restaurant.EnqueueOrder(order);
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
        Console.WriteLine($"Restaurant: {r.Name} ({r.RestaurantId})");
        foreach (FoodItem f in r.Menu)
        {
            Console.WriteLine($"- {f.ItemName}: {f.Description} - ${f.Price:F2}");
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

    int newOrderId = orders.Max(o => o.OrderId) + 1;
    Order order = new Order(newOrderId, DateTime.Now, deliveryDT, address);

    while (true)
    {
        Console.WriteLine("\nAvailable Food Items:");
        for (int i = 0; i < restaurant.Menu.Count; i++)
            Console.WriteLine($"{i + 1}. {restaurant.Menu[i].ItemName} - ${restaurant.Menu[i].Price}");

        Console.Write("Enter item number (0 to finish): ");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 0) break;

        Console.Write("Enter quantity: ");
        int qty = int.Parse(Console.ReadLine());

        order.AddOrderedFoodItem(
            new OrderedFoodItem(restaurant.Menu[choice - 1], qty)
        );
    }

    double total = order.CalculateOrderTotal() + 5.0;
    Console.WriteLine($"Order Total: ${order.CalculateOrderTotal():0.00} + $5.00 (delivery) = ${total:0.00}");

    Console.Write("Proceed to payment? [Y/N]: ");
    if (Console.ReadLine().ToUpper() != "Y") return;

    Console.Write("Payment method [CC / PP / CD]: ");
    Console.ReadLine();

    customer.AddOrder(order);
    restaurant.EnqueueOrder(order);
    orders.Add(order);

    File.AppendAllText(
        "Data-Files/orders.csv",
        $"\n{order.OrderId},{customer.EmailAddress},{restaurant.RestaurantId},{date},{time},{address},{DateTime.Now},{total},Pending"
    );

    Console.WriteLine($"Order {order.OrderId} created successfully! Status: Pending");
}

// =================================================
// FEATURE 7: MODIFY EXISTING ORDER
// =================================================
void ModifyOrder()
{
    Console.Write("Enter Customer Email: ");
    Customer customer = customers.FirstOrDefault(c => c.EmailAddress == Console.ReadLine());
    if (customer == null) return;

    var pendingOrders = customer.OrderList.Where(o => o.ToString().Contains("Pending")).ToList();
    if (pendingOrders.Count == 0) return;

    Console.WriteLine("Pending Orders:");
    foreach (var o in pendingOrders)
        Console.WriteLine(o.OrderId);

    Console.Write("Enter Order ID: ");
    int id = int.Parse(Console.ReadLine());
    Order order = pendingOrders.First(o => o.OrderId == id);

    Console.WriteLine("Modify: [1] Items [2] Address [3] Delivery Time");
    string choice = Console.ReadLine();

    if (choice == "2")
    {
        Console.Write("Enter new Address: ");
        order.UpdateDeliveryAddress(Console.ReadLine());
    }
    else if (choice == "3")
    {
        Console.Write("Enter new Delivery Time (hh:mm): ");
        order.UpdateDeliveryTime(Console.ReadLine());
    }

    Console.WriteLine($"Order {order.OrderId} updated.");
}
