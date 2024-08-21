using Microsoft.AspNetCore.Mvc;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.BusinessLogicLayer.Services.CourseManagement;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly CourseService _courseService;

        public CourseController(UserService userService, CourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
        {
            var course = await _courseService.GetCourses();
            if (course == null)
            {
                return BadRequest("Course not found.");
            }
            return Ok(course);
        }

        [HttpPost("course")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = await _courseService.CreateCourseAsync(createCourseDto);
                return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }
    }
}
