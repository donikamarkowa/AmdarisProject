﻿using WorkoutReservations.Application.DTOs.WorkoutCategory;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class WorkoutCategoryService : IWorkoutCategoryService
    {
        private readonly IGenericRepository<WorkoutCategory, WorkoutReservationsDbContext> _workoutCategoryRepository;
        public WorkoutCategoryService(IGenericRepository<WorkoutCategory, WorkoutReservationsDbContext> workoutCategoryRepository)
        {
            _workoutCategoryRepository = workoutCategoryRepository;
        }

        public async Task AddCategoryAsync(AddWorkoutCategoryDto dto)
        {
            var category = new WorkoutCategory
            {
                Id = Guid.Parse(dto.Id),
                Name = dto.Name
            };

            await _workoutCategoryRepository.Add(category);
            await _workoutCategoryRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _workoutCategoryRepository.GetById(id) != null;
        }
    }
}
