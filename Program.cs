//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
//==========================================================

using PRGAssignment;
using System;
using System.Collections.Generic;

//Main Part
// Declare lists first
List<Restaurant> restaurants = new List<Restaurant>();
List<Customer> customers = new List<Customer>();
List<Order> orders = new List<Order>();

// Load files (your existing load methods)
void LoadRestaurants(string path)
{
    // read restaurants.csv
}

void LoadCustomers(string path)
{
    // read customers.csv
}

void LoadOrders(string path)
{
    // read orders.csv
}
void PrintStartupMessage(int restaurantCount, int foodItemCount, int customerCount, int orderCount)
{
    Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
    Console.WriteLine($"{restaurantCount} restaurants loaded!");
    Console.WriteLine($"{foodItemCount} food items loaded!");
    Console.WriteLine($"{customerCount} customers loaded!");
    Console.WriteLine($"{orderCount} orders loaded!");
    Console.WriteLine();
}

LoadRestaurants("restaurants.csv");
LoadCustomers("customers.csv");
LoadOrders("orders.csv");

//After loading, compute total food items
/*int GetTotalFoodItemCount(List<Restaurant> restaurants)
{
    int total = 0;
    for (int i = 0; i < restaurants.Count; i++)
    {
        total += restaurants[i].menu.foodItems.Count;
    }
    return total;
}*/


// Startup output (matches assignment screenshot)
PrintStartupMessage(
    restaurants.Count,
     0,                 // TEMP: food items count
    customers.Count,
    orders.Count
);

// MAIN MENU LOOP
while (true)
{
    Console.WriteLine("===== Gruberoo Food Delivery System =====");
    Console.WriteLine("1. List all restaurants and menu items");
    Console.WriteLine("2. List all orders");
    Console.WriteLine("3. Create a new order");
    Console.WriteLine("4. Process an order");
    Console.WriteLine("5. Modify an existing order");
    Console.WriteLine("6. Delete an existing order");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");

    string choice = Console.ReadLine();
    Console.WriteLine();

    if (choice == "1")
    {
        // Feature 1
        List<Restaurant> restaurants1 = new List<Restaurant>();
        //Load Files
        //Restaurants File 
        void LoadRestaurants1(string path)
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


    }
    else if (choice == "2")
    {
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

    }
    else if (choice == "3")
    {
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

    }
    else if (choice == "4")
    {
        // Feature 4
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
                if (order.Customer.CustomerName != null)
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
                    order.CalculateOrderTotal                             // Total amount

                    order.Status);                                       // Order status
            }
        }

    }
    else if (choice == "5")
    {
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

        }
    else if (choice == "6")
    {
            // Feature 6
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
                    // Put the order back into the queue so the queue is preserved (important for saving queue.csv later)
                    selectedRestaurant.OrderQueue.Enqueue(currentOrder);
                }
            }


        }
        else if (choice == "7")
    {
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

        }
    else if (choice == "0")
    {
        Console.WriteLine("Exiting system...");
        break;
    }
    else
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }

    Console.WriteLine();
}
