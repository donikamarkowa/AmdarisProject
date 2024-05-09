﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Services.Interfaces;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All([FromQuery] PaginationParameters trainerParameters)
        {
            try
            {
                var allTrainers = await _trainerService.AllTrainersAsync(trainerParameters);

                return Ok(allTrainers);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all trainers! Please try again later!" });
            }
        }

        [HttpGet("{id}/details")]
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

    }
}
