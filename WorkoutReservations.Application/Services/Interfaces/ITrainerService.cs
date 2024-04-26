using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Models.Trainer;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ITrainerService
    {
        public Task<IEnumerable<string>> TrainersByAddressAsync(string address);
        public Task<TrainerDetailsDto> TrainerDetailsAsync(Guid id);
        public Task<PaginatedList<AllTrainersDto>> AllTrainersAsync(TrainerParameters trainerParameters);
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<bool> HasWorkoutsAsync(Guid id);
        public Task<bool> HasSchedulesAsync(Guid id);
        public Task<IEnumerable<AllTrainersDto>> SearchTrainersByCriteria(string criteria);

    }
}
