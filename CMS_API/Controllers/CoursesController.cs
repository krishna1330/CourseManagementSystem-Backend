using CMS.Business.Models;
using CMS.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly string _connectionString;
        public CoursesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection") ?? throw new InvalidOperationException("Connection string cannot be null or whitespace");
        }
        [HttpGet("GetCourses")]
        public IActionResult GetCourses()
        {
            try
            {
                CourseComponent courseComponent = new CourseComponent();
                IEnumerable<Courses> coursesList = new List<Courses>();
                coursesList = courseComponent.GetCourses(_connectionString);
                return Ok(coursesList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
