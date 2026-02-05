//==========================================================
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
// Partner Name : Dayana
//==========================================================
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
LoadRestaurants("restaurants.csv");

//FoodItems File
void LoadFoodItems(string path)
{
    String[] lines =File.ReadAllLines("fooditems.csv");
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


// Feature 1: List restaurants & food items
foreach (var r in restaurants)
{
        Console.WriteLine($"Restaurant: {r.RestaurantName}");

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

