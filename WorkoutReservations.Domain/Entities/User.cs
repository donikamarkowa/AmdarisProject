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

        public string? Gender { get; set; } 

        public string? Bio { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public string? Picture { get; set; } 

        public virtual ICollection<Workout>? Workouts { get; set; } = new List<Workout>();
        public virtual ICollection<Location>? Locations { get; set; } = new List<Location>();
    }
}
