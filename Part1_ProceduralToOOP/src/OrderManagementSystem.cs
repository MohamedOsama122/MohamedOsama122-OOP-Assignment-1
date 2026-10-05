namespace Part1_ProceduralToOOP;

public sealed class OrderManagementSystem
{
    private readonly Dictionary<int, Customer> _customers = [];
    private readonly Dictionary<int, Product> _products = [];
    private readonly Dictionary<int, Order> _orders = [];

    public IEnumerable<Customer> Customers => _customers.Values.OrderBy(customer => customer.Id);
    public IEnumerable<Product> Products => _products.Values.OrderBy(product => product.Id);
    public IEnumerable<Order> Orders => _orders.Values.OrderBy(order => order.Id);
    public decimal PaidSalesTotal => Orders.Where(order => order.IsPaid).Sum(order => order.Total);

    public void AddCustomer(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        if (!_customers.TryAdd(customer.Id, customer)) throw new InvalidOperationException($"Customer id {customer.Id} already exists.");
    }

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (!_products.TryAdd(product.Id, product)) throw new InvalidOperationException($"Product id {product.Id} already exists.");
    }

    public Order CreateOrder(int orderId, int customerId, DateOnly date)
    {
        if (_orders.ContainsKey(orderId)) throw new InvalidOperationException($"Order id {orderId} already exists.");
        var order = new Order(orderId, FindCustomer(customerId), date);
        _orders.Add(orderId, order);
        return order;
    }

    public Order FindOrder(int orderId) => _orders.TryGetValue(orderId, out var order) ? order : throw new KeyNotFoundException($"Order id {orderId} was not found.");
    public Product FindProduct(int productId) => _products.TryGetValue(productId, out var product) ? product : throw new KeyNotFoundException($"Product id {productId} was not found.");
    private Customer FindCustomer(int customerId) => _customers.TryGetValue(customerId, out var customer) ? customer : throw new KeyNotFoundException($"Customer id {customerId} was not found.");

    public static OrderManagementSystem CreateWithSampleData()
    {
        var system = new OrderManagementSystem();
        system.AddCustomer(new Customer(1, "Mona Ali", "mona@example.com", "Cairo", true));
        system.AddCustomer(new Customer(2, "Omar Hassan", "omar@example.com", "Alexandria", false));
        system.AddCustomer(new Customer(3, "Sara Nabil", "sara@example.com", "Giza", false));
        system.AddProduct(new Product(101, "USB Cable", 50m, 100));
        system.AddProduct(new Product(102, "Wireless Mouse", 250m, 40));
        system.AddProduct(new Product(103, "Mechanical Keyboard", 1200m, 15));
        system.AddProduct(new Product(104, "Laptop Stand", 400m, 25));
        var first = system.CreateOrder(1001, 1, new DateOnly(2026, 9, 15)); first.AddLine(system.FindProduct(101), 2); first.AddLine(system.FindProduct(102), 1); first.Pay();
        var second = system.CreateOrder(1002, 2, new DateOnly(2026, 9, 15)); second.AddLine(system.FindProduct(103), 1); second.AddLine(system.FindProduct(104), 1);
        var third = system.CreateOrder(1003, 3, new DateOnly(2026, 9, 16)); third.AddLine(system.FindProduct(101), 5); third.Pay();
        return system;
    }
}
