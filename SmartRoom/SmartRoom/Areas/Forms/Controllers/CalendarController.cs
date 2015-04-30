using System;
using System.Collections.Generic;
using System.Linq;
using SmartRoom.Web.App_Start;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Forms.Controllers
{
    public class CalendarController : Controller
    {
        private SmartModel db = new SmartModel();

        public class JsonEvent
        {
            public string id { get; set; }
            public string text { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            
            public JsonEvent(string id, string text, string start, string end)
            {
                this.id = id;
                this.text = text;
                this.start = start;
                this.end = end;
            }
        }


        public ActionResult Events(DateTime? start, DateTime? end)
        {

            var events = db.ClassDates.Where(x=>x.EndDate <= start || x.StartDate>=end);
            List<JsonEvent> results = new List<JsonEvent>();
            foreach (var x in events)
                results.Add(new JsonEvent(x.Id.ToString(), x.Title, x.StartDate.ToString(), x.EndDate.ToString()));


            return new JsonResult { Data = results };
        }

        public ActionResult Create(string start, string end, string text)
        {
            var toBeCreated = new ClassDate();
            toBeCreated.StartDate = Convert.ToDateTime(start);
            toBeCreated.EndDate = Convert.ToDateTime(end);
            toBeCreated.Title = text;
            toBeCreated.isActive = true;
            db.ClassDates.Add(toBeCreated);
            db.SaveChanges();

            return new JsonResult { Data = new Dictionary<string, object> { { "id", toBeCreated.Id } } };

        }

        public ActionResult Move(int id, string newStart, string newEnd)
        {
            var toBeMoved = db.ClassDates.Where(x => x.Id == id).First();
            toBeMoved.StartDate = Convert.ToDateTime(newStart);
            toBeMoved.EndDate = Convert.ToDateTime(newEnd);
            db.SaveChanges();

            return new JsonResult { Data = new Dictionary<string, object> { { "id", toBeMoved.Id } } };
        }

    }
}