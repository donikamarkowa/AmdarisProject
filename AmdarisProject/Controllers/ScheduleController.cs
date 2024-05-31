using AmdarisProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.Schedule;
using WorkoutReservations.Application.Services.Interfaces;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILocationService _locationService;
        private readonly IWorkoutService _workoutService;
        private readonly ITrainerService _trainerService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ScheduleController(IScheduleService scheduleService, 
            ILocationService locationService, 
            IWorkoutService workoutService,
            ITrainerService trainerService,
            IHttpContextAccessor contextAccessor)
        {
            _scheduleService = scheduleService;
            _locationService = locationService;
            _workoutService = workoutService;
            _trainerService = trainerService;
            _contextAccessor = contextAccessor;
        }


        [Authorize(Roles = "Trainer")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddScheduleDto dto, Guid locationId, Guid workoutId)
        {
            try
            {
                var locationExists = await _locationService.ExistsByIdAsync(locationId);

                if (!locationExists)
                {
                    return BadRequest("Location does not exist.");
                }

                var checkCapacity = await _scheduleService.IsCapacityValidForLocation(dto.Capacity, locationId);
                if (!checkCapacity)
                {
                    return BadRequest("The proposed capacity exceeds the maximum capacity allowed for this location.");

                }

                var trainerId = Guid.Parse(_contextAccessor.HttpContext!.GetUserIdExtension());
                var result = await _workoutService.IsTrainerOfWorkoutAsync(trainerId, workoutId);
                if (!result)
                {
                    return BadRequest("The trainer is not associated with the specified workout.");
                }

                await _scheduleService.AddScheduleToLocationAsync(dto, locationId, trainerId, workoutId);
                return Ok("Schedule added successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to add schedule! Please try again later!" });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(Guid trainerId, Guid workoutId, Guid locationId)
        {
            try
            {
                var schedules = await _scheduleService.AllSchedulesByTrainerWorkoutAndLocationAsync(trainerId, workoutId, locationId);
                return Ok(schedules);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to all schedules! Please try again later!" });
            }
        }


        [Authorize(Roles = "Trainer")]
        [HttpGet("availableSchedules")]
        public async Task<IActionResult> AvailableSchedulesByLocation(Guid locationId)
        {
            try
            {
                var locationExists = await _locationService.ExistsByIdAsync(locationId);
                if (!locationExists)
                {
                    return NotFound(new { message = "Location not found." });
                }

                var avaialbleSchedules = await _scheduleService.GetAllAvailableSchedulesByLocationAsync(locationId);

                return Ok(avaialbleSchedules);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get avaialble schedules by location! Please try again later!" });
            }
        }
    }
}
