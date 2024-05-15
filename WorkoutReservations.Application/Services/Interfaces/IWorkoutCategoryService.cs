using WorkoutReservations.Application.DTOs.WorkoutCategory;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IWorkoutCategoryService
    {
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task AddCategoryAsync(AddWorkoutCategoryDto dto);
    }
}
