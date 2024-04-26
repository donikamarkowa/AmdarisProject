using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Models.Workout;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;

namespace WorkoutReservations.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly WorkoutReservationsDbContext _workoutReservationsDbContext;
        public WorkoutService(WorkoutReservationsDbContext workoutReservationsDbContext)
        {
            _workoutReservationsDbContext = workoutReservationsDbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await this._workoutReservationsDbContext
                .Workouts
                .AnyAsync(w => w.Id == id);
        }

        public async Task<PaginatedList<AllWorkoutsDto>> AllWorkoutsAsync(WorkoutPatrameters workoutPatrameters)
        {
            var allWorkouts = await this._workoutReservationsDbContext
                .Workouts
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                })
                .ToListAsync();

            return PaginatedList<AllWorkoutsDto>.ToPagedList(allWorkouts, workoutPatrameters.PageNumber, workoutPatrameters.PageSize);
        }

        public async Task<WorkoutDetailsDto> WorkoutDetailsByIdAsync(Guid id)
        {
            var workout = await this._workoutReservationsDbContext
                .Workouts
                .Include(w => w.WorkoutCategory)
                .Include(workout => workout.Locations)
                .FirstAsync(w => w.Id == id);

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
                WorkoutCategory = workout.WorkoutCategory.ToString()!
            };

            return workoutDto;
        }
        public async Task<IEnumerable<AllWorkoutsDto>> SearchWorkoutByCriteriaAsync(string criteria)
        {
            IQueryable<Workout> query = _workoutReservationsDbContext.Workouts;

            if (!string.IsNullOrEmpty(criteria))
            {
                query = query.Where(w => w.Title.Contains(criteria));

            }

            var workouts = await query
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                })
                .ToListAsync();

            return workouts;
        }

        public async Task<IEnumerable<AllWorkoutsDto>> WorkoutsByCategory(Guid categoryId)
        {
            var workouts = await _workoutReservationsDbContext
                .Workouts
                .Where(w => w.WorkoutCategory.Id == categoryId)
                .ToListAsync();

            var workoutsByCategory = workouts
                .Select(workout => new AllWorkoutsDto
                {
                    Id = workout.Id.ToString(),
                    Title = workout.Title,
                    Picture = workout.Picture
                });

            return workoutsByCategory;
        }

        public async Task<IEnumerable<AllWorkoutsDto>> WorkoutsByTrainerIdAsync(Guid id)
        {
            var workouts = await _workoutReservationsDbContext
                .Workouts
                .Where(w => w.Trainers.Any(t => t.Id == id))
                .Select(w => new AllWorkoutsDto
                {
                    Id = w.Id.ToString(),
                    Title = w.Title,
                    Picture = w.Picture
                })
                .ToListAsync();

            return workouts;
        }


    }
}
