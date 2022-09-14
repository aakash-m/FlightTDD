using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flight").Options);
        readonly BookingService bookingService;

        public FlightApplicationSpecifications()
        {
            bookingService = new BookingService(entities: entities);
        }

        [Theory]
        [InlineData("a@a.com", 2)]
        [InlineData("b@b.com", 3)]
        public void Books_flight(string passengerEmail, int numberOfSeatsToBook)
        {
            //Given
            Flight flight = new Flight(3);
            entities.Flights.Add(flight);

            //When
            bookingService.Book(new BookDataTransferObject(flightId: flight.Id, passengerEmail:passengerEmail, numberOfSeatsToBook));
            
            //Then
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingReadModel(passengerEmail, numberOfSeatsToBook));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(13)]
        public void Cancels_booking(int initialCapacity)
        {
            //Given            
            Flight flight = new Flight(initialCapacity);
            entities.Flights.Add(flight);

            bookingService.Book(new BookDataTransferObject(flightId: flight.Id, passengerEmail: "d@d.com", numberOfSeats: 2));

            //When
            bookingService.CancelBooking(new CancelBookingDto(flightId: flight.Id, passengerEmail: "d@d.com", numberOfSeats: 2));

            //Then
            bookingService.GetRemainingNumberOfSeats(flight.Id).Should().Be(initialCapacity);

        }
    }
}