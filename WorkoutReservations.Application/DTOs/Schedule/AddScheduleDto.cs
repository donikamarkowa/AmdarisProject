namespace WorkoutReservations.Application.DTOs.Schedule
{
    public class AddScheduleDto
    {
        public string Id { get; set; } = null!;
        public string Date { get; set; } = null!;
        public int Capacity { get; set; } 
    }
}
