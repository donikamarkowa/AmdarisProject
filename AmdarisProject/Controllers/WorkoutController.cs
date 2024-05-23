using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.DTOs.Workout;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IWorkoutCategoryService _workoutCategoryService;
        private readonly ITrainerService _trainerService;
        private readonly ILocationService _locationService;
        public WorkoutController(IWorkoutService workoutService, IWorkoutCategoryService workoutCategoryService, ITrainerService trainerService, ILocationService locationService)
        {
            _workoutService = workoutService;
            _workoutCategoryService = workoutCategoryService;
            _trainerService = trainerService;
            _locationService = locationService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All([FromQuery] PaginationParameters workoutParameters)
        {
            try
            {
                var allWorkouts = await _workoutService.AllWorkoutsAsync(workoutParameters);


                return Ok(allWorkouts);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all workouts! Please try again later!" });
            }
        }

        [HttpGet("details")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Details(Guid id)
        { 
            try
            {
                bool workoutExist = await _workoutService.ExistsByIdAsync(id);

                if (!workoutExist)
                {
                    throw new NotFoundException($"Workout with id = {id} not found!");
                }

                var workout = await _workoutService.WorkoutDetailsByIdAsync(id);

                return Ok(workout);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get workout by id! Please try again later!" });
            }
        }

        [HttpGet("search")]        
        public async Task<IActionResult> Search(string criteria)
        {
            try
            {
                if (string.IsNullOrEmpty(criteria))
                {
                    throw new BadRequestException("Name cannot be empty");
                }

                var workouts = await _workoutService.SearchWorkoutByCriteriaAsync(criteria);

                return Ok(workouts);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while attempting to search for workouts! Please try again later!" });
            }
        }

      
        [HttpGet("searchByCategory")]
        public async Task<IActionResult> SearchByCategory(Guid id)
        {
            try
            {
                var categoryExist = await _workoutCategoryService.ExistsByIdAsync(id);

                if (!categoryExist)
                {
                    throw new NotFoundException("No workouts found for the specified category.");
                }

                var workoutsByCategory = await _workoutService.WorkoutsByCategoryAsync(id);
                return Ok(workoutsByCategory);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to search workouts by given category! Please try again later!" });
            }
        }

        [HttpGet("searchByTrainer")]
        public async Task<IActionResult> SearchByTrainer(Guid id)
        {
            try
            {
                var trainer = await _trainerService.ExistsByIdAsync(id);

                if (!trainer)
                {
                    throw new NotFoundException($"Trainer with {id} not found.");
                }

                var workoutsByTrainer = await _workoutService.WorkoutsByTrainerIdAsync(id);
                return Ok(workoutsByTrainer);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] WorkoutDto workoutDto)
        {
            try
            {
                await _workoutService.AddWorkoutAsync(workoutDto);
                return Ok("Workout added successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                // 403 Forbidden if the user is not authorized
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to add workout! Please try again later!" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addTrainer")]
        public async Task<IActionResult> AddTrainer(Guid workoutId, Guid trainerId)
        {
            try
            {
                var workoutExists = await _workoutService.ExistsByIdAsync(workoutId);
                var trainerExists = await _trainerService.ExistsByIdAsync(trainerId);
                if (!workoutExists || !trainerExists)
                {
                    throw new NotFoundException($"Trainer or workout not found.");
                }

                var isTrainer = await _workoutService.IsTrainerOfWorkoutAsync(trainerId, workoutId);
                if (isTrainer)
                {
                    return BadRequest("Trainer is already associated with the workout.");
                }
                await _workoutService.AddTrainerToWorkoutAsync(workoutId, trainerId);
                return Ok("Trainer successfully added to the workout.");
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to add trainer to the workout! Please try again later!" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(Guid id, WorkoutDto workoutDto)
        {
            try
            {
                var workoutExists = await _workoutService.ExistsByIdAsync(id);

                if (!workoutExists)
                {
                    return NotFound($"Workout with ID {id} not found.");
                }
                await _workoutService.EditWorkoutAsync(id, workoutDto);
                return Ok("Workout edited successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get edit workout! Please try again later!" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var workoutExists = await _workoutService.ExistsByIdAsync(id);

                if (!workoutExists)
                {
                    return NotFound($"Workout with ID {id} not found.");
                }
                await _workoutService.DeleteWorkoutAsync(id);

                return Ok("Workout deleted successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to delete workout! Please try again later!" });
            }
        }
    }
}
