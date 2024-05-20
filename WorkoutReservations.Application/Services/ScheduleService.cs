using WorkoutReservations.Application.DTOs.Schedule;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IGenericRepository<Schedule, WorkoutReservationsDbContext> _scheduleRepository;
        private readonly IGenericRepository<Location, WorkoutReservationsDbContext> _locationRepository;
        private readonly IGenericRepository<User, WorkoutReservationsDbContext> _trainerRepository;


        public ScheduleService(IGenericRepository<Schedule, WorkoutReservationsDbContext> scheduleRepository,
            IGenericRepository<Location, WorkoutReservationsDbContext> locationRepository,
            IGenericRepository<User, WorkoutReservationsDbContext> trainerRepository)
        {
            _scheduleRepository = scheduleRepository;
            _locationRepository = locationRepository;
            _trainerRepository = trainerRepository;
        }
    

        public async Task AddScheduleToLocationAsync(AddScheduleDto scheduleDto, Guid locationId, Guid trainerId)
        {

            var location = await _locationRepository.GetById(locationId);
            var trainer = await _trainerRepository.GetById(trainerId);

            var newSchedule = new Schedule
            {
                Id = Guid.Parse(scheduleDto.Id),
                LocationId = locationId,
                UserId = trainerId,
                Capacity = scheduleDto.Capacity,
                Date = DateTime.Parse(scheduleDto.Date)
            };

            await _scheduleRepository.Add(newSchedule);
            await _scheduleRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByLocationIdAsync(Guid locationId, string date)
        {
            bool scheduleExists = await _scheduleRepository.AnyAsync(
              s => s.LocationId == locationId && s.Date == DateTime.Parse(date));

            return scheduleExists;

        }

        public async Task<IEnumerable<ScheduleDto>> SchedulesByLocationIdAsync(Guid id)
        {
            var schedulesByLocation = await _scheduleRepository.GetAllBy(s => s.LocationId == id);

            var schedules = schedulesByLocation
                .Select(s => new ScheduleDto
                {
                    Id = s.Id.ToString(),
                    Date = s.Date.ToString("dd-MM-yyyy HH:mm")
                });

            return schedules;
        }

        public async Task<IEnumerable<ScheduleDto>> SchedulesByTrainerIdAsync(Guid id)
        {
            var schedulesByTrainer = await _scheduleRepository.GetAllBy(s => s.UserId == id);

            var schedules = schedulesByTrainer
                .Select(s => new ScheduleDto
                {
                    Id = s.Id.ToString(),
                    Date = s.Date.ToString("dd-MM-yyyy HH:mm")
                });

            return schedules;
        }

        public async Task<bool> IsCapacityValidForLocation(int capacity, Guid locationId)
        {
            var location = await _locationRepository.GetById(locationId);
            return capacity <= location.MaxCapacity;
        }

        public async Task<bool> HasCapacityAsync(Guid id)
        {
            var schedule = await _scheduleRepository.GetById(id);

            if (schedule.Capacity <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
