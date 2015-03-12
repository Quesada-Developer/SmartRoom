//Google.Apis.Calendar.v3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SmartRoom.Web.Models
{
    public class CalendarModel
    {
        /**THIS IS TO GET THE VERIFICATION FROM THE USER*/
        //added static because it complained
        static string clientId = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com";
        static string clientSecret = "WIeQIEArSTs1P_drjOQvsSiC";
        static string userName = ""; //  A string used to identify a user.
        static string[] scopes = new string[] {
            CalendarService.Scope.Calendar, // Manage your calendars
 	        CalendarService.Scope.CalendarReadonly // View your Calendars
            };

        // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
        //added static because it complained
        static UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { //added static
 		    ClientId = clientId, ClientSecret = clientSecret
 	        }, scopes, userName, CancellationToken.None, new FileDataStore("Daimto.GoogleCalendar.Auth.Store")).Result;

        /**THIS IS TO CREATE THE SERVICE*/
        // Create the service.
        CalendarService service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Calendar API Sample",
        });
        
        
        //RESOURCES:    Acl (access control)[delete, get, insert, list, patch, update, watch]
        //              CalendarList [delete, get, insert, list, patch, update, watch]
        //              Calenders [clear, delete, get, insert, patch, update]
        //              Channels [stop]
        //              Colors [get]
        //              Events [delete, get, import, insert, instances, list, move, patch, quickAdd, update, watch]
        //              FreeBusy [get]
        //              Settings [get, list, watch]
        //here is where I insert the Google API stuffs
    }
}