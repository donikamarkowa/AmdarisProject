using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Models.Workout;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IGenericRepository<Workout, WorkoutReservationsDbContext> _workoutRepository;

        public WorkoutService(IGenericRepository<Workout, WorkoutReservationsDbContext> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _workoutRepository.GetById(id) != null;
        }

        public async Task<PaginatedList<AllWorkoutsDto>> AllWorkoutsAsync(PaginationParameters workoutPatrameters)
        {
            var allWorkouts = await _workoutRepository.GetAll();
            var mappedWorkouts = allWorkouts
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                })
                .ToList();
         
            return PaginatedList<AllWorkoutsDto>.ToPagedList(mappedWorkouts, workoutPatrameters.PageNumber, workoutPatrameters.PageSize);

        }

        public async Task<WorkoutDetailsDto> WorkoutDetailsByIdAsync(Guid id)
        {
            var workout = await _workoutRepository.GetByWithInclude(predicate: w => w.Id == id, includeProperties: w => w.WorkoutCategory);

            WorkoutDetailsDto workoutDto = new WorkoutDetailsDto()
            {
                Id = workout.Id.ToString(),
                Title = workout.Title,
                Description = workout.Description,
                EquipmentNeeded = workout.EquipmentNeeded,
                Duration = workout.Duration.ToString(),
                Gender = workout.Gender,
                IntensityLevel = workout.IntensityLevel,
                Status = workout.Status,
                Picture = workout.Picture,
                Price = workout.Price.ToString(),
                WorkoutCategory = workout.WorkoutCategory.Name
            };

            return workoutDto;
        }
        public async Task<IEnumerable<AllWorkoutsDto>> SearchWorkoutByCriteriaAsync(string criteria)
        {
            var allWorkouts = await _workoutRepository.GetAllBy(predicate: w => w.Title.Contains(criteria));   

            var workouts = allWorkouts
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                })
                .ToList();

            return workouts;
        }

        public async Task<IEnumerable<AllWorkoutsDto>> WorkoutsByCategoryAsync(Guid categoryId)
        {
            var workouts = await _workoutRepository.GetAllBy(predicate: w => w.WorkoutCategoryId == categoryId);

            var workoutsByCategory = workouts
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                });

            return workoutsByCategory;
        }

        public async Task<IEnumerable<AllWorkoutsDto>> WorkoutsByTrainerIdAsync(Guid trainerId)
        {
            var allWorkouts = await _workoutRepository.GetAllBy(predicate: w => w.Trainers.Any(t => t.Id == trainerId));

            var workoutsByTrainer = allWorkouts
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                })
                .ToList();

            return workoutsByTrainer;
        }


    }
}
