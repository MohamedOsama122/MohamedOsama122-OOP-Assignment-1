namespace Part1_ProceduralToOOP;

public sealed class Customer
{
    public Customer(int id, string name, string email, string city, bool isVip)
    {
        Id = id;
        Name = RequireText(name, nameof(name));
        Email = RequireText(email, nameof(email));
        City = RequireText(city, nameof(city));
        IsVip = isVip;
    }

    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string City { get; }
    public bool IsVip { get; }

    public decimal ApplyDiscount(decimal amount) => IsVip ? amount * 0.90m : amount;

    private static string RequireText(string value, string parameterName) =>
        string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("A value is required.", parameterName) : value.Trim();
}
