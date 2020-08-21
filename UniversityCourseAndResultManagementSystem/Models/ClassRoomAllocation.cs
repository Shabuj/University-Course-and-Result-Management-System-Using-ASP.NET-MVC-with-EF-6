using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class ClassRoomAllocation
    {
        [Key]
        public int ClassRoomAllocationId { get; set; }
        [Required]
        [Display(Name = "Time From")]
        [Column(TypeName="varchar")]
        [StringLength(50)]
        public string TimeFrom { get; set; }
        [Required]
        [Display(Name = "Time To")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string TimeTo { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [Display(Name = "Department ")]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Course ")]
        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Room No. ")]
        public int RoomId { get; set; }
        [Required]
        [Display(Name = "Day ")]
        public int DayId { get; set; }

        public virtual SevenDayWeek SevenDayWeek { get; set; }
        public virtual  Room Room { get; set; }
        public virtual  Department Department { get; set; }
        public virtual  Course Course { get; set; }
    }
}