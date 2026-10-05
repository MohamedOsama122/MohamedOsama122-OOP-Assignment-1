namespace Part1_ProceduralToOOP;

public sealed class OrderLine
{
    public OrderLine(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; }
    public decimal Total => Product.Price * Quantity;
}
