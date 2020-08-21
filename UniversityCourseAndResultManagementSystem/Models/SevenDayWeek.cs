using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class SevenDayWeek
    {
        [Key]
        public int DayId { get; set; }

        public string DayCode { get; set; }

        public string DayName { get; set; }
    }
}