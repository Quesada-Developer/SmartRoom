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
        private readonly SmartModel model = new SmartModel();

        // GET: Classroom/Header
        public ActionResult _Header()
        {
            return PartialView(model.Users.Find(User.Identity.GetUserId()).Course);
        }
    }
}