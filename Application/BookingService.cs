using Data;

namespace Application
{
    public class BookingService
    {
        public Entities Entities { get; set; }

        public BookingService(Entities entities)
        {
            Entities = entities;
        }

        public void Book(BookDataTransferObject BookDto)
        {
            var flight = Entities.Flights.Find(BookDto.FlightId);
            flight.Book(BookDto.PassengerEmail, BookDto.NumberOfSeats);
            Entities.SaveChanges();
        }

        public IEnumerable<BookingReadModel> FindBookings(Guid flightId)
        {
            //BookingReadModel bookingReadModel=new BookingReadModel()
            return Entities.Flights.Find(flightId).BookingList.Select(
                    booking => new BookingReadModel(booking.Email, booking.NumberOfSeats));
        }

    }
}