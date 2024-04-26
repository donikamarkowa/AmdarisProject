using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WorkoutReservations.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        public int? Age { get; set; }

        [Required]
        public string Gender { get; set; } = null!;

        public string? Bio { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public string? Picture { get; set; } 
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Workout>? Workouts { get; set; } = new List<Workout>();
    }
}
