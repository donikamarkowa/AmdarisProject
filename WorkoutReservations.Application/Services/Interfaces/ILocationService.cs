using WorkoutReservations.Application.DTOs.Location;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ILocationService
    {
        public Task<IEnumerable<LocationDto>> LocationsByWorkoutIdAsync(Guid workoutId);
        public Task<IEnumerable<string>> CitiesByWorkoutAsync(Guid workoutId);
        public Task<IEnumerable<string>> AddressesByCityAndWorkoutAsync(Guid workoutId, string city);
        public Task<IEnumerable<string>> GetAllCitiesAsync();
        public Task<IEnumerable<string>> GetAllAddressesByCityAsync(string city);
        public Task AddLocationAsync(AddLocationDto dto);
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<bool> ExistsCityByNameAsync(string cityName);
        public Task<bool> IsLocationOfWorkoutAsync(Guid locationId, Guid workoutId);
    }
}
