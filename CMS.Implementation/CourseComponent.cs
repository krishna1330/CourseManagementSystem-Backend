using CMS.Business.ClientModels;
using CMS.Business.Models;
using CMS.Data;
using Microsoft.AspNetCore.Http;

namespace CMS.Implementation
{
    public class CourseComponent
    {
        public IEnumerable<GetCoursesResponse> GetCourses(string connectionString)
        {
            CoursesDAC courses = new CoursesDAC();
            return courses.GetCourses(connectionString);
        }

        public async Task<string> CreateCourseAsync(CMSDbContext _cmsDbContext, AddCoursePayload payload, IFormFile thumbnail)
        {
            CoursesDAC courses = new CoursesDAC();
            return await courses.CreateCourseAsync(_cmsDbContext, payload, thumbnail);
        }

        public async Task<IEnumerable<GetCoursesResponse>> GetMasterCoursesAsync(CMSDbContext dbContext, int masterId)
        {
            CoursesDAC courses = new CoursesDAC();
            return await courses.GetMasterCoursesAsync(dbContext, masterId);
        }
    }
}
