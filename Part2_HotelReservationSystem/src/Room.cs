namespace Part2_HotelReservationSystem;

public sealed class Room
{
    private readonly List<Reservation> _reservations = [];
    public Room(string roomNumber, RoomType roomType, decimal nightlyRate)
    {
        RoomNumber = RequireText(roomNumber, nameof(roomNumber));
        RoomType = roomType;
        SetNightlyRate(nightlyRate);
    }
    public string RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }
    public void SetNightlyRate(decimal nightlyRate)
    {
        if (nightlyRate <= 0) throw new ArgumentOutOfRangeException(nameof(nightlyRate), "Nightly rate must be positive.");
        NightlyRate = nightlyRate;
    }
    public void StartMaintenance() => IsUnderMaintenance = true;
    public void EndMaintenance() => IsUnderMaintenance = false;
    internal void RegisterReservation(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        if (IsUnderMaintenance) throw new InvalidOperationException($"Room {RoomNumber} is under maintenance and cannot be booked.");
        var overlapsActiveBooking = _reservations.Any(existing => existing.IsActive && reservation.CheckInDate < existing.CheckOutDate && existing.CheckInDate < reservation.CheckOutDate);
        if (overlapsActiveBooking) throw new InvalidOperationException($"Room {RoomNumber} already has an overlapping active reservation.");
        _reservations.Add(reservation);
    }
    private static string RequireText(string value, string parameterName) => string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("A non-empty value is required.", parameterName) : value.Trim();
}
