using System;
using System.Web.Mvc;
using System.Threading.Tasks;


using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace SmartRoom.Web.Areas.YouTube.Controllers
{
    public class BroadcastController : Controller
    {

        private readonly Authen youtubeAuthen = new Authen(new[] { 
                        "https://www.googleapis.com/auth/youtube",  
                        "https://www.googleapis.com/auth/plus.login" });
        private YouTubeService youtube;

        public async Task<LiveBroadcast> createBroadcast(String kind, String snippetTitle, DateTime startTime, DateTime endTime, String privacyStatus)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());
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
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            // Set stream kind
            liveStream.Kind = kind;

            // Set stream's snippet and title.
            liveStream.Snippet = new LiveStreamSnippet();
            liveStream.Snippet.Title = snippetTitle;

            //Set stream's Cdn
            liveStream.Cdn = new CdnSettings();
            liveStream.Cdn.Format = CDNFormat;
            liveStream.Cdn.IngestionType = CDNIngestionType;

            liveStream.Status.StreamStatus = "active";


            LiveStream returnedStream = youtube.LiveStreams.Insert(liveStream, "snippet,cdn,status").Execute();

            return returnedStream;
        }

        public async Task<LiveBroadcast> bindBroadcast(LiveBroadcast broadcast, LiveStream Livestream)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            LiveBroadcastsResource.BindRequest liveBroadcastBind = youtube.LiveBroadcasts.Bind(broadcast.Id, "id,contentDetails");
            liveBroadcastBind.StreamId = Livestream.Id;
            LiveBroadcast returnedBroadcast = liveBroadcastBind.Execute();
            returnedBroadcast.ContentDetails.EnableEmbed = true;

            return returnedBroadcast;

        }

        public async Task<LiveBroadcast> updateBroadcast(LiveBroadcast broadcast)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

           // LiveBroadcastsResource.UpdateRequest liveBroadcastUpdates = youtube.LiveBroadcasts.Update();

          // LiveBroadcast returnedBroadcast = liveBroadcastUpdates.Execute();
          //   returnedBroadcast.ContentDetails.EnableEmbed = true;

            return broadcast;

        }

        public async Task<LiveBroadcast> transitionBroadcast(LiveBroadcast broadcast, String streamId, String broadcastStatus)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            LiveStreamsResource.ListRequest streamStatus = youtube.LiveStreams.List("id");
            streamStatus.Id = 
            List<LiveStream> streamCheck = streamStatus.Execute();

            if (broadcast.Status.Equals(broadcastStatus)) {
                return broadcast;
            }

            LiveBroadcastsResource.TransitionRequest liveBroadcastTransition = youtube.LiveBroadcasts.Transition();

            LiveBroadcast returnedBroadcast = liveBroadcastTransition.Execute();
            returnedBroadcast.ContentDetails.EnableEmbed = true;

            return returnedBroadcast;

        }

        public async Task<LiveBroadcast> deleteBroadcast(LiveBroadcast broadcast)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

          //  LiveBroadcastsResource.UpdateRequest liveBroadcastUpdates = youtube.LiveBroadcasts.Update();

          //  LiveBroadcast returnedBroadcast = liveBroadcastUpdates.Execute();
          //  returnedBroadcast.ContentDetails.EnableEmbed = true;

            return broadcast;

        }
    }
}