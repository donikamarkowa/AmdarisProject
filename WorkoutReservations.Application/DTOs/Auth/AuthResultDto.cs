namespace WorkoutReservations.Application.DTOs.Auth
{
    public class AuthResultDto
    {
        public AuthResultDto(string token)
        {
            this.Token = token;
        }
        public string Token { get; set; } = null!;
    }
}
