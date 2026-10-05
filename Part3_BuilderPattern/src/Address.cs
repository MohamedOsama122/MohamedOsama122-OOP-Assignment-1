namespace Part3_BuilderPattern;

public sealed record Address(string Street, string City, string? State, string PostalCode, string Country);

public sealed class AddressBuilder
{
    private string? _street;
    private string? _city;
    private string? _state;
    private string? _postalCode;
    private string? _country;
    public AddressBuilder AtStreet(string street) { _street = street; return this; }
    public AddressBuilder InCity(string city) { _city = city; return this; }
    public AddressBuilder InState(string? state) { _state = state; return this; }
    public AddressBuilder WithPostalCode(string postalCode) { _postalCode = postalCode; return this; }
    public AddressBuilder InCountry(string country) { _country = country; return this; }
    public Address Build() => new(Required(_street, "street"), Required(_city, "city"), string.IsNullOrWhiteSpace(_state) ? null : _state.Trim(), Required(_postalCode, "postal code"), Required(_country, "country"));
    private static string Required(string? value, string name) => string.IsNullOrWhiteSpace(value) ? throw new InvalidOperationException($"An address {name} is required.") : value.Trim();
}
