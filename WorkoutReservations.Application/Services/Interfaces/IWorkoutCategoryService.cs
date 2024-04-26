namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IWorkoutCategoryService
    {
        public Task<bool> ExistsByIdAsync(Guid id);
    }
}
