
namespace Domain
{
    public class Flight
    {
        List<Booking> bookinglist = new();
        public IEnumerable<Booking> BookingList => bookinglist;
        public int RemainingNumberOfSeats { get; set; }

        public Guid Id { get; }

        [Obsolete("Needed by EF")]
        Flight() { }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public object? Book(string passangerEmail, int numberOfSeats)
        {
            if (numberOfSeats > this.RemainingNumberOfSeats) {
                return new OverBookingError();
            }
            RemainingNumberOfSeats -= numberOfSeats;
            bookinglist.Add(new Booking(passangerEmail, numberOfSeats));
            return null;
        }

        public object? CancelBooking(string passengerEmail, int numberOfSeats)
        {
            if (!BookingList.Any(booking => booking.Email == passengerEmail)) {
                return new BookingNotFoundError();
            }
            RemainingNumberOfSeats += numberOfSeats;
            return null;
        }
    }
}
