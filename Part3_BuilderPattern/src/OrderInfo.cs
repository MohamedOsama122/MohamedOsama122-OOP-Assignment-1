namespace Part3_BuilderPattern;

public sealed record OrderInfo(DateOnly OrderDate, string PaymentMethod, string Currency, decimal SubTotal, decimal DiscountAmount, decimal TaxAmount)
{
    public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;
}

public sealed class OrderBuilder
{
    private DateOnly? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount;
    private decimal _taxAmount;
    public OrderBuilder On(DateOnly orderDate) { _orderDate = orderDate; return this; }
    public OrderBuilder PaidBy(string paymentMethod) { _paymentMethod = paymentMethod; return this; }
    public OrderBuilder InCurrency(string currency) { _currency = currency; return this; }
    public OrderBuilder WithSubTotal(decimal subTotal) { _subTotal = subTotal; return this; }
    public OrderBuilder WithDiscount(decimal discountAmount) { _discountAmount = discountAmount; return this; }
    public OrderBuilder WithTax(decimal taxAmount) { _taxAmount = taxAmount; return this; }
    public OrderInfo Build()
    {
        if (_orderDate is null) throw new InvalidOperationException("Order date is required.");
        if (string.IsNullOrWhiteSpace(_paymentMethod)) throw new InvalidOperationException("Payment method is required.");
        if (string.IsNullOrWhiteSpace(_currency)) throw new InvalidOperationException("Currency is required.");
        if (_subTotal is null || _subTotal < 0) throw new InvalidOperationException("A non-negative subtotal is required.");
        if (_discountAmount < 0 || _discountAmount > _subTotal) throw new InvalidOperationException("Discount must be between zero and the subtotal.");
        if (_taxAmount < 0) throw new InvalidOperationException("Tax amount cannot be negative.");
        return new OrderInfo(_orderDate.Value, _paymentMethod.Trim(), _currency.Trim(), _subTotal.Value, _discountAmount, _taxAmount);
    }
}
