//Feature 2: Load CSV Files
using System.Globalization;

static void LoadCustomers(string filePath = @"Data-Files\customers.csv")
{
    customers.Clear();
    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines.Skip(1))
    {
        var fields = line.Split(',');
        customers.Add(new Customer
        {
            Name = fields[0],
            Email = fields[1]
        });
    }
    Console.WriteLine($"{customers.Count} customers loaded.");
}

static void LoadRestaurants(string filePath = @"Data-Files\restaurants.csv")
{
    restaurants.Clear();
    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines.Skip(1))
    {
        var fields = line.Split(',');
        restaurants.Add(new Restaurant
        {
            RestaurantId = fields[0],
            Name = fields[1],
            Email = fields[2]
        });
    }
    Console.WriteLine($"{restaurants.Count} restaurants loaded.");
}

static void LoadFoodItems(string filePath = @"Data-Files\fooditems.csv")
{
    foodItems.Clear();
    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines.Skip(1))
    {
        var fields = line.Split(',');
        var item = new FoodItem
        {
            RestaurantId = fields[0],
            ItemName = fields[1],
            Description = fields[2],
            Price = decimal.Parse(fields[3])
        };
        foodItems.Add(item);

        var rest = restaurants.FirstOrDefault(r => r.RestaurantId == item.RestaurantId);
        if (rest != null)
            rest.MenuItems.Add(item);
    }
    Console.WriteLine($"{foodItems.Count} food items loaded.");
}

static void LoadOrders(string filePath = @"Data-Files\orders.csv")
{
    orders.Clear();
    foreach (var r in restaurants)
        r.OrderQueue.Clear();
    foreach (var c in customers)
        c.OrderList.Clear();

    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines.Skip(1))
    {
        var fields = line.Split(',');
        var cust = customers.FirstOrDefault(c => c.Email == fields[1]);

        // get restaurant from first item
        var firstItemName = fields[8].Split(';')[0].Split(':')[0];
        var restId = foodItems.FirstOrDefault(f => f.ItemName == firstItemName)?.RestaurantId;
        var rest = restaurants.FirstOrDefault(r => r.RestaurantId == restId);

        if (cust != null && rest != null)
        {
            var order = new Order
            {
                OrderId = fields[0],
                Customer = cust,
                Restaurant = rest,
                DeliveryDateTime = DateTime.ParseExact(fields[2] + " " + fields[3], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                DeliveryAddress = fields[4],
                TotalAmount = decimal.Parse(fields[6]),
                Status = fields[7]
            };

            var itemsList = new List<(FoodItem, int)>();
            foreach (var item in fields[8].Split(';'))
            {
                var parts = item.Split(':');
                var food = rest.MenuItems.FirstOrDefault(f => f.ItemName == parts[0]);
                if (food != null)
                    itemsList.Add((food, int.Parse(parts[1])));
            }
            order.Items = itemsList;

            orders.Add(order);
            cust.OrderList.Add(order);
            rest.OrderQueue.Enqueue(order);
        }
    }
    Console.WriteLine($"{orders.Count} orders loaded.");
}

static void LoadSpecialOffers(string filePath = @"Data-Files\specialoffers.csv")
{
    specialOffers.Clear();
    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines.Skip(1))
    {
        var fields = line.Split(',');
        specialOffers.Add(new SpecialOffer
        {
            RestaurantId = fields[0],
            OfferCode = fields[1],
            Description = fields[2],
            DiscountAmount = decimal.Parse(fields[3])
        });
    }
    Console.WriteLine($"{specialOffers.Count} special offers loaded.");
}

//Feature 3: List Restaurants & Menu
static void ListRestaurants()
{
    Console.WriteLine("\nAll Restaurants and Menu Items");
    Console.WriteLine("==============================");
    foreach (var r in restaurants)
    {
        Console.WriteLine($"Restaurant: {r.Name} ({r.RestaurantId})");
        foreach (var f in r.MenuItems)
            Console.WriteLine($"- {f.ItemName}: {f.Description} - ${f.Price:F2}");
        Console.WriteLine();
    }
}

//Feature 5: Create New Order
static void CreateOrder()
{
    Console.Write("Enter Customer Email: ");
    string email = Console.ReadLine();
    var cust = customers.FirstOrDefault(c => c.Email == email);
    if (cust == null)
    {
        Console.WriteLine("Customer not found.");
        return;
    }

    Console.Write("Enter Restaurant ID: ");
    string restId = Console.ReadLine();






}