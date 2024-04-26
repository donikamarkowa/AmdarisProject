using System.ComponentModel.DataAnnotations;

namespace WorkoutReservations.Domain.Entities
{
    public class Schedule
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Capacity { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; } = null!;
    }
}
