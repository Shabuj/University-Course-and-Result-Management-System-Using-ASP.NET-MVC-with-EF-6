using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Models;
using FlashMessage;
namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class EnrollCourseController : Controller
    {
        UniversityManagementEntities db = new UniversityManagementEntities();
        public ActionResult Index()
        {
            return View(db.EnrollInACourses.ToList());
        }
        public ActionResult CreateEnrollCourse()
        {
            ViewBag.Students = new SelectList(db.Students.ToList(), "StudentId", "StudentRegNo");
            ViewBag.Courses = new SelectList(db.Courses.ToList(), "CourseId", "CoursName");
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentCode");

            return View();
        }
        [HttpPost]

        public ActionResult CreateEnrollCourse(EnrollInACourse enrollInACourse)
        {
            enrollInACourse.Status = "Enroll";

            db.Entry(enrollInACourse).State = EntityState.Added;
            db.SaveChanges();

            return RedirectToAction("CreateEnrollCourse", "EnrollCourse").WithNotice("Enroll Successfully");
        }
        public JsonResult GetStudentNameEmailDeptByRegNo(int studentId)
        {
            var student = db.Students.FirstOrDefault(x => x.StudentId == studentId);

            return Json(student);
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

