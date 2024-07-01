using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using CMS.Business.Models;
using CMS.Business.ClientModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class CoursesDAC
    {
        public IEnumerable<GetCoursesResponse> GetCourses(string connectionString)
        {
            List<GetCoursesResponse> coursesList = new List<GetCoursesResponse>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCourses", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                GetCoursesResponse course = new GetCoursesResponse();
                                course.CourseID = dr.GetInt32(dr.GetOrdinal("CourseID"));
                                course.CourseName = dr.GetString(dr.GetOrdinal("CourseName"));
                                course.MasterId = dr.GetInt32(dr.GetOrdinal("MasterId"));
                                //course.MasterName = dr.GetString(dr.GetOrdinal("MasterName"));
                                course.CoursePrice = dr.GetDecimal(dr.GetOrdinal("CoursePrice"));
                                course.CourseAccessDurationInMonths = dr.GetInt32(dr.GetOrdinal("CourseAccessDurationInMonths"));
                                course.CourseLanguage = dr.GetString(dr.GetOrdinal("CourseLanguage"));
                                course.CreatedDate = dr.GetDateTime(dr.GetOrdinal("CreatedDate"));
                                course.Thumbnail = GetPhotoUsingUrl(dr.IsDBNull(dr.GetOrdinal("Thumbnail")) ? null : dr.GetString(dr.GetOrdinal("Thumbnail")));
                                coursesList.Add(course);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            return coursesList;
        }

        public string GetPhotoUsingUrl(string url)
        {
            if (!File.Exists(url))
            {
                return "Invalid Url";
            }
            var photoBytes = File.ReadAllBytes(url);
            var base64Photo = Convert.ToBase64String(photoBytes);
            return base64Photo;
        }

        public async Task<string> CreateCourseAsync(CMSDbContext _cmsDbContext, AddCoursePayload payload, IFormFile thumbnail)
        {
            try
            {
                string thumbnailPath = string.Empty;

                if (thumbnail != null)
                {
                    thumbnailPath = SaveImageToBlob(thumbnail);
                }

                Courses course = new Courses
                {
                    CourseName = payload.CourseName,
                    MasterId = payload.MasterId,
                    CoursePrice = payload.CoursePrice,
                    CourseAccessDurationInMonths = payload.CourseAccessDurationInMonths,
                    CourseLanguage = payload.CourseLanguage,
                    CreatedDate = DateTime.Now,
                    Thumbnail = thumbnailPath,
                    IsActive = false,
                    IsDeleted = false
                };

                _cmsDbContext.Courses.Add(course);
                await _cmsDbContext.SaveChangesAsync().ConfigureAwait(false); ;

                return "Course created successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SaveImageToBlob(IFormFile thumbnail)
        {
            var imageName = $"{Guid.NewGuid()}_{thumbnail.FileName}";
            var filePath = Path.Combine("E:\\CMS-Assets", imageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                thumbnail.CopyToAsync(stream);
            }

            return filePath;
        }

        public async Task<IEnumerable<GetCoursesResponse>> GetMasterCoursesAsync(CMSDbContext dbContext, int masterId)
        {
            try
            {
                var coursesList = await (from course in dbContext.Courses
                                         join user in dbContext.Users
                                         on course.MasterId equals user.UserId
                                         where course.MasterId == masterId
                                         select new GetCoursesResponse
                                         {
                                             CourseID = course.CourseID,
                                             CourseName = course.CourseName,
                                             MasterId = user.UserId,
                                             MasterName = user.FirstName + " " + user.LastName,
                                             CoursePrice = course.CoursePrice,
                                             CourseAccessDurationInMonths = course.CourseAccessDurationInMonths,
                                             CourseLanguage = course.CourseLanguage,
                                             CreatedDate = course.CreatedDate,
                                             Thumbnail = course.Thumbnail
                                         }).ToListAsync();

                var coursesResponseList = coursesList.Select(course => new GetCoursesResponse
                {
                    CourseID = course.CourseID,
                    CourseName = course.CourseName,
                    MasterId = course.MasterId,
                    MasterName = course.MasterName,
                    CoursePrice = course.CoursePrice,
                    CourseAccessDurationInMonths = course.CourseAccessDurationInMonths,
                    CourseLanguage = course.CourseLanguage,
                    CreatedDate = course.CreatedDate,
                    Thumbnail = GetPhotoUsingUrl(course.Thumbnail)
                }).ToList();

                return coursesResponseList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new List<GetCoursesResponse>();
            }
        }
    }
}
