using AmdarisProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.Schedule;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILocationService _locationService;
        private readonly ITrainerService _trainerService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ScheduleController(IScheduleService scheduleService, ILocationService locationService, ITrainerService trainerService, IHttpContextAccessor contextAccessor)
        {
            _scheduleService = scheduleService;
            _locationService = locationService;
            _trainerService = trainerService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("location")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ByLocation(Guid locationId)
        {
            try
            {
                var locationExists = await _locationService.ExistsByIdAsync(locationId);
                if (!locationExists)
                {
                    return BadRequest("Location does not exist.");
                }
                var schedules = await _scheduleService.SchedulesByLocationIdAsync(locationId);

                return Ok(schedules);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get trainer's schedules by location id! Please try again later!" });

            }
        }

        [HttpGet("trainer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ByTrainer(Guid trainerId)
        {
            try
            {
                var trainerExists = await _trainerService.ExistsByIdAsync(trainerId);    
                if (!trainerExists)
                {
                    return BadRequest("Trainer does not exist.");
                }
                var schedules = await _scheduleService.SchedulesByTrainerIdAsync(trainerId);

                return Ok(schedules);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get trainer's schedules by location id! Please try again later!" });

            }
        }

        [Authorize(Roles = "Trainer")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddScheduleDto dto, Guid locationId)
        {

            try
            {
                var scheduleExists = await _scheduleService.ExistsByLocationIdAsync(locationId, dto.Date);

                if (scheduleExists)
                {
                    return BadRequest("A schedule already exists for this location on the specified date.");
                }

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

                await _scheduleService.AddScheduleToLocationAsync(dto, locationId, trainerId);
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
    }
}
