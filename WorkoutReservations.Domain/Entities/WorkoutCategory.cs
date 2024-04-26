using System.ComponentModel.DataAnnotations;

namespace WorkoutReservations.Domain.Entities
{
    public class WorkoutCategory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
