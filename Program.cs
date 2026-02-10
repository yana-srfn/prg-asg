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
