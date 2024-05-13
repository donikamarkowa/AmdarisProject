using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                .GetPropertyValuesWithIncludeAsync(
                selector: l => l.Id.ToString(),
                predicate: l => l.Workouts.Any(w => w.Id == workoutId),
                includeProperties: l => new { l.Address, l.City }
            );

            return (IEnumerable<LocationDto>)locations;
        }
    }
}
