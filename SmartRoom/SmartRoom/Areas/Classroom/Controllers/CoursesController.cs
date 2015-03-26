using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRoom.Database;
using Microsoft.AspNet.Identity;

namespace SmartRoom.Web.Areas.Classroom.Controllers
{
    public class CoursesController : Controller
    {
        private SmartModel db = new SmartModel();

        // GET: /Classroom/Courses/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.Find(User.Identity.GetUserId()).Course);
        }

        // GET: /Classroom/Courses/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Users.Find(User.Identity.GetUserId()).Course.Where(obj => obj.Id == id).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: /Classroom/Courses/Create
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Classroom/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include="Id,Subject,CourseNumber,Section,Title,StartDate,EndDate,Location,Term,CreatedBy,CreateDate,ModifedBy,ModifiedDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Users.Find(User.Identity.GetUserId()).Course.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: /Classroom/Courses/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Users.Find(User.Identity.GetUserId()).Course.Where(obj => obj.Id == id).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Classroom/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include="Id,Subject,CourseNumber,Section,Title,StartDate,EndDate,Location,Term,CreatedBy,CreateDate,ModifedBy,ModifiedDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }
        [Authorize(Roles = "Teacher")]
        // GET: /Classroom/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Users.Find(User.Identity.GetUserId()).Course.Where(obj => obj.Id == id).FirstOrDefault(); ;
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Classroom/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Users.Find(User.Identity.GetUserId()).Course.Where(obj => obj.Id == id).FirstOrDefault();
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
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
