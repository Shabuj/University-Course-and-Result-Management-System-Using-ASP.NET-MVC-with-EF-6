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
    public class TeacherController : Controller
    {
        UniversityManagementEntities db = new UniversityManagementEntities();

        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }
        public ActionResult Create()
        {
          
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentName");
            ViewBag.Designations = new SelectList(db.Designations.ToList(), "DesignationId", "DesignationName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            var course = db.Courses.FirstOrDefault(x => x.DepartmentId == teacher.DepartmentId);
           
            if (ModelState.IsValid)
            {
               
                    teacher.TeacherRemainingCredit = teacher.CreditToBeTaken;
                db.Entry(teacher).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Create", "Teacher").WithNotice("Successfully Teacher Saved");
            }
            ModelState.Clear();
            return View().WithFlash("Not Saved");
        }

        public JsonResult IsEmailExist(string TeacherEmail)
        {
            var email = db.Teachers.ToList();
            if (!email.Any(x => x.TeacherEmail.ToLower() == TeacherEmail.ToLower()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);

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