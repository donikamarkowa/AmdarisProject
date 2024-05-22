using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Models.Trainer;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IGenericRepository<User, WorkoutReservationsDbContext> _trainerRepository;
        private readonly IGenericRepository<Location, WorkoutReservationsDbContext> _locationRepository;
        public TrainerService(IGenericRepository<User, WorkoutReservationsDbContext> trainerRepository,
            IGenericRepository<Location, WorkoutReservationsDbContext> locationRepository)
        {
            _trainerRepository = trainerRepository;
            _locationRepository = locationRepository;
        }

        public async Task<PaginatedList<AllTrainersDto>> AllTrainersAsync(PaginationParameters trainerParameters)
        {
            var allTrainers = await _trainerRepository.GetAllBy(predicate: u => u.Role.Name == "Trainer");
            var mappedTrainers = allTrainers
                .Select(w => new AllTrainersDto
                {
                    Id = w.Id.ToString(),
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    Picture = w.Picture!
                })
                .ToList();

            return PaginatedList<AllTrainersDto>.ToPagedList(mappedTrainers, trainerParameters.PageNumber, trainerParameters.PageSize);

        }

        public async Task ChooseLocationForTrainerAsync(Guid trainerId, Guid locationId)
        {
            var trainer = await _trainerRepository.GetById(trainerId);
            var location = await _locationRepository.GetById(locationId);

            trainer.Locations!.Add(location);
            _trainerRepository.Edit(trainer);
            await _trainerRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _trainerRepository.GetById(id) != null;
        }
        public async Task<bool> HasWorkoutsAsync(Guid id)
        {
            return await _trainerRepository.AnyAsync(t => t.Id == id && t.Workouts!.Any());
        }

        public async Task<IEnumerable<AllTrainersDto>> SearchTrainersByCriteria(string criteria)
        {
            var trainers = await _trainerRepository.GetAllBy(
                predicate: u => u.Role.Name == "Trainer" &&
                (u.FirstName.Contains(criteria) || u.LastName.Contains(criteria)));

            var trainersByCriteria = trainers
                .Select(t => new AllTrainersDto
                {
                    Id = t.Id.ToString(),
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Picture = t.Picture!
                });

            return trainersByCriteria;
        }

        public async Task<TrainerDetailsDto> TrainerDetailsAsync(Guid id)
        {
            var trainer = await _trainerRepository.GetById(id);

            TrainerDetailsDto viewModel = new TrainerDetailsDto()
            {
                Id = trainer.Id.ToString(),
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Age = (int) trainer.Age!,
                Bio = trainer.Bio!,
                Weight = (double) trainer.Weight!,
                Height = (double) trainer.Height!,
                Picture = trainer.Picture!,
                PhoneNumber = trainer.PhoneNumber!
            };

            return viewModel;
        }

        public async Task<bool> TrainerHasLocationAsync(Guid trainerId, Guid locationId)
        {
            return await _trainerRepository.AnyAsync(t => t.Id == trainerId && t.Locations!.Any(l => l.Id == locationId));
        }

        public async Task<IEnumerable<string>> TrainersByLocationIdAsync(Guid id)
        {
            return await _trainerRepository
                .GetAllByWithSelect(t => t.Locations!.Any(l => l.Id == id), t => $"{t.FirstName} {t.LastName}");
        }
    }
}
