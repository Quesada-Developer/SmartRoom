using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNet.Identity;
using SmartRoom.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartRoom.Web.Controllers
{
    public class HomeController : Controller
    {
        SmartModel model = new SmartModel();

        [Authorize]
        public async Task<ActionResult> Index()
        {
            //ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //var account = new AccountController();
            //account.UserManager.
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //var a = model.Users.Find(User.Identity.GetUserId()).Course;
            List<Course> CourseList = new List<Course>();
            try
            {
                //CourseList = model.Users.Find(User.Identity.GetUserId()).Courses.ToList();
                var id = User.Identity.GetUserId();
                CourseList = model.Courses.Where(obj => obj.UserRelationships.Any(ur => ur.AccountId.Equals(id) && ur.IsActive)).ToList();
            }
            catch(Exception e)
            {

            }
            //await CreateCalendar();

            return View(CourseList);
        }
        private async Task CreateCalendar()
        {
            CalendarService googlecalender = new CalendarService(await (new GoogleAuthentication()).GetInitializer());
            Calendar calendar = new Calendar();
            calendar.Summary = "sampleCalendar";
            calendar.Id = "#contacts@group.v.calendar.google.com";
            calendar.Kind = "calendar#calendar";

            var calendarRequest = googlecalender.Calendars.Insert(calendar);
            var result = calendarRequest.Execute();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}