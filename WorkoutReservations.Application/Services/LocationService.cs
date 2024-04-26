using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Infrastructure.Database;

namespace WorkoutReservations.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly WorkoutReservationsDbContext _workoutReservationsDbContext;
        public LocationService(WorkoutReservationsDbContext workoutReservationsDbContext)
        {
            _workoutReservationsDbContext = workoutReservationsDbContext;
        }

        public async Task<IEnumerable<string>> AddressesByCityAsync(string city)
        {
            var addressess = await _workoutReservationsDbContext
                .Locations
                .Where(l => l.City == city)
                .Select(c => c.Address)
                .ToListAsync();

            return addressess;
        }

        public async Task<bool> AddressHasSchedulesByLocationIdAsync(Guid id)
        {
            return await _workoutReservationsDbContext.Schedules.AnyAsync(s => s.LocationId == id);
        }

        public async Task<IEnumerable<string>> CitiesByWorkoutIdAsync(Guid workoutId)
        {
            var cities = await _workoutReservationsDbContext
                .Workouts
                .Where(w => w.Id == workoutId)
                .SelectMany(w => w.Locations)
                .Select(l => l.City)
                .Distinct()
                .ToListAsync();

            return cities;
        }

        public async Task<IEnumerable<dynamic>> LocationsByWorkoutIdAsync(Guid Id)
        {
            var locations = await _workoutReservationsDbContext
                .Locations
                .Where(l => l.Workouts.Any(w => w.Id == Id))
                .Select(x=> new { x.City, x.Address})
                .ToListAsync();

            return locations;
        }
    }
}
