using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }
        [Column(TypeName = "Varchar")]

        [StringLength(50)]
        public string DesignationName { get; set; }
    }
}