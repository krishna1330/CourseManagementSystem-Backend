using CMS.Business.Models;
using CMS.Data;

namespace CMS.Implementation
{
    public class CourseComponent
    {
        public IEnumerable<Courses> GetCourses(string connectionString)
        {
            CoursesDAC courses = new CoursesDAC();
            return courses.GetCourses(connectionString);
        }
    }
}
