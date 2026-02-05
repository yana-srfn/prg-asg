//==========================================================
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
// Partner Name : Dayana
//==========================================================

/* 
 TODO:
 1.Load files (restaurants and food items)
- load the restaurant.csv file
- create the Restaurant objects based on the data loaded
- load the fooditems.csv file
- create the FoodItem objects and assign them to their respective restaurants 

4. List all orders with basic information *Output in writeup pdf
- display ALL orders with the required details: Order ID, Customer Name, Restaurant Name, Delivery Time, Total Amount, and Order Status 

6. Process an order *Output in writeup pdf
- prompt the user to enter a restaurant ID
- Loop thru’ the Order Queue and display each order for processing.
- display the basic information of the order (order could be pending, cancelled or preparing)
- prompt the user to enter [C] for confirming, [R] for rejecting the order, [S] for skipping or [D] for delivering the order .
    • If confirming, check that the order status is "Pending" and update the order status to "Preparing".
    • If rejecting, check that the order status is "Pending", update the order status to "Rejected", add the order to the Refund Stack and display refund message.
    • If skipping, check that the order status is "Cancelled" and move on to next order.
    • If delivering, check that the order status is "Preparing" and update the order status to "Delivered".
- display a message indicating the outcome. 

8.Delete an existing order *Output in writeup pdf
- prompt the user to enter a Customer Email
- display all orders from the Order List that are "Pending"
- prompt the user to enter an Order ID
- display the basic information of the order
- prompt user to confirm deletion
- update the status of the order to “Cancelled”, add the order to the Refund Stack and display refund message
- display a message indicating the outcome. 
*/

using PRGAssignment;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;


// 1.
List<Restaurant> restaurants = new List<Restaurant>();
//Load Files
//Restaurants File 
void LoadRestaurants(string path)
{
    String[] lines = File.ReadAllLines("restaurants.csv");
    for (int i = 1; i < lines.Length; i++)
    {
        string[] data = lines[i].Split(',');
        string id = data[0];
        string Name = data[1];
        string Email = data[2];
        Restaurant restaurant = new Restaurant();
        restaurants.Add(restaurant);
    }
}
    LoadRestaurants("restaurants.csv");

//FoodItems File
void LoadFoodItems(string path)
{
    String[] lines = File.ReadAllLines("fooditems.csv");
    for (int i = 1; i < lines.Length; i++)
    {
        string[] data = lines[i].Split(',');
        string restId = data[0];
        string itemName = data[1];
        string itemDesc = data[2];
        double itemPrice = Convert.ToDouble(data[3]);

        // Create food item (customise not in CSV -> empty string)
        FoodItem fi = new FoodItem(itemName, itemDesc, itemPrice, "");
        foreach (Restaurant r in restaurants)
        {
            if (r.RestaurantId == restId)
            {
                foreach (Menu menu in r.Menus)
                {
                    menu.AddFoodItem(fi);
                }
            }
            else
            { continue; }
        }
    }
}
LoadFoodItems("fooditems.csv");
//List restaurants & food items
foreach (var r in restaurants)
{
    Console.WriteLine($"Restaurant: {r.RestaurantName}");

    // Restaurant contains menus, menus contain food items
    r.DisplayMenu();

    Console.WriteLine();
}


//4
void ListAllOrders(List<Order> orders)
{
    // Print section title
    Console.WriteLine("\nAll Orders");
    Console.WriteLine("==========");

    // Print table header
    Console.WriteLine("{0,-10}{1,-15}{2,-18}{3,-22}{4,-10}{5,-12}",
        "Order ID", "Customer", "Restaurant", "Delivery Date/Time", "Amount", "Status");
    Console.WriteLine("{0,-10}{1,-15}{2,-18}{3,-22}{4,-10}{5,-12}",
        "--------", "--------", "----------", "------------------", "------", "------");

    // Check if there are no orders in the list
    if (orders.Count == 0)
    {
        Console.WriteLine("No orders found.");
        return;
    }

    // Loop through each order in the orders list
    foreach (Order order in orders)
    {
        string customerName = "-";
        string restaurantName = "-";

        //get the customer's name
        if (order.Customer != null)
            customerName = order.Customer.Name;

        //get the restaurant's name
        if (order.Restaurant != null)
            restaurantName = order.Restaurant.RestaurantName;

        // Display the order details in one row
        Console.WriteLine("{0,-10}{1,-15}{2,-18}{3,-22}${4,-9:0.00}{5,-12}",
            order.OrderId,                                            //Order ID
            customerName,                                            //Customer name
            restaurantName,                                         //Restaurant name
            order.DeliveryDateTime.ToString("dd/MM/yyyy HH:mm"),   // Delivery date and time
            order.TotalAmount,                                    // Total amount
            order.Status);                                       // Order status
    }
}

//6
// Process orders for a specific restaurant (using the restaurant's order queue)

// Actions allowed (typical rules):
// C = Confirm: only valid if order is Pending  -> change to Preparing
// R = Reject : only valid if order is Pending  -> change to Rejected + push to refundStack
// S = Skip   : only valid if order is Cancelled -> leave it and move on
// D = Deliver: only valid if order is Preparing -> change to Delivered

