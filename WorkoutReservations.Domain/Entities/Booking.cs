using System.ComponentModel.DataAnnotations;
using WorkoutReservations.Domain.Enums;

namespace WorkoutReservations.Domain.Entities
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public BookingStatus Status { get; set; } 
        public Guid WorkoutId { get; set; }
        public virtual Workout Workout { get; set; } = null!;
        public Guid ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; } = null!;
    }
}
