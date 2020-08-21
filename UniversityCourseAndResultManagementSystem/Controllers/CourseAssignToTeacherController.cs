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
    public class CourseAssignToTeacherController : Controller
    {
        private UniversityManagementEntities db = new UniversityManagementEntities();

        public ActionResult Index(CourseAssignToTeacher courseAssign)
        {
          
            return View(db.CourseAssignToTeachers.ToList());
        }
        public ActionResult CourseAssignToTeacher()
        { 
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
          
            return View();
        }
        [HttpPost]
        public ActionResult CourseAssignToTeacher(CourseAssignToTeacher courseAssign)
        {
        
         
                var course = db.Courses.FirstOrDefault(x => x.CourseId == courseAssign.CourseId);
                var teacher = db.Teachers.FirstOrDefault(x => x.TeacherId == courseAssign.TeacherId);

            if (IsCourseCodeExist(course.CourseCode.ToString()) && IsTeacherNameExist(teacher.TeacherName.ToString()))
            {
                teacher.TeacherRemainingCredit = teacher.TeacherRemainingCredit - course.CourseCredit;
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();

                courseAssign.Status = "Assigned";

                db.Entry(courseAssign).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("CourseAssignToTeacher", "CourseAssignToTeacher").WithNotice("Course Assign To Teacher Successfully Saved");


            }


            return RedirectToAction("CourseAssignToTeacher", "CourseAssignToTeacher").WithNotice("Not Saved Bcoz Teacher Name Or Course Code Already Exist");

        }
        public bool IsCourseCodeExist(string courseCode)
        {
            var course = db.CourseAssignToTeachers.ToList();
            if (!course.Any(x => x.Course.CourseCode.ToLower() == courseCode.ToLower()))
            {
                return true;
            }
            return false;

        }
        public bool IsTeacherNameExist(string teacherName)
        {
            var teacher = db.CourseAssignToTeachers.ToList();
            if (!teacher.Any(x => x.Teacher.TeacherName.ToLower() == teacherName.ToLower()))
            {
                return true;
            }
            return false;

        }
        public JsonResult GetAllAssignCourses(int deptId)
        {
            var courseStatics = new CourseAssignTeacherGateway().GetAllAssignCourses(deptId);

            return Json(courseStatics, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeacherByDeptId(int departmentId)
        {
            var teachers = db.Teachers.Where(x => x.DepartmentId == departmentId).ToList();
            return Json(teachers);
        }

        public JsonResult GetCourseByDeptId(int departmentId)
        {
            var courses = db.Courses.Where(x => x.DepartmentId == departmentId).ToList();

            return Json(courses);
        }
        public JsonResult GetCreditToBeTakenById(int teacherId)
        {
            var teacher = db.Teachers.FirstOrDefault(x => x.TeacherId == teacherId);

            return Json(teacher);
        }
   

        public JsonResult GetCourseCodeById(int courseId)
        {
            var course = db.Courses.FirstOrDefault(x => x.CourseId == courseId);

            return Json(course);
        }
        public ActionResult CreateCourseStaticsByDeptId()
        {
            ViewBag.DepartmentId =new  SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentCode");
            return View();
        }
       

        public ActionResult UnassignCourse()
        {
            ViewData["Message"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult UnassignCourse( string unAssignCourse)
        {
         
            foreach(var item in db.Courses)
            {
              
                db.Entry(item).State = EntityState.Modified;
            }
          
            db.SaveChanges();
            ViewData["Message"] = "Successfully UnAssign";
      
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