//Find a restaurant using Restaurant ID
Restaurant FindRestaurantById(List<Restaurant> restaurants, string restId)
{
    for (int i = 0; i < restaurants.Count; i++)
    {
        if (restaurants[i].RestaurantId == restId)
            return restaurants[i];
    }
    return null; // not found
}
void ProcessOrder(List<Restaurant> restaurants, Stack<Order> refundStack)
{
    Console.WriteLine("\nProcess Order");
    Console.WriteLine("=========================");

    //Ask user for Restaurant ID
    Restaurant selectedRestaurant = null;        //for loop to work 
    while (selectedRestaurant == null)          // incase user put wrong id
    {
        Console.Write("Enter Restaurant ID: ");
        string inputId = Console.ReadLine();

        // basic empty check
        if (string.IsNullOrWhiteSpace(inputId))
        {
            Console.WriteLine("Restaurant ID cannot be empty.");
            continue;
        }

        inputId = inputId.Trim();                                       // Trim() is so that if user put a spacing behind wont have error
        selectedRestaurant = FindRestaurantById(restaurants, inputId);
        if (selectedRestaurant == null)
        {
            Console.WriteLine("Invalid Restaurant ID. Try again.");
        }
    }

    //Check if the restaurant queue is empty
    // Queue is used to ensure orders are processed in the order they are received (FIFO)
    if (selectedRestaurant.OrderQueue == null || selectedRestaurant.OrderQueue.Count == 0)
    {
        Console.WriteLine("No orders in this restaurant queue.");
        return;
    }

    Console.WriteLine($"\nRestaurant Selected: {selectedRestaurant.RestaurantName}");
    Console.WriteLine($"Orders in queue: {selectedRestaurant.OrderQueue.Count}");

    // going through each order currently in the queue
    // IMPORTANT!! If Dequeue, it disappears. -- "rotate" the queue: Dequeue -> process -> Enqueue back
    int numberOfOrdersToProcess = selectedRestaurant.OrderQueue.Count;

    for (int i = 0; i < numberOfOrdersToProcess; i++)
    {
        // Take the first order out
        Order currentOrder = selectedRestaurant.OrderQueue.Dequeue();

        // Show order details to user
        Console.WriteLine("\n-----------------------------------");
        Console.WriteLine($"Order ID: {currentOrder.OrderId}");

        // Customer name (in case null)
        if (currentOrder.Customer != null)
            Console.WriteLine($"Customer: {currentOrder.Customer.Name}");
        else
            Console.WriteLine("Customer: -");

        Console.WriteLine("Ordered Items:");

        // Show items (in case list is null)
        if (currentOrder.Items != null && currentOrder.Items.Count > 0)
        {
            for (int k = 0; k < currentOrder.Items.Count; k++)
            {
                // OrderItem has Item + Quantity (common pattern)
                FoodItem fi = currentOrder.Items[k].Item;
                int qty = currentOrder.Items[k].Quantity;

                // FoodItem safe name
                string itemName = (fi != null) ? fi.GetItemName() : "-";
                Console.WriteLine($" {k + 1}. {itemName} x {qty}");
            }
        }
        else
        {
            Console.WriteLine(" (No items)");
        }

        Console.WriteLine($"Delivery: {currentOrder.DeliveryDateTime:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Total Amount: ${currentOrder.TotalAmount:0.00}");
        Console.WriteLine($"Current Status: {currentOrder.Status}");
        Console.WriteLine("-----------------------------------");

        //Ask for action
        string action = "";
        while (true)
        {
            Console.Write("Choose action [C]onfirm / [R]eject / [S]kip / [D]eliver: ");
            action = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(action))
                continue;

            action = action.Trim().ToUpper();

            if (action == "C" || action == "R" || action == "S" || action == "D")
                break;

            Console.WriteLine("Invalid choice. Please enter C, R, S, or D.");
        }

        // Update status based on rules
        // action C
        if (action == "C")
        {
            // Confirm only makes sense for Pending
            if (currentOrder.Status == "Pending")
            {
                currentOrder.Status = "Preparing";
                Console.WriteLine($"Order {currentOrder.OrderId} confirmed. Status changed to Preparing.");
            }
            else
            {
                Console.WriteLine("Cannot confirm. Only Pending orders can be confirmed.");
            }
        }
        //action R
        else if (action == "R")
        {
            // Reject only makes sense for Pending
            if (currentOrder.Status == "Pending")
            {
                currentOrder.Status = "Rejected";

                // Push to refund stack (so later you can show refunds or keep record)
                refundStack.Push(currentOrder);

                Console.WriteLine($"Order {currentOrder.OrderId} rejected.");
                Console.WriteLine($"Refund of ${currentOrder.TotalAmount:0.00} processed.");
            }
            else
            {
                Console.WriteLine("Cannot reject. Only Pending orders can be rejected.");
            }
        }
        //action S
        else if (action == "S")
        {
            // Skip is meant for Cancelled orders (we just ignore and move on)
            if (currentOrder.Status == "Cancelled")
            {
                Console.WriteLine($"Order {currentOrder.OrderId} skipped (Cancelled).");
            }
            else
            {
                Console.WriteLine("Skip is only valid for Cancelled orders.");
            }
        }
        //action D
        else if (action == "D")
        {
            // Deliver only makes sense when already Preparing
            if (currentOrder.Status == "Preparing")
            {
                currentOrder.Status = "Delivered";
                Console.WriteLine($"Order {currentOrder.OrderId} delivered. Status changed to Delivered.");
            }
            else
            {
                Console.WriteLine("Cannot deliver. Only Preparing orders can be delivered.");
            }
        }
        // 6) Put the order back into the queue so the queue is preserved (important for saving queue.csv later)
        selectedRestaurant.OrderQueue.Enqueue(currentOrder);
    }
}

//8

