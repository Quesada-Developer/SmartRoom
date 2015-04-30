using Microsoft.AspNet.Identity;
using SmartRoom.Web.App_Start;
using SmartRoom.Web.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Forms.Controllers
{
    public class AnnouncementsController : Controller
    {
        private SmartModel db = new SmartModel();

        // GET: Forms/Announcements
        public ActionResult Index(int? id)
        {
            List<Announcement> announcements;
            if(id == null)
                announcements = db.Announcements.Include(a => a.Course).ToList();
            else
                announcements = db.Announcements.Where(x=>x.CourseId==id).Include(a => a.Course).ToList();
            var userId = User.Identity.GetUserId();
            var flag = db.UserRelationships.Where(x => x.AccountId == userId).Any(x=>x.AccountRole == CourseRole.owner);
            ViewBag.Professor = flag;
            return View(announcements);
        }

        // GET: Forms/Announcements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // GET: Forms/Announcements/Create/Id
        public ActionResult Create(int? id)
        {
            if(id == null)
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            else
            ViewBag.CourseId = new SelectList(db.Courses.Where(x=>x.Id == id), "Id", "Title");

            return View();
        }

        // POST: Forms/Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PublishOn,NewsTitle,NewsText,CourseId")] Announcement announcement)
        {
            announcement.CreatedBy = User.Identity.GetUserId();
            announcement.IsActive = true;

            ModelState.Clear();
            TryValidateModel(announcement); 

            if (ModelState.IsValid)
            {
                db.Announcements.Add(announcement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", announcement.CourseId);
            return View(announcement);
        }

        // GET: Forms/Announcements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", announcement.CourseId);
            return View(announcement);
        }

        // POST: Forms/Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PublishOn,NewsTitle,NewsText,CreatedBy,CreateDate,ModifedBy,ModifiedDate,IsActive,CourseId")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", announcement.CourseId);
            return View(announcement);
        }

        // GET: Forms/Announcements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Forms/Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
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
