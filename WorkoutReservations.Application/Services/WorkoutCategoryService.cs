using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Infrastructure.Database;

namespace WorkoutReservations.Application.Services
{
    public class WorkoutCategoryService : IWorkoutCategoryService
    {
        private readonly WorkoutReservationsDbContext _workoutReservationsDbContext;
        public WorkoutCategoryService(WorkoutReservationsDbContext workoutReservationsDbContext)
        {
            _workoutReservationsDbContext = workoutReservationsDbContext;
        }
        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await this._workoutReservationsDbContext
                .WorkoutCategories
                .AnyAsync(w => w.Id == id);
        }
    }
}
