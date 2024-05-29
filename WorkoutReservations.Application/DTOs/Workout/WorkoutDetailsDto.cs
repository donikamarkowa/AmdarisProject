using WorkoutReservations.Domain.Enums;

namespace WorkoutReservations.Application.Models.Workout
{
    public class WorkoutDetailsDto
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string EquipmentNeeded { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int IntensityLevel { get; set; }
        public WorkoutStatus Status { get; set; } 
        public string Picture { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string WorkoutCategory { get; set; } = null!;
    }
}
