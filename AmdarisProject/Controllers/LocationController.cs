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

        [Authorize(Roles = "Trainer")]
        [HttpGet("cities")]
        public async Task<IActionResult> AllCities()
        {
            try
            {
                var cities = await _locationService.GetAllCitiesAsync();
                return Ok(cities);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all cities! Please try again later!" });
            }
        }

        [Authorize(Roles = "Trainer")]
        [HttpGet("addressesByCity")]
        public async Task<IActionResult> AllAddressesByCity(string city)
        {
            try
            {
                var cityExists = await _locationService.ExistsCityByNameAsync(city);
                if (!cityExists)
                {
                    return BadRequest("City does not exist.");
                }

                var addresses = await _locationService.GetAllAddressesByCityAsync(city);
                return Ok(addresses);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all addresses! Please try again later!" });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("workout")]
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
                return BadRequest(new { Message = "Unexpected error occurred while trying to get workout's locations by id! Please try again later!" });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("addresses")]
        public async Task<IActionResult> GetAddressesByCityAndWorkout(Guid id, string city)
        {
            try
            {
                var workoutExists = await _workoutService.ExistsByIdAsync(id);
                var cityExists = await _locationService.ExistsCityByNameAsync(city);
                if (!workoutExists || !cityExists)
                {
                    return BadRequest("Workout or city does not exist.");
                }

                var addresses = await _locationService.AddressesByCityAndWorkoutAsync(id, city);
                return Ok(addresses);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get addresses by city and workout! Please try again later!" });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("citiesByWorkout")]
        public async Task<IActionResult> GetCitiesByWorkout(Guid workoutId)
        {
            try
            {
                var workoutExists = await _workoutService.ExistsByIdAsync(workoutId);
                if (!workoutExists)
                {
                    return BadRequest("Workout does not exist.");
                }

                var cities = await _locationService.CitiesByWorkoutAsync(workoutId);
                return Ok(cities);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get cities by workout! Please try again later!" });
            }
        }
    }


}
