using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using System.IO;



namespace SmartRoom.Web.Controllers
{
    public class GoogleCalendarController : Controller
    {
        AccountController Account = new AccountController();
        private CalendarService calendar;

       //calendar = new CalendarService();

        //Account.UserManager.FindById(User.Identity.GetUserId()).GoogleAuthentication.GetInitializer()
        
        
        // GET: GoogleCalendar
        public async ActionResult Index()
        {
            calendar = new CalendarService(await Account.UserManager.FindById(User.Identity.GetUserId()).GoogleAuthentication.GetInitializer());
            
            // This example uses the client_secrets.json file for authorization.
            // This file can be downloaded from the Google Developers Console
            // project.
            using (var stream = new FileStream("client_secrets.json", FileMode.Open,
                FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    "user", CancellationToken.None,
                    new FileDataStore("Calendar.Auth.Store")).Result;
            }

            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });
            return View();
        }
    }
}