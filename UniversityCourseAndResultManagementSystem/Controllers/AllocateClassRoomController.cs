using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;
using FlashMessage;
namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class AllocateClassRoomController : Controller
    {
        UniversityManagementEntities db = new UniversityManagementEntities();
        public ActionResult Index(CourseAssignToTeacher courseAssign)
        {

            return View(db.CourseAssignToTeachers.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");

            ViewBag.Courses = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.Rooms = new SelectList(db.Rooms, "RoomId", "RoomNo");
            ViewBag.SevenDayWeeks = new SelectList(db.SevenDayWeeks, "DayId", "DayCode");


            return View();
        }
        [HttpPost]
        public ActionResult Create(ClassRoomAllocation classRoomAllocation)
        {
            classRoomAllocation.Status = "Allocate";
            classRoomAllocation.TimeFrom = classRoomAllocation.TimeFrom.Remove(2, 1);
            classRoomAllocation.TimeTo = classRoomAllocation.TimeTo.Remove(2, 1);
          
            var course = db.Courses.FirstOrDefault(x => x.CourseId == classRoomAllocation.CourseId);

            if (Convert.ToInt32(classRoomAllocation.TimeFrom) <= Convert.ToInt32(classRoomAllocation.TimeTo))
            {
                if (IsDayExist(classRoomAllocation.CourseId, classRoomAllocation.DayId))
                {
                    db.Entry(classRoomAllocation).State = EntityState.Added;
                    db.SaveChanges();

                    return RedirectToAction("Create", "AllocateClassRoom").WithNotice("Class Allocated Successfully");
                  
                }
                else
                {
                    return RedirectToAction("Create", "AllocateClassRoom").WithError("Class Not Allocated Becouse Day Already Assigned to particular course.");
                }
              
            }
        
            ModelState.Clear();
            return RedirectToAction("Create", "AllocateClassRoom").WithError("Time Formate Not Right");

        }
        //public bool IsTimeExist(int totalTime)
        //{
        //    var time = db.ClassRoomAllocations.ToList();

        //    if (!time.Any(x => Convert.ToInt32(x.TimeFrom+x.TimeTo)== totalTime))
        //    {
        //        return true;
        //    }
        //    return false;

        //}
        //public bool IsCourseCodeExist(string courseCode)
        //{
        //    var course = db.ClassRoomAllocations.ToList();
        //    if (!course.Any(x => x.Course.CourseCode.ToLower() == courseCode.ToLower()))
        //    {
        //        return true;
        //    }
        //    return false;

        //}
        public bool IsDayExist( int courseId, int dayId)
        {
            var classAllocation = db.ClassRoomAllocations.ToList();
            if (!classAllocation.Any(x => x.DayId == dayId&&x.CourseId==courseId))
            {
                return true;
            }
            return false;

        }
        public ActionResult ViewClassScheduleAndRoomAllocation()
        {
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");

            return View();
        }


        public JsonResult GetClassScheduleAndRoomAllocationByDeptId(int deptId)
        {
            var getAllClassScheduleViews = GetAllClassScheduleViews(deptId);
            return Json(getAllClassScheduleViews);
        }
        public JsonResult GetCourseByDeptId(int deptId)
        {
            var courses = db.Courses.Where(x => x.DepartmentId == deptId).ToList();

            return Json(courses);
        }
        public List<ClassScheduleView> GetAllClassScheduleViews(int departmentId)
        {
            ViewClassSchedule gateway = new ViewClassSchedule();
            List<ClassScheduleView> list = gateway.GetAllClassScheduleViewsByDeptId(departmentId);
            List<ClassScheduleView> myList = new List<ClassScheduleView>();

            for (var i = 0; i < list.Count;)
            {
                ClassScheduleView classView = list[i];
                ClassScheduleView temp = new ClassScheduleView();
                temp.CourseCode = classView.CourseCode;
                temp.CourseName = classView.CourseName;
                temp.ScheduleInfo = ("R. No : " + classView.RoomName + ", " + classView.DayShortName + ", " + classView.TimeFrom + " - " + classView.TimeTo) + "</br>";
                int ck = 0;
                i++;

                for (var j = i; j < list.Count; j++)
                {
                    ck = 1;
                    ClassScheduleView classView2 = list[j - 1];
                    ClassScheduleView classView3 = list[j];

                    if (classView2.CourseCode == classView3.CourseCode)
                    {
                        i++;
                        temp.ScheduleInfo += ("R. No : " + classView3.RoomName + ", " + classView3.DayShortName + ", " +
                                                         classView3.TimeFrom + " - " + classView3.TimeTo + "</br>");

                        //myList.Add(temp);
                    }
                    else
                    {
                        if (classView.RoomName == "")
                        {
                            temp.ScheduleInfo = "Not Scheduled Yet";
                        }
                        myList.Add(temp);
                        break;
                    }
                }
                if (ck == 0)
                {
                    if (classView.RoomName == "")
                    {
                        temp.ScheduleInfo = "Not Scheduled Yet";
                    }
                    myList.Add(temp);
                }

            }
            return myList;

        }

        [HttpGet]
        public ActionResult UnallocatedClassRooms()
        {
            ViewData["Message"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult UnallocatedClassRooms(string unallocatedClassRoom)
        {
            ViewClassSchedule gateway = new ViewClassSchedule();
            if (gateway.UnAllocateClassRoom())
            {
                ViewData["Message"] = "UnAllocate Successfully";
            }
            else
            {
                ViewData["Message"] = "UnAllocate Failed";
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}