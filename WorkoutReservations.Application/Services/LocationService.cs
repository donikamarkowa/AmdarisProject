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
                Id = Guid.Parse(dto.Id),
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

        public async Task<IEnumerable<string>> AddressesByCityAndWorkoutAsync(Guid workoutId, string city)
        {
            var addresses = await _locationRepository
                    .GetAllByWithSelect(
                    predicate: l => l.City == city && l.Workouts.Any(w => w.Id == workoutId),
                    select: l => l.Address);

            return addresses;
        }

        public async Task<IEnumerable<string>> CitiesByWorkoutAsync(Guid workoutId)
        {
            var cities = await _locationRepository
                 .GetAllByWithSelect(
                 predicate: l => l.Workouts.Any(w => w.Id == workoutId),
                 select: l => l.City);

            return cities;
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

        public async Task AddWorkoutToLocationAsync(Guid locationId, Guid workoutId)
        {
            var workout = await _workoutRepository.GetById(workoutId);
            var location = await _locationRepository.GetById(locationId);

            workout.Locations.Add(location);
            await _locationRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _locationRepository.GetById(id) != null;
        }

        public async Task<bool> ExistsCityByNameAsync(string cityName)
        {
            return await _locationRepository.AnyAsync(l => l.City == cityName);
        }

        public async Task<bool> LocationHasWorkoutsAsync(Guid workoutId)
        {
            var result = await _locationRepository.AnyAsync(l => l.Workouts.Any(w => w.Id == workoutId));

            return result;
        }
    }
}
