using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Models;
using FlashMessage;
using Rotativa;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class SaveStudentResultController : Controller
    {
        UniversityManagementEntities db = new UniversityManagementEntities();
        public ActionResult Index()
        {
            return View(db.SaveStudentResults.ToList());
        }
        public ActionResult ViewResult()
        {
            ViewBag.Students = new SelectList(db.Students.ToList(), "StudentId", "StudentRegNo");
            ViewBag.Courses = new SelectList(db.Courses.ToList(), "CourseId", "CoursName");
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentCode");
            ViewBag.Grades = new SelectList(db.Grades.ToList(), "GradeId", "GradeLetter");

            return View();
        }
        public ActionResult CreateEnrollCourse()
        {
            ViewBag.Students = new SelectList(db.Students.ToList(), "StudentId", "StudentRegNo");
            ViewBag.Courses = new SelectList(db.Courses.ToList(), "CourseId", "CoursName");
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentCode");
            ViewBag.Grades = new SelectList(db.Grades.ToList(), "GradeId", "GradeLetter");

            return View();
        }
        [HttpPost]
        public ActionResult CreateEnrollCourse(SaveStudentResult saveStudentResult)
        {
            saveStudentResult.Status = "Enroll";

            db.Entry(saveStudentResult).State = EntityState.Added;
            db.SaveChanges();

            return RedirectToAction("CreateEnrollCourse", "EnrollCourse").WithNotice("Student Result Successfully");
        }

        public ActionResult PrintAll()
        {
            var q = new ActionAsPdf("ViewResult");
            return q;
        }
        public JsonResult GetStudentNameEmailDeptByRegNo(int studentId)
        {
            var students = db.SaveStudentResults.Where(x => x.StudentId == studentId).ToList();

            return Json(students);
        }
        public JsonResult GetStudentNameEmailDeptByStId(int studentId)
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