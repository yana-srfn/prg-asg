//==========================================================
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
// Partner Name : Dayana
//==========================================================
using prg_asg;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


// 1.
List<Restaurant> restaurants = new List<Restaurant>(); // haven't pull the class needed in github yet
// Feature 1: List restaurants & food items
foreach (var r in restaurants)
{
    Console.WriteLine($"Restaurant: {r.Name}");

    foreach (var item in r.FoodItems)
    {
        Console.WriteLine(
            $" - {item.ItemName}: {item.Description} (${item.Price:0.00})"
        );
    }

    Console.WriteLine();
}

//4
Console.WriteLine("OrderID  Customer        Restaurant       Delivery Time        Amount   Status");

foreach (var o in Orders) // haven't pull the class needed in github yet
{
    Console.WriteLine(
        $"{o.OrderId,-7} " +
        $"{o.Customer.Name,-15} " +
        $"{o.Restaurant.Name,-15} " +
        $"{o.DeliveryDateTime:dd/MM/yyyy HH:mm,-20} " +
        $"${o.TotalAmount,-7:0.00} " +
        $"{o.Status}"
    );
}

//6
//8

