using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using SmartRoom.Web.App_Start;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace SmartRoom.Web.Areas.YouTube.Controllers
{
    public class YouTubeController : Controller
    {

        private SmartModel db = new SmartModel();

        // GET: /Youtube/
        public ActionResult Index()
        {
            var youtubelivedetails = db.YoutubeLiveDetails.Include(y => y.Course);
            return View(youtubelivedetails.ToList());
        }

        // GET: /Youtube/Details/5
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
            ViewBag.BroadcastEmbededhtml = youtubelivedetail.BroadcastEmbededhtml;
            return View(youtubelivedetail);
        }

        // GET: /Youtube/Create/Id
        public ActionResult Create(int? id)
        {
            if(id != null)
                ViewBag.CourseId = new SelectList(db.Courses.Where(x => x.Id == id).Select(x => x), "Id", "Title");
            else
                ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");

            return View();
        }

        // POST: /Youtube/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CourseId,BroadcastId,BroadcastKind,BroadcastTitle,BroadcastDescription,BroadcastScheduledStartTime,BroadcastScheduledEndTime,BroadcastStatus,BroadcastchannelId,BroadcastlifeCycleStatus,BroadcastEmbededhtml,StreamId,StreamKind,StreamName,StreamStatus,StreamSnippetTitle,StreamCDNFormat,StreamCDNIngestionType,StreamCDNIngestionUrl,StreamcontentclosedCaptionsIngestionUrl")] YoutubeLiveDetail youtubelivedetail)
        {
            BroadcastController liveController = new BroadcastController();

            youtubelivedetail.StreamCDNIngestionType = "rtmp";
            youtubelivedetail.BroadcastKind = "youtube#liveBroadcast";
            youtubelivedetail.StreamKind = "youtube#liveStream";
            // Create broadcast and stream for YoutubeLive
            LiveBroadcast broadcast = await liveController.createBroadcast(youtubelivedetail.BroadcastKind, youtubelivedetail.BroadcastTitle, youtubelivedetail.BroadcastScheduledStartTime, youtubelivedetail.BroadcastScheduledEndTime, youtubelivedetail.BroadcastStatus);
            LiveStream stream = await liveController.createStream(youtubelivedetail.StreamKind, youtubelivedetail.StreamSnippetTitle, youtubelivedetail.StreamCDNFormat, youtubelivedetail.StreamCDNIngestionType);

            // Bind them together
            LiveBroadcast bindedBroadcast = await liveController.bindBroadcast(broadcast, stream);
            // Values to-be inserted updated
            youtubelivedetail.BroadcastId = bindedBroadcast.Id;
            youtubelivedetail.BroadcastchannelId = bindedBroadcast.ContentDetails.BoundStreamId;
            youtubelivedetail.StreamName = stream.Cdn.IngestionInfo.StreamName;
            youtubelivedetail.StreamStatus = stream.Status.StreamStatus;
            youtubelivedetail.StreamId = stream.Id;
            youtubelivedetail.StreamCDNIngestionUrl = stream.Cdn.IngestionInfo.IngestionAddress;
          
            String id = (bindedBroadcast.ContentDetails.MonitorStream.EmbedHtml).ToString();
            YouTubeService youtube = new YouTubeService(await (new GoogleAuthentication()).GetInitializer());

                bindedBroadcast.ContentDetails.MonitorStream.EnableMonitorStream = false;
                LiveBroadcastsResource.UpdateRequest disablePreview = youtube.LiveBroadcasts.Update(bindedBroadcast, "contentDetails");
                LiveBroadcast returnBroadcast = disablePreview.Execute();

            //substring for browser
            if(id.Contains("embed/")){

                int startIndex = id.IndexOf("embed/");
                int endIndex = id.IndexOf("?");
                id = id.Substring(startIndex + 6, (endIndex - (startIndex + 6)));
            }

            youtubelivedetail.BroadcastEmbededhtml =  id;
                      
            //BroadCastID for Player

            if (ModelState.IsValid)
            {
                db.YoutubeLiveDetails.Add(youtubelivedetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", youtubelivedetail.CourseId);
            return View(youtubelivedetail);
        }

        public async Task<ActionResult> Start(int id)
        {
            var q = db.YoutubeLiveDetails.Where(x => x.Id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    var liveController = new BroadcastController();

                    var youtube = new YouTubeService(await (new GoogleAuthentication()).GetInitializer());
                    var broadcastRequest = youtube.LiveBroadcasts.List("id,status");
                    broadcastRequest.Id = q.BroadcastId;
                    var returnedList = broadcastRequest.Execute();
                    var broadcast = returnedList.Items.FirstOrDefault();

                    var streamRequest = youtube.LiveStreams.List("id,status");
                    streamRequest.Id = q.StreamId;
                    var streamList = streamRequest.Execute();
                    var stream = streamList.Items.FirstOrDefault();

                    if (broadcast != null && stream != null)
                    {
                        LiveBroadcast transBroadcast = await liveController.transitionBroadcast(broadcast, stream, Google.Apis.YouTube.v3.LiveBroadcastsResource.TransitionRequest.BroadcastStatusEnum.Live);
                    }
                }
                return View("Details", q);
            }
            catch
            {
                return View("Index", db.YoutubeLiveDetails);
            }
        }

        // GET: /Youtube/Edit/5
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

        // POST: /Youtube/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CourseId,BroadcastId,BroadcastKind,BroadcastTitle,BroadcastDescription,BroadcastScheduledStartTime,BroadcastScheduledEndTime,BroadcastStatus,BroadcastchannelId,BroadcastlifeCycleStatus,BroadcastEmbededhtml,StreamId,StreamKind,StreamName,StreamStatus,StreamSnippetTitle,StreamCDNFormat,StreamCDNIngestionType,StreamCDNIngestionUrl,StreamcontentclosedCaptionsIngestionUrl")] YoutubeLiveDetail youtubelivedetail)
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

        // GET: /Youtube/Edit/5
        public ActionResult Transition(int? id)
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

        // POST: /Youtube/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transition([Bind(Include = "Id,CourseId,BroadcastId,BroadcastKind,BroadcastTitle,BroadcastDescription,BroadcastScheduledStartTime,BroadcastScheduledEndTime,BroadcastStatus,BroadcastchannelId,BroadcastlifeCycleStatus,BroadcastEmbededhtml,StreamId,StreamKind,StreamName,StreamStatus,StreamSnippetTitle,StreamCDNFormat,StreamCDNIngestionType,StreamCDNIngestionUrl,StreamcontentclosedCaptionsIngestionUrl")] YoutubeLiveDetail youtubelivedetail)
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

        // GET: /Youtube/Delete/5
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

        // POST: /Youtube/Delete/5
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
