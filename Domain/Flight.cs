using System.Runtime.Intrinsics.X86;

namespace Domain
{
    public class Flight
    {
        List<Booking> bookingList = new List<Booking>();
        public IEnumerable<Booking> BookingList => bookingList;

        public int RemainingNumberOfSeats { get; set; }

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public Object? Book(string passengerEmail, int numberOfSeats)
        {
            if (numberOfSeats > this.RemainingNumberOfSeats)
            {
                return new OverBookingError();
            }

            RemainingNumberOfSeats -= numberOfSeats;
            bookingList.Add(new Booking(passengerEmail,numberOfSeats));

            return null;
        }

        public object? Cancel_Booking(string passengerEmail, int numberOfSeats)
        {
            if (!bookingList.Any(booking => booking.Email == passengerEmail))
            {
                return new BookingNotFoundError();
            }


            RemainingNumberOfSeats += numberOfSeats;
            // bookingList.Remove(new Booking(passengerEmail, numberOfSeats));
            return null;
        }
    }
}