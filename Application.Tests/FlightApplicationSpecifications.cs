using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Books_flight()
        {
            Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flight").Options);

            Flight flight = new Flight(3);
            entities.Flights.Add(flight);

            BookingService bookingService = new BookingService(entities: entities);
            bookingService.Book(new BookDataTransferObject(flightId: flight.Id, passengerEmail: "a@b.com", numberOfSeats: 2));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingReadModel(passengerEmail: "a@b.com", numberOfSeats: 2));
        }
    }

    public class BookingService
    {
        public BookingService(Entities entities)
        {

        }

        public void Book(BookDataTransferObject BookDto)
        {

        }

        public IEnumerable<BookingReadModel> FindBookings(Guid flightId)
        {
            return new[]
            {
                new BookingReadModel(passengerEmail: "a@b.com", numberOfSeats: 2)
            };
        }

    }

    public class BookDataTransferObject
    {
        public BookDataTransferObject(Guid flightId, string passengerEmail, int numberOfSeats)
        {

        }
    }

    public class BookingReadModel
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }

        public BookingReadModel(string passengerEmail, int numberOfSeats)
        {
            PassengerEmail= passengerEmail;
            NumberOfSeats= numberOfSeats;
        }
    }


}