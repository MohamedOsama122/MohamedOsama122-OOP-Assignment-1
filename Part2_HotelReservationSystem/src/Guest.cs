namespace Part2_HotelReservationSystem;

public sealed class Guest
{
    private readonly List<Reservation> _reservations = [];
    public Guest(string guestId, string fullName, string phoneNumber)
    {
        GuestId = RequireText(guestId, nameof(guestId));
        FullName = RequireText(fullName, nameof(fullName));
        PhoneNumber = RequireText(phoneNumber, nameof(phoneNumber));
    }
    public string GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();
    internal Reservation CreateReservation(string reservationId, Room room, DateOnly checkInDate, DateOnly checkOutDate)
    {
        ArgumentNullException.ThrowIfNull(room);
        var reservation = new Reservation(reservationId, this, room, checkInDate, checkOutDate);
        room.RegisterReservation(reservation);
        _reservations.Add(reservation);
        return reservation;
    }
    private static string RequireText(string value, string parameterName) => string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("A non-empty value is required.", parameterName) : value.Trim();
}
