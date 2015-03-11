using System.Web.Mvc;

namespace SmartRoom.Web.Areas.classroom
{
    public class classroomAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Classroom";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Classroom_default",
                "Classroom/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}