//==========================================================
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
// Partner Name : Dayana
//==========================================================
using PRGAssignment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


// 1.
List<Restaurant> restaurants = new List<Restaurant>();
//Load Files
void LoadRestaurants(string path)
{
    foreach (var line in File.ReadAllLines(path).Skip(1))
    {
        var p = line.Split(',');
        restaurants.Add(new Restaurant(p[0], p[1], p[2]));
    }
}
void LoadFoodItems(string path)
{
    foreach (var line in File.ReadAllLines(path).Skip(1))
    {
        var p = line.Split(',');
        string restId = p[0].Trim();
        string itemName = p[1].Trim();
        string itemDesc = p[2].Trim();
        double itemPrice = double.Parse(p[3].Trim(), CultureInfo.InvariantCulture);

        Restaurant r = restaurants.FirstOrDefault(x => x.GetRestaurantId() == restId);
        if (r == null) continue;

        // Ensure the restaurant has at least 1 menu (Main Menu)
        if (r.GetMenus().Count == 0)
        {
            r.AddMenu(new Menu("M1", "Main Menu"));
        }

        // Create food item (customise not in CSV -> empty string)
        FoodItem fi = new FoodItem(itemName, itemDesc, itemPrice, "");

        // Add food item into the first menu
        r.GetMenus()[0].AddFoodItem(fi);
    }
}
LoadRestaurants("restaurants.csv");
LoadFoodItems("fooditems.csv");


// Feature 1: List restaurants & food items
foreach (var r in restaurants)
{
        Console.WriteLine($"Restaurant: {r.GetRestaurantName()}");

        // Restaurant contains menus, menus contain food items
        r.DisplayMenu();

        Console.WriteLine();
}

//4
Console.WriteLine("OrderID  Customer        Restaurant       Delivery Time        Amount   Status");

/*foreach (var o in Orders) // haven't pull the class needed in github yet
{
    Console.WriteLine(
        $"{o.OrderId,-7} " +
        $"{o.Customer.Name,-15} " +
        $"{o.Restaurant.Name,-15} " +
        $"{o.DeliveryDateTime:dd/MM/yyyy HH:mm,-20} " +
        $"${o.TotalAmount,-7:0.00} " +
        $"{o.Status}"
    );
}*/

//6
//8

