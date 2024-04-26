using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<string>> SchedulesByTrainerIdAsync(Guid id)
        {
            var schedules = await _workoutReservationsDbContext
                .Schedules
                .Where(s => s.UserId == id)
                .Select(s => s.Date.ToString())
                .ToListAsync();

            return schedules;
        }

        public async Task<IEnumerable<string>> SchedulesByAddressAsync(Guid locationId)
        {
            var schedules = await _workoutReservationsDbContext
                .Schedules
                .Where(s => s.LocationId == locationId)
                .Select(s => s.Date.ToString())
                .ToListAsync();

            return schedules;
        }

    }
}
