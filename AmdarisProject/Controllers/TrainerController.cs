﻿using AmdarisProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Services;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly ILocationService _locationService;
        private readonly IWorkoutService _workoutService;
        private readonly IHttpContextAccessor _contextAccessor;
        public TrainerController(ITrainerService trainerService, ILocationService locationService, IWorkoutService workoutService, IHttpContextAccessor contextAccessor)
        {
            _trainerService = trainerService;
            _locationService = locationService;
            _workoutService = workoutService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All([FromQuery] PaginationParameters trainerParameters)
        {
            try
            {
                var allTrainers = await _trainerService.AllTrainersByPaggingAsync(trainerParameters);

                return Ok(allTrainers);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all trainers! Please try again later!" });
            }
        }

        [HttpGet("allNames")]
        public async Task<IActionResult> AllTrainersNames()
        {
            try
            {
                var trainers = await _trainerService.AllTrainersAsync();

                return Ok(trainers);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all trainers! Please try again later!" });

            }
        }

        [HttpGet("details")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Details(Guid id)
        {

            try
            {
                bool trainerExist = await _trainerService.ExistsByIdAsync(id);

                if (!trainerExist)
                {
                    return NotFound(new { Message = $"Trainer with id = {id} not found!" });
                }

                bool trainerHasWorkouts = await _trainerService.HasWorkoutsAsync(id);

                if (!trainerHasWorkouts)
                {
                    return NotFound(new { Message = "Trainer doesn't have any workouts!" });
                }

                var trainer = await _trainerService.TrainerDetailsAsync(id);

                return Ok(trainer);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get trainer by id! Please try again later!" });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string criteria)
        {
            try
            {
                if (string.IsNullOrEmpty(criteria))
                {
                    return BadRequest("Name cannot be empty");
                }

                var trainersByName = await _trainerService.SearchTrainersByCriteria(criteria);

                return Ok(trainersByName);

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An unexpected error occurred while searching for trainers. Please try again later." });
            }
        }

        [HttpGet("trainersByWorkout")]
        public async Task<IActionResult> ByWorkout(Guid id)
        {
            try
            {
                var workout = await _workoutService.ExistsByIdAsync(id);
                if (!workout)
                {
                    return NotFound(new { Message = $"Workout with id = {id} not found!" });
                }

                var trainersByWorkout = await _trainerService.TrainersByWorkoutIdAsync(id);
                return Ok(trainersByWorkout);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to search workouts by given trainer! Please try again later!" });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("location")]
        public async Task<IActionResult> TrainersByLocation(Guid id)
        {
            try
            {
                var locationExists = await _locationService.ExistsByIdAsync(id);
                if (!locationExists)
                {
                    return BadRequest("Location does not exist.");
                }

                var trainers = await _trainerService.TrainersByLocationIdAsync(id);

                return Ok(trainers);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get workout's locations by id! Please try again later!" });

            }
        }

        [Authorize(Roles = "Trainer")]
        [HttpPost("addLocation")]
        public async Task<IActionResult> AddLocation(Guid locationId)
        {
            try
            {
                var locationExists = await _locationService.ExistsByIdAsync(locationId);
                if (!locationExists)
                {
                    return BadRequest("Location does not exist.");
                }

                var trainerId = Guid.Parse(_contextAccessor.HttpContext!.GetUserIdExtension());
                var hasLocation = await _trainerService.TrainerHasLocationAsync(trainerId, locationId);
                if (hasLocation)
                {
                    return BadRequest("Trainer already has this location.");
                }

                 await _trainerService.ChooseLocationForTrainerAsync(trainerId, locationId);
                return Ok("Location successfully added to the trainer.");
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to add location to trainer! Please try again later!" });

            }
        }

    }
}
