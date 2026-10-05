namespace Part3_BuilderPattern;

public sealed class Invoice
{
    internal Invoice(string invoiceId, string customerName, string customerEmail, string customerPhone, Address billingAddress, Address shippingAddress, OrderInfo order)
    {
        InvoiceId = invoiceId; CustomerName = customerName; CustomerEmail = customerEmail; CustomerPhone = customerPhone;
        BillingAddress = billingAddress; ShippingAddress = shippingAddress; Order = order;
    }
    public string InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string CustomerPhone { get; }
    public Address BillingAddress { get; }
    public Address ShippingAddress { get; }
    public OrderInfo Order { get; }
    public DateOnly OrderDate => Order.OrderDate;
    public string PaymentMethod => Order.PaymentMethod;
    public string Currency => Order.Currency;
    public decimal SubTotal => Order.SubTotal;
    public decimal DiscountAmount => Order.DiscountAmount;
    public decimal TaxAmount => Order.TaxAmount;
    public decimal TotalAmount => Order.TotalAmount;
}
