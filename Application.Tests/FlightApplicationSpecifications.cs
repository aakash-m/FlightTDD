using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("a@a.com", 2)]
        [InlineData("b@b.com", 3)]
        public void Books_flight(string passengerEmail, int numberOfSeatsToBook)
        {
            Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flight").Options);

            Flight flight = new Flight(3);
            entities.Flights.Add(flight);

            BookingService bookingService = new BookingService(entities: entities);
            bookingService.Book(new BookDataTransferObject(flightId: flight.Id, passengerEmail, numberOfSeatsToBook));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingReadModel(passengerEmail, numberOfSeatsToBook));
        }
    }


}