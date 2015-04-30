using Google.Apis.Calendar.v3;
using SmartRoom.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Forms.Controllers
{
    public class EventController : Controller
    {
        private SmartModel db = new SmartModel();

        public async Task<ActionResult> Index()
        {
            GoogleAuthentication youtubeAuthen = new GoogleAuthentication();
            CalendarService googlecalender = new CalendarService(await youtubeAuthen.GetInitializer());
            var re = googlecalender.Events.List("bbell31@students.kennesaw.edu").Execute();
            return View(re.Items);
        }
        public ActionResult Edit(string id)
        {
            var ids = Convert.ToInt32(id);
            var t = db.ClassDates.Where(x => x.Id == ids).First();
            var ev = new EventData
            {
                Id = t.Id,
                Start = t.StartDate,
                End = t.EndDate,
                Text = t.Title
            };
            return View(ev);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["Id"]);
            DateTime start,end;
            DateTime.TryParse(form["StartDate"], out start);
            DateTime.TryParse(form["EndDate"], out end);
            string text = form["Title"];

            var record = db.ClassDates.Where(x => x.Id == id).First();
            record.StartDate = start;
            record.EndDate = end;
            record.Title = text;
            db.SaveChanges();

            return new JsonResult { Data = new Dictionary<string, object> { { "result", "OK" } } };
        }

        public class EventData
        {
            public int Id { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public SelectList Resource { get; set; }
            public string Text { get; set; }
        }

    }
}