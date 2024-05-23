using WorkoutReservations.Application.DTOs.Schedule;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task AddScheduleToLocationAsync(AddScheduleDto scheduleDto, Guid locationId, Guid trainerId, Guid workoutId);
        public Task<IEnumerable<ScheduleDto>> AllSchedulesByTrainerWorkoutAndLocationAsync(Guid trainerId, Guid workoutId, Guid locationId);
        public Task<IEnumerable<ScheduleDto>> GetAllAvailableSchedulesByLocationAsync(Guid locationId);
        public Task<bool> ExistsByLocationIdAsync(Guid locationId, string date);
        public Task<bool> IsCapacityValidForLocation(int capacity, Guid locationId);
        public Task<bool> HasCapacityAsync(Guid id);
    }
}
