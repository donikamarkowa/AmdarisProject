using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.DTOs.Schedule;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Infrastructure.Database;

namespace WorkoutReservations.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly WorkoutReservationsDbContext _workoutReservationsDbContext;
        public ScheduleService(WorkoutReservationsDbContext workoutReservationsDbContext)
        {
            _workoutReservationsDbContext = workoutReservationsDbContext;
        }
        public async Task<IEnumerable<ScheduleDto>> SchedulesByTrainerIdAsync(Guid id)
        {
            var schedules = await _workoutReservationsDbContext
                .Schedules
                .Where(s => s.UserId == id)
                .Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    Date = s.Date.ToString("dd-MM-yyyy HH:mm")
                })
                .ToListAsync();

            return schedules;
        }

        public async Task<IEnumerable<ScheduleDto>> SchedulesByLocationIdAsync(Guid id)
        {
            var schedules = await _workoutReservationsDbContext
                .Schedules
                .Where(s => s.LocationId == id)
                .Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    Date = s.Date.ToString("dd-MM-yyyy HH:mm")
                })
                .ToListAsync();


            return schedules;
        }
    }
}
