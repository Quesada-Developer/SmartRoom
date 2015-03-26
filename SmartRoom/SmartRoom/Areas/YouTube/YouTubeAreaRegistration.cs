using System.Web.Mvc;

namespace SmartRoom.Web.Areas.YouTube
{
    public class YouTubeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "YouTube";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "YouTube_default",
                "YouTube/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}