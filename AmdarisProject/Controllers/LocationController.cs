using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.Location;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IWorkoutService _workoutService;

        public LocationController(ILocationService locationService, IWorkoutService workoutService)
        {
            _locationService = locationService;
            _workoutService = workoutService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddLocationDto dto)
        {
            try
            {
                await _locationService.AddLocationAsync(dto);

                return Ok("Location added successfully.");

            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to add location! Please try again later!" });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("byWorkout")]
        public async Task<IActionResult> LocationsByWorkout(Guid id)
        {
            try
            {
               var workoutExists = await _workoutService.ExistsByIdAsync(id);
                if (!workoutExists)
                {
                    return BadRequest("Workout does not exist.");
                }

                var locations = await _locationService.LocationsByWorkoutIdAsync(id);

                return Ok(locations);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get workout's locations by workout id! Please try again later!" });
            }
        }


        [Authorize(Roles = "Trainer")]
        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            try
            {
                var locations = await _locationService.AllLocationsAsync();

                return Ok(locations);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all locations! Please try again later!" });
            }
        }


    }


}
