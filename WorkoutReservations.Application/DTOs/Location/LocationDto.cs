namespace WorkoutReservations.Application.DTOs.Location
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
