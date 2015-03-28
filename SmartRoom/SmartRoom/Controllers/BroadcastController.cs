using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


namespace SmartRoom.Web.Controllers
{

    

    public class BroadcastController : Controller
    {
        AccountController Account = new AccountController();
        private YouTubeService youtube;

        public async Task<LiveBroadcast> createBroadcast(String kind, String snippetTitle, DateTime startTime, DateTime endTime, String privacyStatus)
        {

            youtube = new YouTubeService(await Account.UserManager.FindById(User.Identity.GetUserId()).GoogleAuthentication.GetInitializer());
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
        }

        public async Task<LiveStream> createStream(String kind, String snippetTitle, String CDNFormat, String CDNIngestionType)
        {
            LiveStream liveStream = new LiveStream();
            youtube = new YouTubeService(await Account.UserManager.FindById(User.Identity.GetUserId()).GoogleAuthentication.GetInitializer());

            // Set stream kind
            liveStream.Kind = kind;

            // Set stream's snippet and title.
            liveStream.Snippet = new LiveStreamSnippet();
            liveStream.Snippet.Title = snippetTitle;

            //Set stream's Cdn
            liveStream.Cdn = new CdnSettings();
            liveStream.Cdn.Format = CDNFormat;
            liveStream.Cdn.IngestionType = CDNIngestionType;
            

            LiveStream returnedStream = youtube.LiveStreams.Insert(liveStream, "snippet,cdn").Execute();

            return returnedStream;
        }

        public async Task<LiveBroadcast> bindBroadcast(LiveBroadcast broadcast, LiveStream Livestream)
        {
            youtube = new YouTubeService(await Account.UserManager.FindById(User.Identity.GetUserId()).GoogleAuthentication.GetInitializer());

            LiveBroadcastsResource.BindRequest liveBroadcastBind = youtube.LiveBroadcasts.Bind(broadcast.Id, "id,contentDetails");
            liveBroadcastBind.StreamId = Livestream.Id;
            LiveBroadcast returnedBroadcast = liveBroadcastBind.Execute(); 
            returnedBroadcast.ContentDetails.EnableEmbed = true;
 
            return returnedBroadcast;

        }
    }
}