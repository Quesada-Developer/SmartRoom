using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRoom.Database.Tables;
using SmartRoom.Database;

using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace SmartRoom.Web.Controllers
{
    public class YoutubeLiveController : Controller
    {
        private SmartModel db = new SmartModel();

        // GET: /YoutubeLive/
        public ActionResult Index()
        {
            var youtubelivedetails = db.YoutubeLiveDetails.Include(y => y.Course);
            return View(youtubelivedetails.ToList());
        }

        // GET: /YoutubeLive/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YoutubeLiveDetail youtubelivedetail = db.YoutubeLiveDetails.Find(id);
            if (youtubelivedetail == null)
            {
                return HttpNotFound();
            }
            youtubelivedetail.Embededhtml = Server.HtmlEncode(youtubelivedetail.Embededhtml);
            return View(youtubelivedetail);
        }

        // GET: /YoutubeLive/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            return View();
        }

        // POST: /YoutubeLive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CourseId,BroadcastId,BroadcastTitle,BroadcastDescription,BroadcastScheduledStartTime,BroadcastScheduledEndTime,BroadcastStatus,StreamId,StreamTitle,StreamKind,StreamSnippetTitle,StreamCDNFormat,StreamCDNIngestionType,StreamCDNIngestionUrl,Embededhtml")] YoutubeLiveDetail youtubelivedetail)
        {

            BroadcastController liveController = new BroadcastController();
           
            // Create broadcast and stream for YoutubeLive
            LiveBroadcast broadcast = await liveController.createBroadcast("youtube#liveBroadcast", youtubelivedetail.BroadcastTitle, youtubelivedetail.BroadcastScheduledStartTime, youtubelivedetail.BroadcastScheduledEndTime, youtubelivedetail.BroadcastStatus);
            LiveStream stream = await liveController.createStream("youtube#liveStream", youtubelivedetail.StreamSnippetTitle, youtubelivedetail.StreamCDNFormat, "rtmp");

            // Bind them together
            LiveBroadcast bindedBroadcast = await liveController.bindBroadcast(broadcast, stream);
            
            // Values to-be inserted updated
            youtubelivedetail.BroadcastId = bindedBroadcast.Id;
            youtubelivedetail.StreamId = stream.Id;
            youtubelivedetail.StreamCDNIngestionUrl = stream.Cdn.IngestionInfo.IngestionAddress;
            youtubelivedetail.Embededhtml = bindedBroadcast.ContentDetails.MonitorStream.EmbedHtml;
            
                       
            if (ModelState.IsValid)
            {
                db.YoutubeLiveDetails.Add(youtubelivedetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", youtubelivedetail.CourseId);
            return View(youtubelivedetail);
        }

        // GET: /YoutubeLive/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YoutubeLiveDetail youtubelivedetail = db.YoutubeLiveDetails.Find(id);
            if (youtubelivedetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", youtubelivedetail.CourseId);
            return View(youtubelivedetail);
        }

        // POST: /YoutubeLive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,BroadcastId,BroadcastTitle,BroadcastDescription,BroadcastScheduledStartTime,BroadcastScheduledEndTime,BroadcastStatus,StreamId,StreamTitle,StreamKind,StreamSnippetTitle,StreamCDNFormat,StreamCDNIngestionType,StreamCDNIngestionUrl,Embededhtml")] YoutubeLiveDetail youtubelivedetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(youtubelivedetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", youtubelivedetail.CourseId);
            return View(youtubelivedetail);
        }

        // GET: /YoutubeLive/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YoutubeLiveDetail youtubelivedetail = db.YoutubeLiveDetails.Find(id);
            if (youtubelivedetail == null)
            {
                return HttpNotFound();
            }
            return View(youtubelivedetail);
        }

        // POST: /YoutubeLive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YoutubeLiveDetail youtubelivedetail = db.YoutubeLiveDetails.Find(id);
            db.YoutubeLiveDetails.Remove(youtubelivedetail);
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
