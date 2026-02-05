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



//6
//8

