using WorkoutReservations.Domain.Entities;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ILocationService
    {
        public Task<IEnumerable<string>> CitiesByWorkoutIdAsync(Guid workoutId);
        public Task<IEnumerable<string>> AddressesByCityAsync(string city);
        public Task<bool> AddressHasSchedulesByLocationIdAsync(Guid id);
        public Task<IEnumerable<dynamic>> LocationsByWorkoutIdAsync(Guid Id);
    }
}
