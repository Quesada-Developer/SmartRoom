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
using SmartRoom.Web.Controllers;

namespace SmartRoom.Web.Areas.Classroom.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SmartModel db = new SmartModel();
        private readonly AccountController Account = new AccountController();

        // GET: /Classroom/Courses/
        [Authorize]
        public ActionResult Index()
        {
            string _UserId = User.Identity.GetUserId();
            var list = db.Users.Find(_UserId);
            List<Course> Courses = (list != null)? list.CoursesByRole(Database.Helpers.CourseRole.owner).ToList(): new List<Course>();
            return View(Courses);
        }

        /*
         * Get User Courses
         * db.Users.Find(User.Identity.GetUserId()).Courses
         * 
         * Ger User Role For A Course
         * db.Users.Find(User.Identity.GetUserId()).RoleFromCourse(_Course)
         * 
         * Get a Courses for user were user is a owner
         * db.Users.Find(User.Identity.GetUserId()).CoursesByRole(Database.Helpers.CourseRole.owner)
         * 
         * */

        // GET: /Classroom/Courses/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string _UserId = User.Identity.GetUserId();
            Course course = db.Courses.Find(id);// db.Courses.Where(obj => obj.Id == id).FirstOrDefault();

            if (!course.UserRelationships.Any(obj => obj.AccountId.Equals(_UserId)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
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
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create([Bind(Include="Id,Subject,CourseNumber,Section,Title,StartDate,EndDate,Location,Term,CreateDate")] Course course)
        {
            course.CreatedById = db.Users.Find(User.Identity.GetUserId()).Id;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                UserRelationship _UserRelationship = new UserRelationship() { AccountId = course.CreatedById, AccountRole = Database.Helpers.CourseRole.owner };
                db.UserRelationships.Add(_UserRelationship);
                course.UserRelationships.Add(_UserRelationship);
                db.SaveChanges();
                db.Courses.Add(course);
                db.SaveChanges();
                db.Users.Find(User.Identity.GetUserId()).Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: /Classroom/Courses/Edit/5
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string _UserId = User.Identity.GetUserId();
            List<Course> Courses = db.UserRelationships.Where(obj => obj.Account.Id.Equals(_UserId)).Select(obj => obj.Course).ToList();
            Course course = db.Courses.Find(id);// db.Courses.Where(obj => obj.Id == id).FirstOrDefault();
            
            //User is owner of course or is a admin
            if(!course.UserRelationships.Any(obj => obj.AccountId.Equals(_UserId) && obj.AccountRole == Database.Helpers.CourseRole.owner) || !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
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
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit([Bind(Include="Id,Subject,CourseNumber,Section,Title,StartDate,EndDate,Location,Term,CreatedBy,CreateDate,ModifedBy,ModifiedDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.ModifedById = db.Users.Find(User.Identity.GetUserId()).Id;
                course.ModifiedDate = DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }
        [Authorize(Roles = "Teacher,Admin")]
        // GET: /Classroom/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Users.Find(User.Identity.GetUserId()).Courses.Where(obj => obj.Id == id).FirstOrDefault(); ;
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Classroom/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            /*Course course = db.Users.Find(User.Identity.GetUserId()).Courses.Where(obj => obj.Id == id).FirstOrDefault();
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");*/
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
