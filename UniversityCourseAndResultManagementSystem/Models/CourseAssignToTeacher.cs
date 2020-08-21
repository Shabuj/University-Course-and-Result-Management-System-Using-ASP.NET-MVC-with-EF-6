using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class CourseAssignToTeacher
    {
        [Key]
        public int CourseAssignId { get; set; }
        public int DepartmentId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Status { get; set; }
        [Required(ErrorMessage = "Please Give the Teacher Name")]

        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Please Give the Course Code")]
        
        public int CourseId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }

    }

}