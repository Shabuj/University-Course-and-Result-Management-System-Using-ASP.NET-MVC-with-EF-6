using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please give the Course Code")]
        [Remote("IsCourseCodeExist", "Course", ErrorMessage = "Course Code already exist")]
        [Column(TypeName = "Varchar")]

        [StringLength(15, MinimumLength = 5, ErrorMessage = "Code must be at least five (5) characters long.")]
        public string CourseCode { get; set; }
        [Required(ErrorMessage = "Please give the Course name")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name cannot be a number")]
        [Remote("IsCourseNameExist", "Course", ErrorMessage = "Name  already exist")]
        public string CoursName { get; set; }
        [Required(ErrorMessage = "Please give the Course Credit")]
        [Range(0.5, 5, ErrorMessage = "Course Credit must be between 0.5 and 5.0")]
        public double CourseCredit { get; set; }
        [Required(ErrorMessage = "Please give the Course Description")]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "Varchar")]
        [StringLength(150)]
        public string CourseDescription { get; set; }
        [Required(ErrorMessage = "Please Select Department Name")]

        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please Select Semester Name")]

        public int SemesterId { get; set; }
  
        public virtual Department Department { get; set; }

        public virtual Semester Semester { get; set; }
    
    }
}