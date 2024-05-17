using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.WorkoutCategory;
using WorkoutReservations.Application.Services.Interfaces;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class WorkoutCategoryController : ControllerBase
    {
        private readonly IWorkoutCategoryService _workoutCategoryService;
        public WorkoutCategoryController(IWorkoutCategoryService workoutCategoryService)
        {
            _workoutCategoryService = workoutCategoryService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddWorkoutCategoryDto dto)
        {
            try
            {
                await _workoutCategoryService.AddCategoryAsync(dto);

                return Ok("Category added successfully.");

            }
            catch (UnauthorizedAccessException ex)
            {
                // 403 Forbidden if the user is not authorized
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Failed to add workout.");
            }
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] EditWorkoutCategoryDto dto, Guid id)
        {
            try
            {
                await _workoutCategoryService.EditCategoryAsyn(id, dto);

                return Ok("Category edited successfully.");

            }
            catch (UnauthorizedAccessException ex)
            {
                // 403 Forbidden if the user is not authorized
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Failed to add workout.");
            }
        }
    }
}
