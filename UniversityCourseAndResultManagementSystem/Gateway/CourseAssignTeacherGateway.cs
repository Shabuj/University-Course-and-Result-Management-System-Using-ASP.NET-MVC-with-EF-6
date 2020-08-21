using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class CourseAssignTeacherGateway
    {

        private string conString = WebConfigurationManager.ConnectionStrings["UniversityManagementEntities"].ConnectionString;


        public List<ViewAssignCourse> GetAllAssignCourses(int deptId)
        {
            string query = "SELECT Courses.CourseCode, Courses.CoursName,Semesters.SemesterName,Teachers.TeacherName"
                            + " FROM CourseAssignToTeachers"
                            + " Right JOIN Courses ON Courses.CourseId=CourseAssignToTeachers.CourseId"
                            + " LEFT JOIN Teachers ON Teachers.TeacherId=CourseAssignToTeachers.TeacherId"
                            + " Inner JOIN Semesters ON Courses.SemesterId=Semesters.SemesterId"
                            + " where Courses.DepartmentId='" + deptId + "' order by CourseCode";

            SqlConnection connection = new SqlConnection(conString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<ViewAssignCourse> listOfItems = new List<ViewAssignCourse>();
            while (reader.Read())
            {
                ViewAssignCourse viewAssignCourse = new ViewAssignCourse();

                viewAssignCourse.CourseCode = reader["CourseCode"].ToString();
                viewAssignCourse.CoursName = reader["CoursName"].ToString();
                viewAssignCourse.CourseSemester = reader["SemesterName"].ToString();
                viewAssignCourse.AssignTeacherName = reader["TeacherName"].ToString();
                if (viewAssignCourse.AssignTeacherName =="")
                {
                    viewAssignCourse.AssignTeacherName = "Not Assigned Yet";
                }
                listOfItems.Add(viewAssignCourse);

            }
            connection.Close();
            return listOfItems;
        }
        public bool UnAllocateClassRoom()
        {
            SqlConnection connection = new SqlConnection(conString);
            string query = "UPDATE ClassRoomAllocations SET Status = " + "1" + " ";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();
            if (rowAffect > 0)
            {
                return true;
            }
            return false;
        }
    }
}