using WorkoutReservations.Application.DTOs.Location;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly IGenericRepository<Location, WorkoutReservationsDbContext> _locationRepository;
        public LocationService(IGenericRepository<Location, WorkoutReservationsDbContext> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<LocationDto>> LocationsByWorkoutIdAsync(Guid workoutId)
        {
            var locations = await _locationRepository
               .GetAllBy(l => l.Workouts.Any(w => w.Id == workoutId));

            var locationDtos = locations.Select(l => new LocationDto
            {
                Id = l.Id,
                City = l.City,
                Address = l.Address
            });

            return locationDtos.ToList();
        }

    }
}
