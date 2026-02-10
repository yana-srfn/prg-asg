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

    }
    else if (choice == "2")
    {
        // Feature 2
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
        // Feature 3

    }
    else if (choice == "4")
    {
        // Feature 4

    }
    else if (choice == "5")
    {
        // Feature 5

    }
    else if (choice == "6")
    {
        // Feature 6

    }
    else if (choice == "7")
    {
        // Feature 7

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
