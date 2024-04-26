using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WorkoutReservations.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {

        [Required]
        public string Description { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
