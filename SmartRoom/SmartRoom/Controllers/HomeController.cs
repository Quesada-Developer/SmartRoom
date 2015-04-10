using Microsoft.AspNet.Identity;
using SmartRoom.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SmartRoom.Web.Controllers
{
    public class HomeController : Controller
    {
        SmartModel model = new SmartModel();

        [Authorize]
        public ActionResult Index()
        {
            //ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //var account = new AccountController();
            //account.UserManager.
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //var a = model.Users.Find(User.Identity.GetUserId()).Course;
            List<Course> CourseList = new List<Course>();
            try
            {
                CourseList = model.Users.Find(User.Identity.GetUserId()).Courses.ToList();
            }
            catch(Exception e)
            {

            }

            return View(CourseList);
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