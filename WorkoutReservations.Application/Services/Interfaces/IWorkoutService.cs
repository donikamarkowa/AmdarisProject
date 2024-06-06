using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.DTOs.Workout;
using WorkoutReservations.Application.Models.Workout;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IWorkoutService
    {
        public Task<PaginatedList<AllWorkoutsDto>> AllWorkoutsAsync(PaginationParameters workoutPatrameters);
        public Task<WorkoutDetailsDto> WorkoutDetailsByIdAsync(Guid id);
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<bool> IsTrainerOfWorkoutAsync(Guid trainerId, Guid workoutId);
        public Task<IEnumerable<AllWorkoutsDto>> SearchWorkoutByCriteriaAsync(string criteria);
        public Task<IEnumerable<AllWorkoutsDto>> WorkoutsByTrainerIdAsync(Guid id);
        public Task<IEnumerable<AllWorkoutsDto>> WorkoutsByCategoryIdAsync(Guid id);
        public Task AddTrainerToWorkoutAsync(Guid workoutId, Guid trainerId);
        public Task AddLocationToWorkoutAsync(Guid workoutId, Guid locationId);
        public Task AddWorkoutAsync(WorkoutDto workoutDto);
        public Task EditWorkoutAsync(Guid id, WorkoutDto workoutDto);
        public Task DeleteWorkoutAsync(Guid id);
        public Task<IEnumerable<string>> GetWorkoutsPhotos();

    }
}
