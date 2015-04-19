using SmartRoom.Web.App_Start;
using SmartRoom.Web.Controllers;
using SmartRoom.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Classroom.Controllers
{
    public class HeaderController : Controller
    {
        private readonly SmartModel db = new SmartModel();
        private readonly AccountController Account = new AccountController();

        public ActionResult _Header()
        {
            string _UserId = User.Identity.GetUserId();
            var list = db.Users.Find(_UserId);
            List<Course> Courses = (list != null) ? list.CoursesByRole(CourseRole.owner).ToList() : new List<Course>();
            return View(Courses);
        }
    }
}