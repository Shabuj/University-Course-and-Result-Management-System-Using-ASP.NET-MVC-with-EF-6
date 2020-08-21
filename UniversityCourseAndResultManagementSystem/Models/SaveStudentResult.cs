using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class SaveStudentResult
    {
        public int SaveStudentResultId { get; set; }
        [Required(ErrorMessage = "Please Select Reg No")]

        [Display(Name = "Student Reg. No.")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please Select Course")]

        [Display(Name = "Select Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage ="Please Select Grade")]

        public int GradeId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual Grade Grade { get; set; }
    }
}