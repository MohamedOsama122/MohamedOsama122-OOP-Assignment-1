namespace Part2_HotelReservationSystem;

public sealed class Reservation
{
    internal Reservation(string reservationId, Guest guest, Room room, DateOnly checkInDate, DateOnly checkOutDate)
    {
        if (checkOutDate <= checkInDate) throw new ArgumentException("Check-out date must be strictly after check-in date.", nameof(checkOutDate));
        ReservationId = string.IsNullOrWhiteSpace(reservationId) ? throw new ArgumentException("A reservation ID is required.", nameof(reservationId)) : reservationId.Trim();
        Guest = guest ?? throw new ArgumentNullException(nameof(guest));
        Room = room ?? throw new ArgumentNullException(nameof(room));
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Status = ReservationStatus.Pending;
    }
    public string ReservationId { get; }
    public Guest Guest { get; }
    public Room Room { get; }
    public DateOnly CheckInDate { get; }
    public DateOnly CheckOutDate { get; }
    public ReservationStatus Status { get; private set; }
    public int Nights => CheckOutDate.DayNumber - CheckInDate.DayNumber;
    public decimal TotalCost => Nights * Room.NightlyRate;
    internal bool IsActive => Status is not ReservationStatus.Cancelled and not ReservationStatus.CheckedOut;
    public void Confirm() => ChangeStatus(ReservationStatus.Confirmed);
    public void CheckIn() => ChangeStatus(ReservationStatus.CheckedIn);
    public void CheckOut() => ChangeStatus(ReservationStatus.CheckedOut);
    public void Cancel() => ChangeStatus(ReservationStatus.Cancelled);
    private void ChangeStatus(ReservationStatus target)
    {
        var legal = (Status, target) switch
        {
            (ReservationStatus.Pending, ReservationStatus.Confirmed) => true,
            (ReservationStatus.Confirmed, ReservationStatus.CheckedIn) => true,
            (ReservationStatus.CheckedIn, ReservationStatus.CheckedOut) => true,
            (ReservationStatus.Pending, ReservationStatus.Cancelled) => true,
            (ReservationStatus.Confirmed, ReservationStatus.Cancelled) => true,
            _ => false
        };
        if (!legal) throw new InvalidOperationException($"Cannot change reservation {ReservationId} from {Status} to {target}.");
        Status = target;
    }
}
