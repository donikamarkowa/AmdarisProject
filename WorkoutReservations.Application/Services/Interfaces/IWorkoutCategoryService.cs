using WorkoutReservations.Application.DTOs.WorkoutCategory;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IWorkoutCategoryService
    {
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task AddCategoryAsync(WorkoutCategoryDto dto);
        public Task EditCategoryAsyn(Guid id, WorkoutCategoryDto dto);
    }
}
