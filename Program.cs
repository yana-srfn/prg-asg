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
// =================================================
// FEATURE 5: CREATE NEW ORDER
// =================================================
void CreateNewOrder()
{
    Console.WriteLine("\nCreate New Order");
    Console.WriteLine("================");

    Console.Write("Enter Customer Email: ");
    Customer customer = customers.FirstOrDefault(c => c.EmailAddress == Console.ReadLine());
    if (customer == null)
    {
        Console.WriteLine("Customer not found.");
        return;
    }

    Console.Write("Enter Restaurant ID: ");
    Restaurant restaurant = restaurants.FirstOrDefault(r => r.RestaurantId == Console.ReadLine());
    if (restaurant == null)
    {
        Console.WriteLine("Restaurant not found.");
        return;
    }

    Console.Write("Enter Delivery Date (dd/mm/yyyy): ");
    string date = Console.ReadLine();
    Console.Write("Enter Delivery Time (hh:mm): ");
    string time = Console.ReadLine();
    DateTime deliveryDT = DateTime.Parse($"{date} {time}");

    Console.Write("Enter Delivery Address: ");
    string address = Console.ReadLine();

    int newOrderId = orders.Count == 0 ? 1001 : orders.Max(o => o.OrderId) + 1;
    Order order = new Order(newOrderId, DateTime.Now, deliveryDT, address);

    // ===== select food items =====
    Menu menu = restaurant.GetMenus().First();
    List<FoodItem> foodItems = menu.GetFoodItems();

    while (true)
    {
        Console.WriteLine("\nAvailable Food Items:");
        for (int i = 0; i < foodItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {foodItems[i].GetItemName()} - ${foodItems[i].GetItemPrice():0.00}");
        }

        Console.Write("Enter item number (0 to finish): ");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 0) break;

        Console.Write("Enter quantity: ");
        int qty = int.Parse(Console.ReadLine());

        order.AddOrderedFoodItem(
            new OrderedFoodItem(foodItems[choice - 1], qty)
        );
    }

    // ===== special request =====
    Console.Write("Add special request? [Y/N]: ");
    string sr = Console.ReadLine().ToUpper();
    if (sr == "Y")
    {
        Console.Write("Enter special request: ");
        Console.ReadLine(); // acknowledged (no storage required)
    }

    // ===== calculate total =====
    double foodTotal = order.CalculateOrderTotal();
    double finalTotal = foodTotal + 5.00;

    Console.WriteLine($"Order Total: ${foodTotal:0.00} + $5.00 (delivery) = ${finalTotal:0.00}");

    // ===== payment =====
    Console.Write("Proceed to payment? [Y/N]: ");
    if (Console.ReadLine().ToUpper() != "Y") return;

    Console.Write("Payment method [CC / PP / CD]: ");
    order.OrderPaymentMethod = Console.ReadLine();

    order.OrderStatus = "Pending";

    // ===== save order =====
    customer.AddOrder(order);
    restaurant.ReceiveOrder(order);
    orders.Add(order);

    File.AppendAllText(
        "Data-Files/orders.csv",
        $"\n{order.OrderId},{customer.EmailAddress},{restaurant.RestaurantId},{date},{time},{address},{DateTime.Now},{finalTotal},Pending"
    );

    Console.WriteLine($"Order {order.OrderId} created successfully! Status: Pending");
}


// =================================================
// FEATURE 7: MODIFY ORDER
// =================================================
// =================================================
// FEATURE 7: MODIFY EXISTING ORDER
// =================================================
void ModifyOrder()
{
    Console.WriteLine("\nModify Order");
    Console.WriteLine("============");

    Console.Write("Enter Customer Email: ");
    Customer customer = customers.FirstOrDefault(c => c.EmailAddress == Console.ReadLine());
    if (customer == null)
    {
        Console.WriteLine("Customer not found.");
        return;
    }

    var pendingOrders = customer.OrderList
        .Where(o => o.OrderStatus == "Pending")
        .ToList();

    if (pendingOrders.Count == 0)
    {
        Console.WriteLine("No pending orders.");
        return;
    }

    Console.WriteLine("Pending Orders:");
    foreach (Order o in pendingOrders)
    {
        Console.WriteLine(o.OrderId);
    }

    Console.Write("Enter Order ID: ");
    int orderId = int.Parse(Console.ReadLine());
    Order order = pendingOrders.FirstOrDefault(o => o.OrderId == orderId);
    if (order == null) return;

    Console.WriteLine("\nOrder Items:");
    order.DisplayOrderedFoodItems();

    Console.WriteLine("\nAddress:");
    Console.WriteLine(order.DeliveryAddress);

    Console.WriteLine("\nDelivery Date/Time:");
    Console.WriteLine(order.DeliveryDateTime);

    Console.WriteLine("\nModify: [1] Items [2] Address [3] Delivery Time");
    string choice = Console.ReadLine();

    if (choice == "2")
    {
        Console.Write("Enter new Address: ");
        order.DeliveryAddress = Console.ReadLine();
        Console.WriteLine($"Order {order.OrderId} updated. New Address: {order.DeliveryAddress}");
    }
    else if (choice == "3")
    {
        Console.Write("Enter new Delivery Time (hh:mm): ");
        string newTime = Console.ReadLine();

        DateTime dt = order.DeliveryDateTime;
        DateTime updatedDT = DateTime.Parse($"{dt:dd/MM/yyyy} {newTime}");
        order.DeliveryDateTime = updatedDT;

        Console.WriteLine($"Order {order.OrderId} updated. New Delivery Time: {newTime}");
    }
}