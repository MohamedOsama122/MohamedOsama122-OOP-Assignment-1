namespace Part1_ProceduralToOOP;

public sealed class Order
{
    private readonly List<OrderLine> _lines = [];

    public Order(int id, Customer customer, DateOnly date)
    {
        Id = id;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Date = date;
    }

    public int Id { get; }
    public Customer Customer { get; }
    public DateOnly Date { get; }
    public bool IsPaid { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines;
    public decimal Total => Customer.ApplyDiscount(_lines.Sum(line => line.Total));

    public void AddLine(Product product, int quantity)
    {
        if (IsPaid) throw new InvalidOperationException("Cannot change a paid order.");
        ArgumentNullException.ThrowIfNull(product);
        product.ReserveStock(quantity);
        _lines.Add(new OrderLine(product, quantity));
    }

    public void Pay()
    {
        if (_lines.Count == 0) throw new InvalidOperationException("Cannot pay an empty order.");
        IsPaid = true;
    }
}
