using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.DTOs.Trainer;
using WorkoutReservations.Application.Models.Trainer;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ITrainerService
    {
        public Task<TrainerDetailsDto> TrainerDetailsAsync(Guid id);
        public Task<PaginatedList<AllTrainersDto>> AllTrainersByPaggingAsync(PaginationParameters trainerParameters);
        public Task<IEnumerable<TrainerDto>> AllTrainersAsync();
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<bool> HasWorkoutsAsync(Guid id);
        public Task<bool> TrainerHasLocationAsync(Guid trainerId, Guid locationId);
        public Task<IEnumerable<AllTrainersDto>> SearchTrainersByCriteria(string criteria);
        public Task<IEnumerable<TrainerDto>> TrainersByWorkoutIdAsync(Guid id);
        public Task<IEnumerable<TrainerDto>> TrainersByLocationIdAsync(Guid id);
        public Task ChooseLocationForTrainerAsync(Guid trainerId, Guid locationId);

    }
}
