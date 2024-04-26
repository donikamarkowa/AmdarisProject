namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<IEnumerable<string>> SchedulesByTrainerIdAsync(Guid id);
        public Task<IEnumerable<string>> SchedulesByAddressAsync(Guid locationId);

    }
}
