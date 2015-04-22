using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRoom.Web.App_Start;
using SmartRoom.Web.Helpers;

namespace SmartRoom.Web.Areas.Forms.Controllers
{
    public class SyllabusController : Controller
    {
        private SmartModel db = new SmartModel();

        // GET: Forms/Syllabus
        public ActionResult Index()
        {
            var syllabi = db.Syllabi.Include(s => s.Course).Include(s => s.Professor);
            return View(syllabi.ToList());
        }

        // GET: Forms/Syllabus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabus syllabus = db.Syllabi.Find(id);
            if (syllabus == null)
            {
                return HttpNotFound();
            }
            return View(syllabus);
        }

        // GET: Forms/Syllabus/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title"); ViewBag.ProfessorId = new SelectList(db.Users.Where(q => (db.UserRelationships.Where(x => x.AccountId == userId).Select(x => x.AccountId)).Contains(q.Id)), "Id", "UserName");
            return View(new Syllabus());
        }

        // POST: Forms/Syllabus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,ProfessorId,OfficeLocation,OfficePhone,CellPhone,EmergencyPhone,EmailAddress,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,MeetingTime,isActive")] Syllabus syllabus)
        {
            if (ModelState.IsValid)
            {
                db.Syllabi.Add(syllabus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", syllabus.CourseId);
            ViewBag.ProfessorId = new SelectList(db.Users.Where(q => (db.UserRelationships.Where(x => x.AccountId == userId).Select(x => x.AccountId)).Contains(q.Id)), "Id", "UserName", syllabus.ProfessorId);
            return View(syllabus);
        }

        // GET: Forms/Syllabus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabus syllabus = db.Syllabi.Find(id);
            if (syllabus == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", syllabus.CourseId);
            ViewBag.ProfessorId = new SelectList(db.Users.Where(q => (db.UserRelationships.Where(x => x.AccountId == userId).Select(x => x.AccountId)).Contains(q.Id)), "Id", "UserName", syllabus.ProfessorId);
            return View(syllabus);
        }

        // POST: Forms/Syllabus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,ProfessorId,OfficeLocation,OfficePhone,CellPhone,EmergencyPhone,EmailAddress,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,MeetingTime")] Syllabus syllabus)
        {
            syllabus.isActive = true;
            if (ModelState.IsValid)
            {
                db.Entry(syllabus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var userId = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", syllabus.CourseId);
            ViewBag.ProfessorId = new SelectList(db.Users.Where(q => (db.UserRelationships.Where(x => x.AccountId == userId).Select(x => x.AccountId)).Contains(q.Id)), "Id", "UserName", syllabus.ProfessorId);
            return View(syllabus);
        }

        // GET: Forms/Syllabus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabus syllabus = db.Syllabi.Find(id);
            if (syllabus == null)
            {
                return HttpNotFound();
            }
            return View(syllabus);
        }

        // POST: Forms/Syllabus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Syllabus syllabus = db.Syllabi.Find(id);
            syllabus.isActive = false;
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
