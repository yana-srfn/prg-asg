//==========================================================
// Student Number : S10268653
// Student Name : Ng Sook Min Calista
// Partner Name : Dayana
//==========================================================

// 1.
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

//
