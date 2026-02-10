//==========================================================
// Student Number : S10275337C
// Student Name   : Dayana Sharafeena
// Student Number : S10268653
// Student Name   : Ng Sook Min Calista
//==========================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PRGAssignment
{
    class Program
    {
        // ===================== DATA =====================
        static List<Restaurant> restaurants = new List<Restaurant>();
        static List<Customer> customers = new List<Customer>();
        static List<Order> orders = new List<Order>();
        static Stack<Order> refundStack = new Stack<Order>();

        // ===================== MAIN =====================
        static void Main(string[] args)
        {
            LoadRestaurants();
            LoadCustomers();
            LoadFoodItems();
            LoadOrders();

            while (true)
            {
                Console.WriteLine("\n===== Gruberoo Food Delivery System =====");
                Console.WriteLine("1. List all restaurants and menu items");
                Console.WriteLine("2. List all orders");
                Console.WriteLine("3. Create a new order");
                Console.WriteLine("4. Process an order");
                Console.WriteLine("5. Modify an existing order");
                Console.WriteLine("6. Delete an existing order");
                Console.WriteLine("7. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Feature3_ListRestaurants(); break;
                    case "2": Feature4_ListAllOrders(); break;
                    case "3": Feature5_CreateOrder(); break;
                    case "4": Feature6_ProcessOrder(); break;
                    case "5": Feature7_ModifyOrder(); break;
                    case "6": Feature8_DeleteOrder(); break;
                    case "7": return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // =================================================
        // FEATURE 2 – LOAD FILES (Dayana)
        // =================================================
        static void LoadRestaurants()
        {
            foreach (var line in File.ReadAllLines("Data-Files/restaurants.csv").Skip(1))
            {
                var r = line.Split(',');
                restaurants.Add(new Restaurant(r[0], r[1], r[2]));
            }
        }

        static void LoadCustomers()
        {
            foreach (var line in File.ReadAllLines("Data-Files/customers.csv").Skip(1))
            {
                var c = line.Split(',');
                customers.Add(new Customer(c[0], c[1]));
            }
        }

        static void LoadFoodItems()
        {
            foreach (var r in restaurants)
                r.AddMenu(new Menu("M1", "Main Menu"));

            foreach (var line in File.ReadAllLines("Data-Files/fooditems.csv").Skip(1))
            {
                var f = line.Split(',');
                Restaurant r = restaurants.First(x => x.RestaurantId == f[0]);
                r.GetMenus()[0].AddFoodItem(
                    new FoodItem(f[1], f[2], double.Parse(f[3]), "")
                );
            }
        }

        static void LoadOrders()
        {
            foreach (var line in File.ReadAllLines("Data-Files/orders.csv").Skip(1))
            {
                var o = line.Split(',');
                int id = int.Parse(o[0]);
                Customer c = customers.First(x => x.EmailAddress == o[1]);
                Restaurant r = restaurants.First(x => x.RestaurantId == o[2]);

                DateTime dt = DateTime.Parse($"{o[3]} {o[4]}");
                Order order = new Order(id, DateTime.Now, dt, o[5])
                {
                    OrderStatus = o[8],
                    OrderTotal = double.Parse(o[7])
                };

                c.AddOrder(order);
                r.ReceiveOrder(order);
                orders.Add(order);
            }
        }

        // =================================================
        // FEATURE 3 – LIST RESTAURANTS (Dayana)
        // =================================================
        static void Feature3_ListRestaurants()
        {
            Console.WriteLine("\nAll Restaurants and Menu Items");
            Console.WriteLine("==============================");

            foreach (var r in restaurants)
            {
                Console.WriteLine($"Restaurant: {r.RestaurantName} ({r.RestaurantId})");
                foreach (var m in r.GetMenus())
                    m.DisplayFoodItems();
                Console.WriteLine();
            }
        }

        // =================================================
        // FEATURE 4 – LIST ALL ORDERS (Calista)
        // =================================================
        static void Feature4_ListAllOrders()
        {
            Console.WriteLine("\nAll Orders");
            Console.WriteLine("==========");

            foreach (var o in orders)
                Console.WriteLine(o);
        }

        // =================================================
        // FEATURE 5 – CREATE ORDER (Dayana)
        // =================================================
        static void Feature5_CreateOrder()
        {
            Console.Write("Enter Customer Email: ");
            string email = Console.ReadLine();
            Customer c = customers.FirstOrDefault(x => x.EmailAddress == email);

            if (c == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write("Enter Restaurant ID: ");
            Restaurant r = restaurants.FirstOrDefault(x => x.RestaurantId == Console.ReadLine());
            if (r == null)
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

            int newId = orders.Count == 0 ? 1001 : orders.Max(o => o.OrderId) + 1;
            Order order = new Order(newId, DateTime.Now, deliveryDT, address);

            var items = r.GetMenus()[0].GetFoodItems();

            while (true)
            {
                for (int i = 0; i < items.Count; i++)
                    Console.WriteLine($"{i + 1}. {items[i].GetItemName()} - ${items[i].GetItemPrice():0.00}");

                Console.Write("Enter item number (0 to finish): ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice == 0)
                    break;

                Console.Write("Enter quantity: ");
                int qty = int.Parse(Console.ReadLine());

                order.AddOrderedFoodItem(new OrderedFoodItem(items[choice - 1], qty));
            }

            double total = order.CalculateOrderTotal() + 5.0;
            order.OrderTotal = total;

            c.AddOrder(order);
            r.ReceiveOrder(order);
            orders.Add(order);

            File.AppendAllText("Data-Files/orders.csv",
                $"\n{order.OrderId},{c.EmailAddress},{r.RestaurantId},{date},{time},{address},{DateTime.Now},{total},Pending");

            Console.WriteLine($"Order {order.OrderId} created successfully!");
        }

        // =================================================
        // FEATURE 6 – PROCESS ORDER (Calista)
        // =================================================
        static void Feature6_ProcessOrder()
        {
            Console.Write("Enter Restaurant ID: ");
            Restaurant r = restaurants.FirstOrDefault(x => x.RestaurantId == Console.ReadLine());
            if (r == null) return;

            int count = r.GetOrderQueue().Count;
            for (int i = 0; i < count; i++)
            {
                Order o = r.GetOrderQueue().Dequeue();
                Console.WriteLine(o);

                Console.Write("Action [C/R/D/S]: ");
                string act = Console.ReadLine().ToUpper();

                if (act == "C" && o.OrderStatus == "Pending")
                    o.OrderStatus = "Preparing";
                else if (act == "D" && o.OrderStatus == "Preparing")
                    o.OrderStatus = "Delivered";
                else if (act == "R" && o.OrderStatus == "Pending")
                {
                    o.OrderStatus = "Rejected";
                    refundStack.Push(o);
                }

                r.ReceiveOrder(o);
            }
        }

        // =================================================
        // FEATURE 7 – MODIFY ORDER (Dayana)
        // =================================================
        static void Feature7_ModifyOrder()
        {
            Console.Write("Enter Customer Email: ");
            Customer c = customers.FirstOrDefault(x => x.EmailAddress == Console.ReadLine());
            if (c == null) return;

            var pending = c.OrderList.Where(o => o.OrderStatus == "Pending").ToList();
            if (pending.Count == 0) return;

            Console.WriteLine("Pending Orders:");
            pending.ForEach(o => Console.WriteLine(o.OrderId));

            Console.Write("Enter Order ID: ");
            int id = int.Parse(Console.ReadLine());
            Order o = pending.First(x => x.OrderId == id);

            Console.WriteLine("[1] Address  [2] Delivery Time");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("New Address: ");
                o.DeliveryAddress = Console.ReadLine();
            }
            else if (choice == "2")
            {
                Console.Write("New Time (hh:mm): ");
                string t = Console.ReadLine();
                o.DeliveryDateTime = DateTime.Parse($"{o.DeliveryDateTime:dd/MM/yyyy} {t}");
            }

            Console.WriteLine("Order updated.");
        }

        // =================================================
        // FEATURE 8 – DELETE ORDER (Calista)
        // =================================================
        static void Feature8_DeleteOrder()
        {
            Console.Write("Enter Customer Email: ");
            Customer c = customers.FirstOrDefault(x => x.EmailAddress == Console.ReadLine());
            if (c == null) return;

            var pending = c.OrderList.Where(o => o.OrderStatus == "Pending").ToList();
            if (pending.Count == 0) return;

            Console.Write("Enter Order ID: ");
            int id = int.Parse(Console.ReadLine());
            Order o = pending.First(x => x.OrderId == id);

            o.OrderStatus = "Cancelled";
            refundStack.Push(o);

            Console.WriteLine($"Order {o.OrderId} cancelled.");
        }
    }
}
