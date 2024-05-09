using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class WorkoutCategoryService : IWorkoutCategoryService
    {
        private readonly IGenericRepository<WorkoutCategory, WorkoutReservationsDbContext> _workoutCategoryRepository;
        public WorkoutCategoryService(IGenericRepository<WorkoutCategory, WorkoutReservationsDbContext> workoutCategoryRepository)
        {
            _workoutCategoryRepository = workoutCategoryRepository;
        }
        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _workoutCategoryRepository.GetById(id) != null;
        }
    }
}
