using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkoutReservations.Application.DTOs.Parameters;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly ILocationService _locationService;
        private readonly IScheduleService _scheduleService;
        private readonly IWorkoutCategoryService _workoutCategoryService;
        public WorkoutController(IWorkoutService workoutService, ILocationService locationService, IScheduleService scheduleService, IWorkoutCategoryService workoutCategoryService)
        {
            _workoutService = workoutService;
            _locationService = locationService;
            _scheduleService = scheduleService;
            _workoutCategoryService = workoutCategoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All([FromQuery] WorkoutPatrameters workoutParameters)
        {
            try
            {
                var allWorkouts = await _workoutService.AllWorkoutsAsync(workoutParameters);

                var metadata = new
                {
                    allWorkouts.TotalCount,
                    allWorkouts.PageSize,
                    allWorkouts.HasNext,
                    allWorkouts.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(allWorkouts);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all workouts! Please try again later!" });
            }
        }

        [HttpGet("{id}/details")]
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

                var workoutsByCategory = await _workoutService.WorkoutsByCategory(id);
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
    }
}
