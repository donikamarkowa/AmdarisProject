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
        public string Status { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string WorkoutCategory = null!;
    }
}
