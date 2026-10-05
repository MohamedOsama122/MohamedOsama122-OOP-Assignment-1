namespace Part2_HotelReservationSystem;

public sealed class Hotel
{
    private readonly Dictionary<string, Guest> _guests = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Room> _rooms = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Reservation> _reservations = new(StringComparer.OrdinalIgnoreCase);
    public void RegisterGuest(Guest guest)
    {
        ArgumentNullException.ThrowIfNull(guest);
        if (!_guests.TryAdd(guest.GuestId, guest)) throw new InvalidOperationException($"Guest ID {guest.GuestId} already exists.");
    }
    public void AddRoom(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);
        if (!_rooms.TryAdd(room.RoomNumber, room)) throw new InvalidOperationException($"Room {room.RoomNumber} already exists.");
    }
    public Reservation CreateReservation(string reservationId, string guestId, string roomNumber, DateOnly checkInDate, DateOnly checkOutDate)
    {
        if (_reservations.ContainsKey(reservationId)) throw new InvalidOperationException($"Reservation ID {reservationId} already exists.");
        if (!_guests.TryGetValue(guestId, out var guest)) throw new KeyNotFoundException($"Guest ID {guestId} was not found.");
        if (!_rooms.TryGetValue(roomNumber, out var room)) throw new KeyNotFoundException($"Room {roomNumber} was not found.");
        var reservation = guest.CreateReservation(reservationId, room, checkInDate, checkOutDate);
        _reservations.Add(reservation.ReservationId, reservation);
        return reservation;
    }
}
