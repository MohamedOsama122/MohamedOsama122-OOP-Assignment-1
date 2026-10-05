namespace Part1_ProceduralToOOP;

public sealed class Product
{
    public Product(int id, string name, decimal price, int stock)
    {
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        if (stock < 0) throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");
        Id = id;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("A product name is required.", nameof(name)) : name.Trim();
        Price = price;
        Stock = stock;
    }

    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public void ReserveStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        if (Stock < quantity) throw new InvalidOperationException($"Not enough stock for product #{Id}.");
        Stock -= quantity;
    }
}
