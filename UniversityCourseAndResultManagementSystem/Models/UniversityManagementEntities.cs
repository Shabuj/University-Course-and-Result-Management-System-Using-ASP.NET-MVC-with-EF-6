using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class UniversityManagementEntities : DbContext
    {
        public UniversityManagementEntities():base()
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
      
        public DbSet<CourseAssignToTeacher> CourseAssignToTeachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SevenDayWeek> SevenDayWeeks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ClassRoomAllocation> ClassRoomAllocations { get; set; }
        public DbSet<EnrollInACourse> EnrollInACourses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SaveStudentResult> SaveStudentResults { get; set; }
    


    }
}