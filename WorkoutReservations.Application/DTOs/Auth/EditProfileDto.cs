namespace WorkoutReservations.Application.DTOs.Auth
{
    public class EditProfileDto
    {
        public int? Age { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; } 
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string? PhoneNumber { get; set; } 
        public string? Picture { get; set; }
    }
}
