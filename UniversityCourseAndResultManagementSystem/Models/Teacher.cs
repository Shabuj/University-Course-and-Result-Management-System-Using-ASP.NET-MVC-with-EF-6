using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Please Give the Name")]
        [Column(TypeName = "Varchar")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name cannot be a number")]

        public string TeacherName { get; set; }
        [Required(ErrorMessage = "Please Give the  Address")]

        [Column(TypeName = "text")]
        public string TeacherAddress { get; set; }
        [Required(ErrorMessage = "Please Give the  Email")]

        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailExist", "Teacher", ErrorMessage = "Email already exist")]
        public string TeacherEmail { get; set; }
        [Required(ErrorMessage = "Please Give the Contact No")]
        [Column(TypeName = "Varchar")]
        [RegularExpression(@"^\(?[+. ]?([0-9]{2})\)?[-. ]?([0-9]{11})$", ErrorMessage = "Invalid Phone number(e.+01XXXXXXXXX)")]
        public string TeacherContactNo { get; set; }
        [Required(ErrorMessage = "Please Select  the  Designation")]



        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Please Select  the  Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please Select  the  Department")]

        public int DepartmentId { get; set; }
        [Range(+.5, Double.MaxValue, ErrorMessage = "Credit Can not be negetive")]
        [Required(ErrorMessage = "Please Give the Credit")]
      
        public double CreditToBeTaken { get; set; }
        public double TeacherRemainingCredit { get; set; }
        public virtual Department Department { get; set; }

        public virtual Designation Designation { get; set; }
    

    }
}