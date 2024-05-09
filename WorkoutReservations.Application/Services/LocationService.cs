using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.DTOs.Location;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly GenericRepository<Location, WorkoutReservationsDbContext> _locationRepository;
        public LocationService(GenericRepository<Location, WorkoutReservationsDbContext> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<string>> AddressesByCityAsync(string city)
        {
            var addresses = await _locationRepository.GetPropertyValuesAsync(l => l.Address, l => l.City == city);

            return addresses.ToList();
        }

        public async Task<bool> AddressHasSchedulesByLocationIdAsync(Guid id)
        {
            return await _workoutReservationsDbContext.Schedules.AnyAsync(s => s.LocationId == id);
        }

        public async Task<IEnumerable<string>> CitiesByWorkoutIdAsync(Guid workoutId)
        {
            var cities = await _locationRepository
                .GetAllBy(l => l.Workouts.Any(w => w.Id == workoutId)
                .Select(l => l.City)
                .Distinct()
                .ToListAsync();

            return cities;
        }

        public async Task<IEnumerable<LocationDto>> LocationsByWorkoutIdAsync(Guid Id)
        {
            var locations = await _workoutReservationsDbContext
                .Locations
                .Where(l => l.Workouts.Any(w => w.Id == Id))
                .Select(l => new LocationDto
                {
                    Id = l.Id,
                    City = l.City,
                    Address = l.Address
                })
                .ToListAsync();

            return locations;
        }
    }
}
