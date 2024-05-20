using WorkoutReservations.Application.DTOs.Schedule;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task AddScheduleToLocationAsync(AddScheduleDto scheduleDto, Guid locationId, Guid trainerId);
        public Task<IEnumerable<ScheduleDto>> SchedulesByLocationIdAsync(Guid id);
        public Task<IEnumerable<ScheduleDto>> SchedulesByTrainerIdAsync(Guid id);
        public Task<bool> ExistsByLocationId(Guid locationId, string date);
        public Task<bool> IsCapacityValidForLocation(int capacity, Guid locationId);
    }
}
