using SmartRoom.Web.App_Start;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.YouTube.Controllers
{
    public class CoursePlaylistsController : Controller
    {
        private SmartModel db = new SmartModel();

        // GET: YouTube/CoursePlaylists
        public ActionResult Index()
        {
            var coursePlaylist = db.CoursePlaylists.Include(c => c.Course);
            return View(coursePlaylist.ToList());
        }

        // GET: YouTube/CoursePlaylists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePlaylist coursePlaylist = db.CoursePlaylists.Find(id);
            if (coursePlaylist == null)
            {
                return HttpNotFound();
            }
            return View(coursePlaylist);
        }

        // GET: YouTube/CoursePlaylists/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            return View();
        }

        // POST: YouTube/CoursePlaylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoursePlaylistId,CourseId,PlaylistId,PlaylistName")] CoursePlaylist coursePlaylist)
        {
            if (ModelState.IsValid)
            {
                db.CoursePlaylists.Add(coursePlaylist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", coursePlaylist.CourseId);
            return View(coursePlaylist);
        }

        // GET: YouTube/CoursePlaylists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePlaylist coursePlaylist = db.CoursePlaylists.Find(id);
            if (coursePlaylist == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", coursePlaylist.CourseId);
            return View(coursePlaylist);
        }

        // POST: YouTube/CoursePlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoursePlaylistId,CourseId,PlaylistId,PlaylistName")] CoursePlaylist coursePlaylist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursePlaylist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", coursePlaylist.CourseId);
            return View(coursePlaylist);
        }

        // GET: YouTube/CoursePlaylists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePlaylist coursePlaylist = db.CoursePlaylists.Find(id);
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
            CoursePlaylist coursePlaylist = db.CoursePlaylists.Find(id);
            db.CoursePlaylists.Remove(coursePlaylist);
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
