using Microsoft.AspNetCore.Identity;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.DTOs.Trainer;
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
        private readonly IGenericRepository<Workout, WorkoutReservationsDbContext> _workoutRepository;

        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public TrainerService(IGenericRepository<User, WorkoutReservationsDbContext> trainerRepository,
            IGenericRepository<Location, WorkoutReservationsDbContext> locationRepository,
            IGenericRepository<Workout, WorkoutReservationsDbContext> workoutRepository,
            RoleManager<Role> roleManager, 
            UserManager<User> userManager)
        {
            _trainerRepository = trainerRepository;
            _locationRepository = locationRepository;
            _workoutRepository = workoutRepository;
            _roleManager = roleManager;
            _userManager = userManager;

        }

        public async Task<IEnumerable<TrainerDto>> AllTrainersAsync()
        {
            var trainerRole = await _roleManager.FindByNameAsync("Trainer");
            var allTrainers = await _userManager.GetUsersInRoleAsync(trainerRole!.Name!);

            var trainersDto = allTrainers.Select(t => new TrainerDto
            {
                Id = t.Id.ToString(),
                FirstName = t.FirstName,
                LastName = t.LastName
            });

            return trainersDto;
        }

        public async Task<PaginatedList<AllTrainersDto>> AllTrainersByPaggingAsync(PaginationParameters trainerParameters)
        {
            var trainerRole = await _roleManager.FindByNameAsync("Trainer");
            var allTrainers = await _userManager.GetUsersInRoleAsync(trainerRole!.Name!);

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
            var trainers = await _userManager.GetUsersInRoleAsync("Trainer");

            var filteredTrainers = trainers
                .Where(u => u.FirstName.Contains(criteria, StringComparison.OrdinalIgnoreCase) ||
                            u.LastName.Contains(criteria, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var trainersByCriteria = filteredTrainers
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

        public async Task<IEnumerable<TrainerDto>> TrainersByLocationIdAsync(Guid id)
        {
            var trainers = await _trainerRepository.GetAllByWithSelect(
             user => user.Locations!.Any(l => l.Id == id),
             user => new TrainerDto
             {
                 Id = user.Id.ToString(),
                 FirstName = user.FirstName,
                 LastName = user.LastName
             });

            return trainers;
        }

        public async Task<IEnumerable<TrainerDto>> TrainersByWorkoutIdAsync(Guid id)
        {
            var workout = await _workoutRepository.GetByWithInclude(w => w.Id == id, w => w.Trainers);

            var trainers = workout.Trainers.Select(t => new TrainerDto
            {
                Id = t.Id.ToString(),
                FirstName = t.FirstName,
                LastName = t.LastName
            });

            return trainers;
        }
    }
}
