using Domain;
using FluentAssertions;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(6,3,3)]
        [InlineData(10,4,6)]
        [InlineData(12,8,4)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remaingNumberOfSeats)
        {
            var flight = new Flight(seatCapacity: seatCapacity);
            flight.Book("rishabh.beni@gmail.com", numberOfSeats);
            flight.RemainingNumberOfSeats.Should().Be(remaingNumberOfSeats);
        }

        [Fact]
        public void Avoids_overbooking()
        {
            var flight = new Flight(seatCapacity: 3);
            var error = flight.Book("rishabh.beni@gmail.com", 4);
            error.Should().BeOfType<OverBookingError>();
        }

        [Fact]
        public void Flight_booked_successfully()
        {
            var flight = new Flight(seatCapacity: 3);
            var error = flight.Book("rishabh.beni@gmail.com", 1);
            error.Should().BeNull();
        }

        [Fact]
        public void Remembers_Booking()
        {
            var flight = new Flight(seatCapacity: 150);
            flight.Book(passangerEmail: "rishabh.beni@gmail.com", numberOfSeats: 4);
            flight.BookingList.Should().ContainEquivalentOf(new Booking("rishabh.beni@gmail.com", 4));
        }

        [Theory]
        [InlineData(3,1,1,3)]
        [InlineData(4,2,2,4)]
        [InlineData(7,5,4,6)]
        public void Cancel_booking_frees_up_the_seats(int initialCapacity, int numberOfSeatsToBook,
            int numberOfSeatsToCancel, int remainingNumberOfSeats)
        {
            var flight = new Flight(initialCapacity);
            flight.Book(passangerEmail: "rishabh.beni@gmail.com", numberOfSeatsToBook);
            flight.CancelBooking(passengerEmail: "rishabh.beni@gmail.com", numberOfSeatsToCancel);
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Dont_cancel_booking_for_passengers_who_have_not_booked_yet()
        {
            var flight = new Flight(seatCapacity: 3);
            var error = flight.CancelBooking(passengerEmail: "rishabh.beni@gmail.com", numberOfSeats: 2);
            error.Should().BeOfType<BookingNotFoundError>();
        }

        [Fact]
        public void Returns_null_successfully_when_booking_cancelled()
        {
            var flight = new Flight(seatCapacity: 3);
            flight.Book(passangerEmail: "rishabh.beni@gmail.com", numberOfSeats: 1);
            var error = flight.CancelBooking(passengerEmail: "rishabh.beni@gmail.com", numberOfSeats: 1);
            error.Should().BeNull();
        }
    }
}