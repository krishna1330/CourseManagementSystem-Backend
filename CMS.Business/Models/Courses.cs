﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Business.Models
{
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public int MasterId { get; set; }
        //public string? MasterName { get; set; }
        public decimal CoursePrice { get; set; }
        public int CourseAccessDurationInMonths { get; set; }
        public string? CourseLanguage { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Thumbnail { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
