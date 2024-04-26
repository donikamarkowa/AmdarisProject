using System.ComponentModel.DataAnnotations;

namespace WorkoutReservations.Domain.Entities
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        //public string Status { get; set; } = null!;
        public Guid WorkoutId { get; set; }
        public virtual Workout Workout { get; set; } = null!;
        public Guid ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; } = null!;
    }
}
