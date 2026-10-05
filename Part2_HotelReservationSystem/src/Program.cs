using Part2_HotelReservationSystem;

var hotel = new Hotel();
hotel.RegisterGuest(new Guest("G-100", "Mona Ali", "01000000000"));
hotel.AddRoom(new Room("101", RoomType.Single, 800m));
var reservation = hotel.CreateReservation("R-100", "G-100", "101", new DateOnly(2026, 10, 10), new DateOnly(2026, 10, 13));
reservation.Confirm();
reservation.CheckIn();
reservation.CheckOut();
Console.WriteLine($"{reservation.ReservationId}: {reservation.Guest.FullName} stayed {reservation.Nights} nights in room {reservation.Room.RoomNumber}; total cost: {reservation.TotalCost:F2}; status: {reservation.Status}.");

var verificationHotel = new Hotel();
verificationHotel.RegisterGuest(new Guest("G-200", "Omar Hassan", "01100000000"));
var verificationRoom = new Room("201", RoomType.Double, 1200m);
verificationHotel.AddRoom(verificationRoom);
var activeReservation = verificationHotel.CreateReservation("R-200", "G-200", "201", new DateOnly(2026, 11, 1), new DateOnly(2026, 11, 4));

ExpectInvalid(() => verificationHotel.CreateReservation("R-201", "G-200", "201", new DateOnly(2026, 11, 3), new DateOnly(2026, 11, 5)), "overlapping active reservation");
ExpectInvalid(activeReservation.CheckIn, "check-in before confirmation");
activeReservation.Cancel();
var replacement = verificationHotel.CreateReservation("R-202", "G-200", "201", new DateOnly(2026, 11, 3), new DateOnly(2026, 11, 5));
verificationRoom.StartMaintenance();
ExpectInvalid(() => verificationHotel.CreateReservation("R-203", "G-200", "201", new DateOnly(2026, 11, 5), new DateOnly(2026, 11, 6)), "booking under maintenance");
verificationRoom.EndMaintenance();
ExpectInvalid(() => new Guest("G-201", "", "010"), "empty guest identity");
ExpectInvalid(() => new Room("202", RoomType.Single, 0m), "non-positive nightly rate");
ExpectInvalid(() => verificationHotel.CreateReservation("R-204", "G-200", "201", new DateOnly(2026, 11, 7), new DateOnly(2026, 11, 7)), "invalid date range");
replacement.Confirm(); replacement.CheckIn(); replacement.CheckOut();
ExpectInvalid(replacement.Cancel, "cancelling a checked-out reservation");
Console.WriteLine("Validation verification passed.");

static void ExpectInvalid(Action action, string scenario)
{
    try
    {
        action();
        throw new InvalidOperationException($"Verification failed: {scenario} was incorrectly accepted.");
    }
    catch (ArgumentException) { }
    catch (InvalidOperationException exception) when (exception.Message.StartsWith("Verification failed:")) { throw; }
    catch (InvalidOperationException) { }
}
