using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutReservations.Domain.Entities
{
    public class Rating
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int RatingCount { get; set; }
        public string? Comment { get; set; } 
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public Guid WorkoutId { get; set; }
        public virtual Workout Workout { get; set; } = null!;
    }
}
