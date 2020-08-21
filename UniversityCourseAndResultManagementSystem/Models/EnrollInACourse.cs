using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class EnrollInACourse
    {
        public int EnrollInACourseId { get; set; }
        [Required]
        [Display(Name = "Student Reg. No.")]
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Select Course")]
        public int CourseId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}