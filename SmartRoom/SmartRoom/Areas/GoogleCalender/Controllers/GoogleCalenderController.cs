using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Calendar.v3;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.GoogleCalender.Controllers
{
    using SmartRoom.Web.App_Start;
    using tranRef = Google.Apis.YouTube.v3.LiveBroadcastsResource.TransitionRequest;
    public class GoogleCalenderController : Controller
    {

        private readonly Authen googleAuthen = new Authen(new[] {   
                        "https://www.googleapis.com/auth/plus.login",
                        "https://www.googleapis.com/auth/Calendar"
        });
        private CalendarService googlecalender;

        // creates a broadcast verified
        /*public async Task<LiveBroadcast> createBroadcast(String kind, String snippetTitle, DateTime startTime, DateTime endTime, String privacyStatus)
        {
            googlecalender = new CalendarService(await googleAuthen.getInitializer());


            LiveBroadcast broadcast = new LiveBroadcast();

            // Set broadcast Kind
            broadcast.Kind = kind;

            // Set broadcast snippet 
            broadcast.Snippet = new LiveBroadcastSnippet();
            broadcast.Snippet.Title = snippetTitle;
            broadcast.Snippet.ScheduledStartTime = startTime;
            broadcast.Snippet.ScheduledEndTime = endTime;

            //Set broadcast status
            broadcast.Status = new LiveBroadcastStatus();
            broadcast.Status.PrivacyStatus = privacyStatus;


            LiveBroadcast returnedBroadcast = youtube.LiveBroadcasts.Insert(broadcast, "id,snippet,status").Execute();


            return returnedBroadcast;
        }*/
    }
}