using WorkoutReservations.Application.DTOs.Schedule;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<IEnumerable<ScheduleDto>> SchedulesByTrainerIdAsync(Guid id);
        public Task<IEnumerable<ScheduleDto>> SchedulesByLocationIdAsync(Guid id);

    }
}
