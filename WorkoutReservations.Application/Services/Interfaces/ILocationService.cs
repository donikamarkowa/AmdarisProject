using WorkoutReservations.Application.DTOs.Location;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ILocationService
    {
        public Task<IEnumerable<LocationDto>> LocationsByWorkoutIdAsync(Guid workoutId);
        public Task<IEnumerable<LocationDto>> AllLocationsAsync();
        public Task AddLocationAsync(AddLocationDto dto);
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<bool> IsLocationOfWorkoutAsync(Guid locationId, Guid workoutId);
    }
}
