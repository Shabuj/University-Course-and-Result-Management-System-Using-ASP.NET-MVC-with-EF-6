using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class ViewClassSchedule
    {

        private string conString = WebConfigurationManager.ConnectionStrings["UniversityManagementEntities"].ConnectionString;


        public List<ClassScheduleView> GetAllClassScheduleViewsByDeptId(int deptId)
        {
            SqlConnection connection = new SqlConnection(conString);

            string query =
                "SELECT Courses.CourseCode, Courses.CoursName,SevenDayWeeks.DayCode,ClassRoomAllocations.TimeFrom," +
                " ClassRoomAllocations.TimeTo,ClassRoomAllocations.Status,Rooms.RoomNo" +
                " FROM  ClassRoomAllocations" +
                " Left join SevenDayWeeks ON SevenDayWeeks.DayId=ClassRoomAllocations.DayId" +
                " Inner join Rooms ON Rooms.RoomId=ClassRoomAllocations.RoomId" +
                " Right JOIN Courses ON Courses.CourseId=ClassRoomAllocations.CourseId" +
                " where Courses.DepartmentId='" + deptId + "' order by CourseCode";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<ClassScheduleView> listOfItems = new List<ClassScheduleView>();
            while (reader.Read())
            {
                if (reader["Status"].ToString() == "1")
                {
                    continue;
                }
                ClassScheduleView item = new ClassScheduleView();
                item.CourseCode = reader["CourseCode"].ToString();
                item.CourseName = reader["CoursName"].ToString();
                item.DayShortName = reader["DayCode"].ToString();
                item.TimeFrom = reader["TimeFrom"].ToString();
                if (item.TimeFrom != "" && Convert.ToInt32(item.TimeFrom) >= 1200)
                {
                    int temp1 = Convert.ToInt32(item.TimeFrom) - 1200;
                    string timeFrom = "";
                    if (temp1.ToString().Length == 1)
                    {
                        timeFrom = "12:00" + " PM";
                    }
                    else if (temp1.ToString().Length == 2)
                    {
                        timeFrom = "12:" + temp1 + " PM";
                    }
                    else if (temp1.ToString().Length == 3)
                    {
                        string temp = temp1.ToString();
                        char[] array = temp.ToCharArray();
                        timeFrom = "0" + array[0] + ":" + array[1] + array[2] + " PM";
                    }
                    else
                    {
                        string temp = temp1.ToString();
                        char[] array = temp.ToCharArray();
                        timeFrom = "" + array[0] + array[1] + ":" + array[2] + array[3] + " PM";
                    }
                    item.TimeFrom = timeFrom;
                }
                else if (item.TimeFrom != "" && Convert.ToInt32(item.TimeFrom) < 1200)
                {
                    string temp = item.TimeFrom.ToString();
                    char[] array = temp.ToCharArray();
                    item.TimeFrom = "" + array[0] + array[1] + ":" + array[2] + array[3] + " AM";
                }
                item.TimeTo = reader["TimeTo"].ToString();
                if (item.TimeTo != "" && Convert.ToInt32(item.TimeTo) >= 1200)
                {
                    int temp1 = Convert.ToInt32(item.TimeTo) - 1200;
                    string timeTo = "";
                    if (temp1.ToString().Length == 1)
                    {
                        timeTo = "12:00" + " PM";
                    }
                    else if (temp1.ToString().Length == 2)
                    {
                        timeTo = "12:" + temp1 + " PM";
                    }
                    else if (temp1.ToString().Length == 3)
                    {
                        string temp = temp1.ToString();
                        char[] array = temp.ToCharArray();
                        timeTo = "0" + array[0] + ":" + array[1] + array[2] + " PM";
                    }
                    else
                    {
                        string temp = temp1.ToString();
                        char[] array = temp.ToCharArray();
                        timeTo = "" + array[0] + array[1] + ":" + array[2] + array[3] + " PM";
                    }
                    item.TimeTo = timeTo;
                }
                else if (item.TimeTo != "" && Convert.ToInt32(item.TimeTo) < 1200)
                {
                    string temp = item.TimeTo.ToString();
                    char[] array = temp.ToCharArray();
                    item.TimeTo = "" + array[0] + array[1] + ":" + array[2] + array[3] + " AM";
                }
                item.RoomName = reader["RoomNo"].ToString();
                listOfItems.Add(item);

            }
            connection.Close();
            return listOfItems;
        }
        public bool UnAllocateClassRoom()
        {
            SqlConnection connection = new SqlConnection(conString);
            string query = "UPDATE ClassRoomAllocations SET Status = "+"1"+" ";
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