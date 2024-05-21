namespace WorkoutReservations.Application.DTOs.Location
{
    public class AddLocationDto
    {
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public int MaxCapacity { get; set; }
    }
}
