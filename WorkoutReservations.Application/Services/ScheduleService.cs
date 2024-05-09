using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.DTOs.Schedule;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly GenericRepository<Schedule, WorkoutReservationsDbContext> _scheduleRepository;
        public ScheduleService(GenericRepository<Schedule, WorkoutReservationsDbContext> scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }
        public async Task<IEnumerable<ScheduleDto>> SchedulesByTrainerIdAsync(Guid id)
        {
            var schedulesByTrainer = await _scheduleRepository.GetAllBy(predicate: s => s.UserId == id);

            var schedulesDto = schedulesByTrainer
                .Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    Date = s.Date.ToString("dd-MM-yyyy HH:mm")
                });

            return schedulesDto;
        }

        public async Task<IEnumerable<ScheduleDto>> SchedulesByLocationIdAsync(Guid id)
        {
            var schedulesByLocation = await _scheduleRepository.GetAllBy(s => s.LocationId == id);

            var schedules = schedulesByLocation
                .Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    Date = s.Date.ToString("dd-MM-yyyy HH:mm")
                });

            return schedules;
        }
    }
}
