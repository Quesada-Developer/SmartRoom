using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using SmartRoom.Web.Models;


namespace SmartRoom.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SmartModel db = new SmartModel();
        
        [Authorize]
        public ActionResult Index()
        {
            //ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //var account = new AccountController();
            //account.UserManager.
            //var user = UserManager.FindById(User.Identity.GetUserId());await Account.UserManager.FindById(User.Identity.GetUserId()).GoogleAuthentication.GetInitializer()
            var query = new GetCourseList(User.Identity.GetUserId());

            return View();
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