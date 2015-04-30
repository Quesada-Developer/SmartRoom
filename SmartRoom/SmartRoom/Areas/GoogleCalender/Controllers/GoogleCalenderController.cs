using Google.Apis.Calendar.v3;
using SmartRoom.Web.App_Start;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.GoogleCalender.Controllers
{
    public class GoogleCalenderController : Controller
    {

        private readonly GoogleAuthentication googleAuthen = new GoogleAuthentication();
        private CalendarService googlecalender;

        public async Task<ActionResult >Index()
        {
            googlecalender = new CalendarService(await googleAuthen.GetInitializer());
            string pageToken = null;
            do
            {
                //Events events = googlecalender.CalendarList.Get("SmartRoom");
            }
            while (pageToken != null);


            return View();
        }
    }
}