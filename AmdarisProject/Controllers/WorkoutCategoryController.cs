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
        public async Task<IActionResult> Add([FromBody] WorkoutCategoryDto dto)
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
                return BadRequest(new { Message = "Unexpected error occurred while trying to add new category! Please try again later!" });
            }
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] WorkoutCategoryDto dto, Guid id)
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
                return BadRequest(new { Message = "Unexpected error occurred while trying to get edit category! Please try again later!" });

            }
        }
    }
}
