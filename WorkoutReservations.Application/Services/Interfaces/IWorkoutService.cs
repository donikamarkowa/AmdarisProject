using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Models.Workout;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IWorkoutService
    {
        public Task<PaginatedList<AllWorkoutsDto>> AllWorkoutsAsync(WorkoutPatrameters workoutPatrameters);
        public Task<WorkoutDetailsDto> WorkoutDetailsByIdAsync(Guid id);
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<IEnumerable<AllWorkoutsDto>> SearchWorkoutByCriteriaAsync(string criteria);
        public Task<IEnumerable<AllWorkoutsDto>> WorkoutsByTrainerIdAsync(Guid id);
        public Task<IEnumerable<AllWorkoutsDto>> WorkoutsByCategory(Guid categoryId);
    }
}
