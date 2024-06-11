using WorkoutReservations.Application.DTOs.Auth;

namespace WorkoutReservations.Application.DTOs.User
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public RoleDto Role { get; set; } = null!;
    }
}
