﻿using WorkoutReservations.Application.DTOs.Schedule;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IGenericRepository<Schedule, WorkoutReservationsDbContext> _scheduleRepository;
        private readonly IGenericRepository<Location, WorkoutReservationsDbContext> _locationRepository;
        private readonly IGenericRepository<User, WorkoutReservationsDbContext> _trainerRepository;
        private readonly IGenericRepository<Workout, WorkoutReservationsDbContext> _workoutRepository;


        public ScheduleService(IGenericRepository<Schedule, WorkoutReservationsDbContext> scheduleRepository,
            IGenericRepository<Location, WorkoutReservationsDbContext> locationRepository,
            IGenericRepository<User, WorkoutReservationsDbContext> trainerRepository,
            IGenericRepository<Workout, WorkoutReservationsDbContext> workoutRepository)
        {
            _scheduleRepository = scheduleRepository;
            _locationRepository = locationRepository;
            _trainerRepository = trainerRepository;
            _workoutRepository = workoutRepository;
        }

        public async Task<bool> ExistsByLocationIdAsync(Guid locationId, string date)
        {
            bool scheduleExists = await _scheduleRepository.AnyAsync(
              s => s.LocationId == locationId && s.Date == DateTime.Parse(date));

            return scheduleExists;

        }
        public async Task<bool> IsCapacityValidForLocation(int capacity, Guid locationId)
        {
            var location = await _locationRepository.GetById(locationId);
            return capacity <= location.MaxCapacity;
        }

        public async Task<bool> HasCapacityAsync(Guid id)
        {
            var schedule = await _scheduleRepository.GetById(id);

            if (schedule.Capacity <= 0)
            {
                return false;
            }

            return true;
        }
        public async Task AddScheduleToWorkoutByTrainerAsync(AddScheduleDto scheduleDto, Guid locationId, Guid workoutId, Guid trainerId)
        {
            var location = await  _locationRepository.GetById(locationId);
            var workout = await _workoutRepository.GetById(workoutId);

            var schedule = new Schedule
            { 
                Date = DateTime.Parse(scheduleDto.Date),
                Capacity = scheduleDto.Capacity,
                LocationId = locationId,
                WorkoutId = workoutId,
                UserId = trainerId
            };

            await _scheduleRepository.Add(schedule);
            await _scheduleRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ScheduleDto>> AllSchedulesByTrainerWorkoutAndLocationAsync(Guid trainerId, Guid workoutId, Guid locationId)
        {
            var schedules = await _scheduleRepository.GetAllBy(s =>
                s.UserId == trainerId && s.WorkoutId == workoutId && s.LocationId == locationId && s.Date >= DateTime.Today);

            // Map the schedules to ScheduleDto objects
            var scheduleDtos = schedules.Select(s => new ScheduleDto
            {
                Id = s.Id.ToString(),
                Date = s.Date.ToString("dd-MM-yyyy HH:mm"),
            });

            return scheduleDtos;
        }
    }
}
