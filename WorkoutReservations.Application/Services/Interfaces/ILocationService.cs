using WorkoutReservations.Application.DTOs.Location;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ILocationService
    {
        //public Task<IEnumerable<string>> CitiesByWorkoutIdAsync(Guid workoutId);
        //public Task<IEnumerable<string>> AddressesByCityAsync(string city);
        public Task<IEnumerable<LocationDto>> LocationsByWorkoutIdAsync(Guid workoutId);
    }
}
