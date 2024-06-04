using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.WorkoutCategory;
using WorkoutReservations.Application.Services.Interfaces;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutCategoryController : ControllerBase
    {
        private readonly IWorkoutCategoryService _workoutCategoryService;
        public WorkoutCategoryController(IWorkoutCategoryService workoutCategoryService)
        {
            _workoutCategoryService = workoutCategoryService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> All()
        {
            try
            {
                var categories = await _workoutCategoryService.AllCategoriesAsync();

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get all categories! Please try again later!" });

            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] WorkoutCategoryDto dto)
        {
            try
            {
                await _workoutCategoryService.AddCategoryAsync(dto);

                return Ok("Category added successfully.");

            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to add new category! Please try again later!" });
            }
        }

        [HttpPost("edit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([FromBody] WorkoutCategoryDto dto, Guid id)
        {
            try
            {
                await _workoutCategoryService.EditCategoryAsyn(id, dto);

                return Ok("Category edited successfully.");

            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get edit category! Please try again later!" });

            }
        }
    }
}
