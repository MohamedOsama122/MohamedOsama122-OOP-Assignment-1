using Part1_ProceduralToOOP;

var system = OrderManagementSystem.CreateWithSampleData();
Console.WriteLine("Object-Oriented Order System");
Console.WriteLine("Seeded sample data and the original demo scenario.");
PrintCustomers(system); PrintProducts(system); PrintOrders(system);
Console.WriteLine($"\nPaid sales total after demo: {system.PaidSalesTotal:F2}");

while (true)
{
    Console.WriteLine("\n---------- MENU ----------");
    Console.WriteLine("1) Print customers\n2) Print products\n3) Print all orders\n4) Print one order by id");
    Console.WriteLine("5) Create order\n6) Add line to order\n7) Mark order paid\n8) Show paid sales total\n0) Exit");
    Console.Write("Choice: ");
    try
    {
        switch (Console.ReadLine())
        {
            case "1": PrintCustomers(system); break;
            case "2": PrintProducts(system); break;
            case "3": PrintOrders(system); break;
            case "4": PrintOrder(system.FindOrder(ReadInt("Order id: "))); break;
            case "5": system.CreateOrder(ReadInt("Order id: "), ReadInt("Customer id: "), ReadDate("Date (YYYY-MM-DD): ")); break;
            case "6": system.FindOrder(ReadInt("Order id: ")).AddLine(system.FindProduct(ReadInt("Product id: ")), ReadInt("Quantity: ")); break;
            case "7": system.FindOrder(ReadInt("Order id: ")).Pay(); break;
            case "8": Console.WriteLine($"Paid sales total: {system.PaidSalesTotal:F2}"); break;
            case "0": Console.WriteLine("Bye."); return;
            default: Console.WriteLine("Unknown choice."); break;
        }
    }
    catch (Exception exception) when (exception is ArgumentException or InvalidOperationException or KeyNotFoundException or FormatException) { Console.WriteLine($"ERROR: {exception.Message}"); }
}

static int ReadInt(string prompt) { Console.Write(prompt); return int.TryParse(Console.ReadLine(), out var value) ? value : throw new FormatException("A whole number is required."); }
static DateOnly ReadDate(string prompt) { Console.Write(prompt); return DateOnly.TryParse(Console.ReadLine(), out var date) ? date : throw new FormatException("Use a valid date in YYYY-MM-DD format."); }
static void PrintCustomers(OrderManagementSystem system) { Console.WriteLine($"\n=== CUSTOMERS ({system.Customers.Count()}) ==="); foreach (var customer in system.Customers) Console.WriteLine($"#{customer.Id} {customer.Name} <{customer.Email}> {customer.City} vip={(customer.IsVip ? "yes" : "no")}"); }
static void PrintProducts(OrderManagementSystem system) { Console.WriteLine($"\n=== PRODUCTS ({system.Products.Count()}) ==="); foreach (var product in system.Products) Console.WriteLine($"#{product.Id} {product.Name} price={product.Price:F2} stock={product.Stock}"); }
static void PrintOrders(OrderManagementSystem system) { Console.WriteLine($"\n=== ALL ORDERS ({system.Orders.Count()}) ==="); foreach (var order in system.Orders) PrintOrder(order); }
static void PrintOrder(Order order) { Console.WriteLine($"\n=== ORDER #{order.Id} ===\nDate: {order.Date:yyyy-MM-dd}\nCustomer: {order.Customer.Name} (#{order.Customer.Id})\nPaid: {(order.IsPaid ? "yes" : "no")}\nLines:"); foreach (var line in order.Lines) Console.WriteLine($" - {line.Product.Name} x{line.Quantity} @{line.Product.Price:F2} = {line.Total:F2}"); Console.WriteLine($"TOTAL: {order.Total:F2}"); }
