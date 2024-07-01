using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Business.ClientModels
{
    public class AddCoursePayload
    {
        public string? CourseName { get; set; }
        public int MasterId { get; set; }
        public decimal CoursePrice { get; set; }
        public int CourseAccessDurationInMonths { get; set; }
        public string? CourseLanguage { get; set; }
    }
}
