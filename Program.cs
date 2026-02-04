static void Main(string[] args)
{
    // Load all CSVs into master lists
    LoadCustomers();
    LoadRestaurants();
    LoadFoodItems();
    LoadOrders();
    LoadSpecialOffers();

    // Interactive menu for features
    while (true)
    {
        Console.WriteLine("\nSelect Feature:");
        Console.WriteLine("1. List Restaurants & Menu");
        Console.WriteLine("2. Create Order");
        Console.WriteLine("3. Modify Order");
        Console.WriteLine("0. Exit");
        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": ListRestaurants(); break;
            case "2": CreateOrder(); break;
            case "3": ModifyOrder(); break;
            case "0": return;
            default: Console.WriteLine("Invalid choice"); break;
        }
    }
}
