using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRoom.Web.App_Start;

namespace SmartRoom.Web.Areas.YouTube.Controllers
{
    public class CoursePlaylistsController : Controller
    {
        private SmartModel db = new SmartModel();

        // GET: YouTube/CoursePlaylists
        public ActionResult Index()
        {
            var coursePlaylist = db.CoursePlaylist.Include(c => c.Course).Include(c => c.CreatedBy).Include(c => c.ModifedBy);
            return View(coursePlaylist.ToList());
        }

        // GET: YouTube/CoursePlaylists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePlaylist coursePlaylist = db.CoursePlaylist.Find(id);
            if (coursePlaylist == null)
            {
                return HttpNotFound();
            }
            return View(coursePlaylist);
        }

        // GET: YouTube/CoursePlaylists/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "RegistrationCode");
            ViewBag.CreatedById = new SelectList(db.ApplicationUsers, "Id", "Email");
            ViewBag.ModifedById = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: YouTube/CoursePlaylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoursePlaylistId,CourseId,PlaylistId,CreatedById,CreateDate,ModifedById,ModifiedDate")] CoursePlaylist coursePlaylist)
        {
            if (ModelState.IsValid)
            {
                db.CoursePlaylist.Add(coursePlaylist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "RegistrationCode", coursePlaylist.CourseId);
            ViewBag.CreatedById = new SelectList(db.ApplicationUsers, "Id", "Email", coursePlaylist.CreatedById);
            ViewBag.ModifedById = new SelectList(db.ApplicationUsers, "Id", "Email", coursePlaylist.ModifedById);
            return View(coursePlaylist);
        }

        // GET: YouTube/CoursePlaylists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePlaylist coursePlaylist = db.CoursePlaylist.Find(id);
            if (coursePlaylist == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "RegistrationCode", coursePlaylist.CourseId);
            ViewBag.CreatedById = new SelectList(db.ApplicationUsers, "Id", "Email", coursePlaylist.CreatedById);
            ViewBag.ModifedById = new SelectList(db.ApplicationUsers, "Id", "Email", coursePlaylist.ModifedById);
            return View(coursePlaylist);
        }

        // POST: YouTube/CoursePlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoursePlaylistId,CourseId,PlaylistId,CreatedById,CreateDate,ModifedById,ModifiedDate")] CoursePlaylist coursePlaylist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursePlaylist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "RegistrationCode", coursePlaylist.CourseId);
            ViewBag.CreatedById = new SelectList(db.ApplicationUsers, "Id", "Email", coursePlaylist.CreatedById);
            ViewBag.ModifedById = new SelectList(db.ApplicationUsers, "Id", "Email", coursePlaylist.ModifedById);
            return View(coursePlaylist);
        }

        // GET: YouTube/CoursePlaylists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePlaylist coursePlaylist = db.CoursePlaylist.Find(id);
            if (coursePlaylist == null)
            {
                return HttpNotFound();
            }
            return View(coursePlaylist);
        }

        // POST: YouTube/CoursePlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoursePlaylist coursePlaylist = db.CoursePlaylist.Find(id);
            db.CoursePlaylist.Remove(coursePlaylist);
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
