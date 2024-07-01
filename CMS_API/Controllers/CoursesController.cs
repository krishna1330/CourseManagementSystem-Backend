using CMS.Business.ClientModels;
using CMS.Business.Models;
using CMS.Data;
using CMS.Implementation;
using Microsoft.AspNetCore.Authorization;
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
        private readonly CMSDbContext _cmsDbContext;

        public CoursesController(IConfiguration configuration, CMSDbContext cmsDbContext)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection") ?? throw new InvalidOperationException("Connection string cannot be null or whitespace");
            this._cmsDbContext = cmsDbContext;
        }

        [HttpGet("GetCourses")]
        public async Task<ActionResult> GetCourses()
        {
            try
            {
                CourseComponent courseComponent = new CourseComponent();
                IEnumerable<GetCoursesResponse> coursesList = new List<GetCoursesResponse>();
                coursesList = courseComponent.GetCourses(_connectionString);
                return Ok(coursesList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("CreateCourse")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCourseAsync([FromForm] AddCoursePayload payload, IFormFile thumbnail)
        {
            try
            {
                CourseComponent courseComponent = new CourseComponent();
                string res = await courseComponent.CreateCourseAsync(_cmsDbContext, payload, thumbnail);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetMasterCourses")]
        public async Task<ActionResult> GetMasterCoursesAsync(int masterId)
        {
            try
            {
                CourseComponent courseComponent = new CourseComponent();
                IEnumerable<GetCoursesResponse> coursesList = new List<GetCoursesResponse>();
                coursesList = await courseComponent.GetMasterCoursesAsync(_cmsDbContext, masterId);
                return Ok(coursesList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
