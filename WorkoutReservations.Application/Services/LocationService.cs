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
        private readonly IGenericRepository<Workout, WorkoutReservationsDbContext> _workoutRepository;
        public LocationService(IGenericRepository<Location, WorkoutReservationsDbContext> locationRepository,
            IGenericRepository<Workout, WorkoutReservationsDbContext> workoutRepository)
        {
            _locationRepository = locationRepository;
            _workoutRepository = workoutRepository;
        }

        public async Task AddLocationAsync(AddLocationDto dto)
        {
            var location = new Location
            {
                City = dto.City,
                Region = dto.Region,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                ZipCode = dto.ZipCode,
                MaxCapacity = dto.MaxCapacity
            };

            await _locationRepository.Add(location);
            await _locationRepository.SaveChangesAsync();
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

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _locationRepository.GetById(id) != null;
        }

        public async Task<bool> ExistsCityByNameAsync(string cityName)
        {
            return await _locationRepository.AnyAsync(l => l.City == cityName);
        }

        public async Task<bool> IsLocationOfWorkoutAsync(Guid locationId, Guid workoutId)
        {
            var location = await _locationRepository.GetByWithInclude(l => l.Id == locationId, l => l.Workouts);

            return location != null && location.Workouts.Any(w => w.Id == workoutId);
        }
        public async Task<IEnumerable<LocationDto>> AllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllByWithSelect(l => true,
                l => new LocationDto
                {
                    Id = l.Id,
                    City = l.City,
                    Address = l.Address
                });

            return locations;
        }
    }
}
