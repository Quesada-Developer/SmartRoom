using System.Web.Mvc;

namespace SmartRoom.Web.Areas.GoogleCalender
{
    public class GoogleCalenderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GoogleCalender";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GoogleCalender_default",
                "GoogleCalender/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}