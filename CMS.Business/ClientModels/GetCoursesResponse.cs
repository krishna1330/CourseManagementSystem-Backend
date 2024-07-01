using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Business.ClientModels
{
    public class GetCoursesResponse
    {
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public int MasterId { get; set; }
        public string? MasterName { get; set; }
        public decimal CoursePrice { get; set; }
        public int CourseAccessDurationInMonths { get; set; }
        public string? CourseLanguage { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Thumbnail { get; set; }
    }
}
