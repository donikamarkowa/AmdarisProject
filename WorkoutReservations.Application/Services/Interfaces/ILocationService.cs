using WorkoutReservations.Application.DTOs.Location;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ILocationService
    {
        public Task<IEnumerable<LocationDto>> LocationsByWorkoutIdAsync(Guid workoutId);
        public Task<IEnumerable<string>> CitiesByWorkoutAsync(Guid workoutId);
        public Task<IEnumerable<string>> AddressesByCityAndWorkoutAsync(Guid workoutId, string city);
        public Task AddLocationAsync(AddLocationDto dto);
        public Task AddWorkoutToLocationAsync(Guid locationId, Guid workoutId);
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<bool> ExistsCityByNameAsync(string cityName);
        public Task<bool> LocationHasWorkoutsAsync(Guid id);
    }
}
