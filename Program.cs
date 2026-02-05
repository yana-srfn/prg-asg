//Feature 2: Load CSVs
using PRGAssignment;

static void LoadCustomers()
{
    customers.Clear();
    foreach (var line in File.ReadAllLines(@"Data-Files\customers.csv").Skip(1))
    {
        var fields = line.Split(',');
        customers.Add(new Customer { Name = fields[0], Email = fields[1] });
    }
}

static void LoadRestaurants()
{
    restaurants.Clear();
    foreach (var line in File.ReadAllLines(@"Data-Files\restaurants.csv").Skip(1))
    {
        var fields = line.Split(',');
        restaurants.Add(new Restaurant { RestaurantId = fields[0], Name = fields[1], Email = fields[2] });
    }
}

static void LoadFoodItems()
{
    foodItems.Clear();
    foreach (var line in File.ReadAllLines(@"Data-Files\fooditems.csv").Skip(1))
    {
        var fields = line.Split(',');
        var item = new FoodItem { RestaurantId = fields[0], ItemName = fields[1], Description = fields[2], Price = decimal.Parse(fields[3]) };
        foodItems.Add(item);

        var rest = restaurants.FirstOrDefault(r => r.RestaurantId == item.RestaurantId);
        if (rest != null) rest.MenuItems.Add(item);
    }
}

static void LoadOrders()
{
    orders.Clear();
    foreach (var r in restaurants) r.OrderQueue.Clear();
    foreach (var c in customers) c.OrderList.Clear();

    foreach (var line in File.ReadAllLines(@"Data-Files\orders.csv").Skip(1))
    {
        var f = line.Split(',');
        var cust = customers.FirstOrDefault(c => c.Email == f[1]);
        var firstItem = f[8].Split(';')[0].Split(':')[0];
        var rest = restaurants.FirstOrDefault(r => r.RestaurantId == foodItems.First(fi => fi.ItemName == firstItem).RestaurantId);

        if (cust != null && rest != null)
        {
            var order = new Order
            {
                OrderId = f[0],
                Customer = cust,
                Restaurant = rest,
                DeliveryDateTime = DateTime.ParseExact(f[2] + " " + f[3], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                DeliveryAddress = f[4],
                TotalAmount = decimal.Parse(f[6]),
                Status = f[7],
                Items = f[8].Split(';').Select(i =>
                {
                    var parts = i.Split(':');
                    return (rest.MenuItems.First(mi => mi.ItemName == parts[0]), int.Parse(parts[1]));
                }).ToList()
            };
            orders.Add(order);
            cust.OrderList.Add(order);
            rest.OrderQueue.Enqueue(order);
        }
    }
}

//Feature 3: List Restaurants & Menu
static void ListRestaurants()
{
    foreach (var r in restaurants)
    {
        Console.WriteLine($"Restaurant: {r.Name} ({r.RestaurantId})");
        foreach (var f in r.MenuItems)
            Console.WriteLine($"- {f.ItemName}: {f.Description} - ${f.Price:F2}");
        Console.WriteLine();
    }
}
