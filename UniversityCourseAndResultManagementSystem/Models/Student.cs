using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage ="Please Give the  Name")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name cannot be a number")]

        public string StudentName { get; set; }
        [Required(ErrorMessage = "Please Give the Email")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailExist", "Student", ErrorMessage = "Email already exist")]
  
        [StringLength(50)]
        public string StudentEmail { get; set; }
        [Required(ErrorMessage = "Please Give the Contact Number")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
     
        public string StudentContactNo { get; set; }
        [Required(ErrorMessage = "Please Select  Date")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please Give Address")]
        [DataType(DataType.MultilineText)]

        public string StudentAddress { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string StudentRegNo { get; set; }

        [Required(ErrorMessage = "Please Select A Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}