namespace Part3_BuilderPattern;

// Task 3.2: one fluent builder for the complete invoice.
public sealed class InvoiceBuilder
{
    private string? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;
    private Address? _billingAddress;
    private Address? _shippingAddress;
    private OrderInfo? _order;
    public InvoiceBuilder WithInvoiceId(string value) { _invoiceId = value; return this; }
    public InvoiceBuilder ForCustomer(string name, string email, string phone) { _customerName = name; _customerEmail = email; _customerPhone = phone; return this; }
    public InvoiceBuilder BillTo(Address address) { _billingAddress = address; return this; }
    public InvoiceBuilder ShipTo(Address address) { _shippingAddress = address; return this; }
    public InvoiceBuilder WithOrder(OrderInfo order) { _order = order; return this; }
    public Invoice Build() => new(Required(_invoiceId, "invoice ID"), Required(_customerName, "customer name"), Required(_customerEmail, "customer email"), Required(_customerPhone, "customer phone"), _billingAddress ?? throw new InvalidOperationException("Billing address is required."), _shippingAddress ?? throw new InvalidOperationException("Shipping address is required."), _order ?? throw new InvalidOperationException("Order information is required."));
    private static string Required(string? value, string name) => string.IsNullOrWhiteSpace(value) ? throw new InvalidOperationException($"{name} is required.") : value.Trim();
}

// Task 3.3: a coordinator that composes dedicated builders.
public sealed class InvoiceCompositionBuilder
{
    private string? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;
    private AddressBuilder? _billing;
    private AddressBuilder? _shipping;
    private OrderBuilder? _order;
    public InvoiceCompositionBuilder WithInvoiceId(string value) { _invoiceId = value; return this; }
    public InvoiceCompositionBuilder ForCustomer(string name, string email, string phone) { _customerName = name; _customerEmail = email; _customerPhone = phone; return this; }
    public InvoiceCompositionBuilder WithBillingAddress(AddressBuilder builder) { _billing = builder; return this; }
    public InvoiceCompositionBuilder WithShippingAddress(AddressBuilder builder) { _shipping = builder; return this; }
    public InvoiceCompositionBuilder WithOrder(OrderBuilder builder) { _order = builder; return this; }
    public Invoice Build() => new InvoiceBuilder()
        .WithInvoiceId(Required(_invoiceId, "invoice ID"))
        .ForCustomer(Required(_customerName, "customer name"), Required(_customerEmail, "customer email"), Required(_customerPhone, "customer phone"))
        .BillTo((_billing ?? throw new InvalidOperationException("Billing address builder is required.")).Build())
        .ShipTo((_shipping ?? throw new InvalidOperationException("Shipping address builder is required.")).Build())
        .WithOrder((_order ?? throw new InvalidOperationException("Order builder is required.")).Build())
        .Build();
    private static string Required(string? value, string name) => string.IsNullOrWhiteSpace(value) ? throw new InvalidOperationException($"{name} is required.") : value.Trim();
}
