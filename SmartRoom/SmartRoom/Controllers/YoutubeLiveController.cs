﻿using System;
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
            var youtubelivedetails = db.YoutubeLiveDetails.Include(y => y.Id);
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
            return View(youtubelivedetail);
        }

        // GET: /YoutubeLive/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Courses, "Id", "Subject");
            return View();
        }

        // POST: /YoutubeLive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,BroadcastId,BroadcastTitle,BroadcastDescription,BroadcastScheduledStartTime,BroadcastScheduledEndTime,BroadcastStatus,StreamId,StreamTitle,StreamKind,StreamSnippetTitle,StreamCDNFormat,StreamCDNIngestionType,StreamCDNIngestionUrl")] YoutubeLiveDetail youtubelivedetail)
        {
            BroadcastController liveController = new BroadcastController();
           
            // Create broadcast and stream for YoutubeLive
            LiveBroadcast broadcast = liveController.createBroadcast("youtube#liveBroadcast", youtubelivedetail.BroadcastTitle, youtubelivedetail.BroadcastScheduledStartTime, youtubelivedetail.BroadcastScheduledEndTime, youtubelivedetail.BroadcastStatus);
            LiveStream stream = liveController.createStream("youtube#liveStream", youtubelivedetail.StreamSnippetTitle, youtubelivedetail.StreamCDNFormat, "rtmp");

            // Bind them together
            LiveBroadcast bindedBroadcast = liveController.bindBroadcast(broadcast, stream);
            
            // Values to-be inserted updated
            youtubelivedetail.BroadcastId = bindedBroadcast.Id;
            youtubelivedetail.StreamId = stream.Id;
            youtubelivedetail.StreamCDNIngestionUrl = stream.Cdn.IngestionInfo.IngestionAddress;

            if (ModelState.IsValid)
            {
                db.YoutubeLiveDetails.Add(youtubelivedetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Courses, "Id", "Subject", youtubelivedetail.Id);
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
            ViewBag.Id = new SelectList(db.Courses, "Id", "Subject", youtubelivedetail.Id);
            return View(youtubelivedetail);
        }

        // POST: /YoutubeLive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,BroadcastId,BroadcastTitle,BroadcastDescription,BroadcastScheduledStartTime,BroadcastScheduledEndTime,BroadcastStatus,StreamId,StreamTitle,StreamKind,StreamSnippetTitle,StreamCDNFormat,StreamCDNIngestionType,StreamCDNIngestionUrl")] YoutubeLiveDetail youtubelivedetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(youtubelivedetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Courses, "Id", "Subject", youtubelivedetail.Id);
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