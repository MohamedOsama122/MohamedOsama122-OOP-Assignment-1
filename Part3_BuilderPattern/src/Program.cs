using Part3_BuilderPattern;

var invoice = new InvoiceCompositionBuilder()
    .WithInvoiceId("INV-2026-001")
    .ForCustomer("Mona Ali", "mona@example.com", "01000000000")
    .WithBillingAddress(new AddressBuilder().AtStreet("10 Nile Street").InCity("Cairo").InState("Cairo").WithPostalCode("11511").InCountry("Egypt"))
    .WithShippingAddress(new AddressBuilder().AtStreet("20 Corniche Road").InCity("Alexandria").InState("Alexandria").WithPostalCode("21500").InCountry("Egypt"))
    .WithOrder(new OrderBuilder().On(new DateOnly(2026, 10, 5)).PaidBy("Card").InCurrency("EGP").WithSubTotal(1500m).WithDiscount(100m).WithTax(196m))
    .Build();
Console.WriteLine($"{invoice.InvoiceId} for {invoice.CustomerName}: {invoice.TotalAmount:F2} {invoice.Currency}");
