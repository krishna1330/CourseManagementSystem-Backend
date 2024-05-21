using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using CMS.Business.Models;

namespace CMS.Data
{
    public class CoursesDAC
    {
        public IEnumerable<Courses> GetCourses(string connectionString)
        {
            List<Courses> coursesList = new List<Courses>();
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
                                Courses course = new Courses();
                                course.CourseID = dr.GetInt32(dr.GetOrdinal("CourseID"));
                                course.CourseName = dr.GetString(dr.GetOrdinal("CourseName"));
                                course.MasterId = dr.GetInt32(dr.GetOrdinal("MasterId"));
                                course.MasterNmae = dr.GetString(dr.GetOrdinal("MasterName"));
                                course.CoursePrice = dr.GetDecimal(dr.GetOrdinal("CoursePrice"));
                                course.CourseDuration = dr.GetInt32(dr.GetOrdinal("CourseDuration"));
                                course.CourseLanguage = dr.GetString(dr.GetOrdinal("CourseLanguage"));
                                course.CourseCreatedDate = dr.GetDateTime(dr.GetOrdinal("CourseCreatedDate"));
                                course.Thumbnail = GetPhotoUsingUrl(dr.IsDBNull(dr.GetOrdinal("Thumbnail")) ? null : dr.GetString(dr.GetOrdinal("Thumbnail")));
                                coursesList.Add(course);
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
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
    }
}
