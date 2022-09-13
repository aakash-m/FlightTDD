using Domain;
using FluentAssertions;

namespace FlightTDD
{
    public class FlightSpecificationsTests
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(6, 3, 3)]
        [InlineData(10, 5, 5)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeatBooked, int remainingNumberOfSeats)
        {
            Flight flight = new Flight(seatCapacity: seatCapacity);
            flight.Book("testUser@gmail.com", numberOfSeatBooked);
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        //[Fact]
        //public void Booking_reduces_the_number_of_seats_2()
        //{
        //    Flight flight = new Flight(seatCapacity: 6);
        //    flight.Book("testUser@gmail.com", 3);
        //    flight.RemainingNumberOfSeats.Should().Be(3);
        //}

        //[Fact]
        //public void Booking_reduces_the_number_of_seats_3()
        //{
        //    Flight flight = new Flight(seatCapacity: 10);
        //    flight.Book("testUser@gmail.com", 3);
        //    flight.RemainingNumberOfSeats.Should().Be(7);
        //}

        [Fact]
        public void Avoids_OverBooking()
        {
            //Given
            Flight flight = new Flight(seatCapacity: 3);

            //When
            var error = flight.Book("testUser@gmail.com", 4);

            //Then
            error.Should().BeOfType<OverBookingError>();
            
        }

        [Fact]
        public void Book_Flights_Successfully()
        {
            Flight flight=new Flight(seatCapacity: 3);
            var error = flight.Book("testUser@gmail.com", 1);
            error.Should().BeNull();
        }

        [Fact]
        public void Remembers_Bookings()
        {
            Flight flight = new Flight(seatCapacity: 150);

            flight.Book("abcd@gmail.com", 5);

            flight.BookingList.Should().ContainEquivalentOf(new Booking("abcd@gmail.com", 5));

        }

        [Theory]
        [InlineData(5, 2, 2, 5)]
        [InlineData(8, 4, 4, 8)]
        [InlineData(4, 2, 1, 3)]
        public void Canceling_Booking_frees_up_the_seats(int initialCapacity, int numberOfSeatsToBook, int numberOfSeatsToCancel, 
            int remainingNumberOfSeats)
        {
            //Given
            Flight flight = new Flight(seatCapacity: initialCapacity);
            flight.Book(passengerEmail: "user1@gmail.com", numberOfSeats: numberOfSeatsToBook);
            
            //When
            flight.Cancel_Booking(passengerEmail: "user1@gmail.com", numberOfSeats: numberOfSeatsToCancel);

            //Then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);

        }

        [Fact]
        public void Doesnt_Cancel_Bookings_for_passengers_who_are_not_Booked()
        {
            //Given
            Flight flight = new Flight(seatCapacity: 3);

            //When
            var error = flight.Cancel_Booking(passengerEmail: "user2@gmail.com", numberOfSeats: 1);

            //Then
            error.Should().BeOfType<BookingNotFoundError>();
        }

        [Fact]
        public void Returns_Null_When_successfully_cancelled_a_booking()
        {
            Flight flight = new Flight(seatCapacity: 3);
            flight.Book("user3@gmail.com", 2);
            var error = flight.Cancel_Booking("user3@gmail.com", 2);
            error.Should().BeNull();
        }


    }
}