namespace WorkoutReservations.Application.Models.Booking
{
    public class BookingDetailsDto
    {
        public string Id { get; set; } = null!;
        public string WorkoutTitle { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string TrainerFullName { get; set; } = null!;
        public string TrainerPhoneNumber { get; set; } = null!;
    }
}
