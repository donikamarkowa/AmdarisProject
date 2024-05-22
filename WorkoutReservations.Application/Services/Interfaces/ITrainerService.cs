﻿using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Models.Trainer;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface ITrainerService
    {
        public Task<TrainerDetailsDto> TrainerDetailsAsync(Guid id);
        public Task<PaginatedList<AllTrainersDto>> AllTrainersAsync(PaginationParameters trainerParameters);
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<bool> HasWorkoutsAsync(Guid id);
        public Task<bool> TrainerHasLocationAsync(Guid trainerId, Guid locationId);
        public Task<IEnumerable<AllTrainersDto>> SearchTrainersByCriteria(string criteria);
        public Task<IEnumerable<string>> TrainersByLocationIdAsync(Guid id);
        public Task ChooseLocationForTrainerAsync(Guid trainerId, Guid locationId);

    }
}
