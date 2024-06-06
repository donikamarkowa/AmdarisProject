using System.Linq.Expressions;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.DTOs.Workout;
using WorkoutReservations.Application.Models.Workout;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Domain.Enums;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IGenericRepository<Workout, WorkoutReservationsDbContext> _workoutRepository;
        private readonly IGenericRepository<User, WorkoutReservationsDbContext> _trainerRepository;
        private readonly IGenericRepository<Location, WorkoutReservationsDbContext> _locationRepository;

        public WorkoutService(IGenericRepository<Workout, WorkoutReservationsDbContext> workoutRepository, IGenericRepository<User, WorkoutReservationsDbContext> trainerRepository, 
            IGenericRepository<Location, WorkoutReservationsDbContext> locationRepository)
        {
            _workoutRepository = workoutRepository;
            _trainerRepository = trainerRepository;
            _locationRepository = locationRepository;
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

            var workoutDto = new WorkoutDetailsDto()
            {
                Id = workout.Id.ToString(),
                Title = workout.Title,
                Description = workout.Description,
                EquipmentNeeded = workout.EquipmentNeeded,
                Duration = workout.Duration.ToString(),
                Gender = workout.Gender,
                IntensityLevel = workout.IntensityLevel,
                Status = workout.Status.ToString(),
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

        public async Task<IEnumerable<AllWorkoutsDto>> WorkoutsByCategoryIdAsync(Guid id)
        {
            var workouts = await _workoutRepository.GetAllBy(predicate: w => w.WorkoutCategoryId == id);

            var workoutsByCategory = workouts
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                });

            return workoutsByCategory;
        }

        public async Task<IEnumerable<AllWorkoutsDto>> WorkoutsByTrainerIdAsync(Guid id)
        {
            var allWorkouts = await _workoutRepository.GetAllBy(
                  w => w.Trainers.Any(t => t.Id == id));

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

        public async Task AddWorkoutAsync(WorkoutDto workoutDto)
        {
       

            var workout = new Workout()
            {
                Title = workoutDto.Title,
                Description = workoutDto.Description,
                EquipmentNeeded = workoutDto.EquipmentNeeded,
                Duration = TimeSpan.Parse(workoutDto.Duration),
                Gender = workoutDto.Gender,
                IntensityLevel = workoutDto.IntensityLevel,
                Status = workoutDto.Status,
                Picture = workoutDto.Picture,
                Price = decimal.Parse(workoutDto.Price),
                RecommendedFrequency = workoutDto.RecommendedFrequency,
                WorkoutCategoryId = Guid.Parse(workoutDto.WorkoutCategoryId),
            };

            
            await _workoutRepository.Add(workout);
            await _workoutRepository.SaveChangesAsync();
        }

        public async Task EditWorkoutAsync(Guid id, WorkoutDto workoutDto)
        {
            var workout = await _workoutRepository.GetById(id);

            workout.Title = workoutDto.Title;
            workout.Description = workoutDto.Description;
            workout.EquipmentNeeded = workoutDto.EquipmentNeeded;
            workout.Duration = TimeSpan.Parse(workoutDto.Duration.ToString());
            workout.Gender = workoutDto.Gender;
            workout.IntensityLevel = workoutDto.IntensityLevel;
            workout.Status = workoutDto.Status;
            workout.Picture = workoutDto.Picture;
            workout.Price = decimal.Parse(workoutDto.Price);
            workout.WorkoutCategoryId = Guid.Parse(workoutDto.WorkoutCategoryId);


            await _workoutRepository.SaveChangesAsync();
        }

        public async Task DeleteWorkoutAsync(Guid id)
        {
            var workout = await _workoutRepository.GetById(id);
            workout.Status = WorkoutStatus.Deleted;
            _workoutRepository.Delete(workout);
            await _workoutRepository.SaveChangesAsync();
        }

        public async Task AddTrainerToWorkoutAsync(Guid workoutId, Guid trainerId)
        {
            var workout = await _workoutRepository.GetById(workoutId);
            var trainer = await _trainerRepository.GetById(trainerId);

            workout.Trainers.Add(trainer);
            await _workoutRepository.SaveChangesAsync();
        }

        public async Task<bool> IsTrainerOfWorkoutAsync(Guid trainerId, Guid workoutId)
        {
            var trainer = await _trainerRepository.GetByWithInclude(t => t.Id == trainerId, t => t.Workouts!);

            return trainer != null && trainer.Workouts!.Any(w => w.Id == workoutId);
        }

        public async Task AddLocationToWorkoutAsync(Guid workoutId, Guid locationId)
        {
            var workout = await _workoutRepository.GetById(workoutId);
            var location = await _locationRepository.GetById(locationId);

            workout.Locations.Add(location);
            await _workoutRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetWorkoutsPhotos()
        {
            Expression<Func<Workout, bool>> predicate = w => true;

            var photos = await _workoutRepository.GetAllByWithSelect(predicate, w => w.Picture);

            return photos;
        }
    }
}
