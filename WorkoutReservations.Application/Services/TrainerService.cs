using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Models.Trainer;
using WorkoutReservations.Application.Models.Workout;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Infrastructure.Database;

namespace WorkoutReservations.Application.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly WorkoutReservationsDbContext _workoutReservationsDbContext;
        public TrainerService(WorkoutReservationsDbContext workoutReservationsDbContext)
        {
            _workoutReservationsDbContext = workoutReservationsDbContext;
        }

        public async Task<PaginatedList<AllTrainersDto>> AllTrainersAsync(TrainerParameters trainerParameters)
        {
            var allTrainers = await this._workoutReservationsDbContext
               .Users
               .Where(u => u.Role.Name == "Trainer")
               .Select(w => new AllTrainersDto
               {
                   Id = w.Id.ToString(),
                   FirstName = w.FirstName,
                   LastName = w.LastName,
                   Picture = w.Picture!
               })
               .ToListAsync();

            return PaginatedList<AllTrainersDto>.ToPagedList(allTrainers, trainerParameters.PageNumber, trainerParameters.PageSize);

        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await this._workoutReservationsDbContext
                .Users
                .AnyAsync(w => w.Id == id);
        }

        public async Task<bool> HasSchedulesAsync(Guid id)
        {
            return await _workoutReservationsDbContext.Schedules.AnyAsync(s => s.UserId == id);
        }

        public async Task<bool> HasWorkoutsAsync(Guid id)
        {
            return await _workoutReservationsDbContext.Workouts.AnyAsync(w => w.Trainers.Any(t => t.Id == id));
        }

        public async Task<IEnumerable<AllTrainersDto>> SearchTrainersByCriteria(string criteria)
        {
            var trainers = await _workoutReservationsDbContext
                .Users
                .Where(u => u.Role.Name == "Trainer" && (u.FirstName.Contains(criteria) || u.LastName.Contains(criteria)))
                .Select(u => new AllTrainersDto
                {
                    Id = u.Id.ToString(),
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Picture = u.Picture!
                })
                .ToListAsync();

            return trainers;
        }

        public async Task<TrainerDetailsDto> TrainerDetailsAsync(Guid id)
        {
            var trainer = await _workoutReservationsDbContext
                .Users
                .FirstAsync(u => u.Id == id);

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

        public async Task<IEnumerable<string>> TrainersByAddressAsync(string address)
        {
            var trainers = await _workoutReservationsDbContext
               .Workouts
               .Include(w => w.Locations)
               .Where(w => w.Locations.Any(l => l.Address == address))
               .SelectMany(w => w.Trainers.Select(t => t.FirstName + " " + t.LastName))
               .ToListAsync();

            return trainers;
        }
    }
}
