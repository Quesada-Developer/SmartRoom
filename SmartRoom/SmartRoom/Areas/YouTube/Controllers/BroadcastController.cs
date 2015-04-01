using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;


using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
// 

namespace SmartRoom.Web.Areas.YouTube.Controllers
{
    using tranRef = Google.Apis.YouTube.v3.LiveBroadcastsResource.TransitionRequest;
    public class BroadcastController : Controller
    {

        private readonly Authen youtubeAuthen = new Authen(new[] { 
                        "https://www.googleapis.com/auth/youtube",  
                        "https://www.googleapis.com/auth/plus.login" });
        private YouTubeService youtube;

        // creates a broadcast verified
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

        // creates a stream verified
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

        // Binds a stream to a broadcast verified
        public async Task<LiveBroadcast> bindBroadcast(LiveBroadcast broadcast, LiveStream Livestream)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            LiveBroadcastsResource.BindRequest liveBroadcastBind = youtube.LiveBroadcasts.Bind(broadcast.Id, "id,contentDetails");
            liveBroadcastBind.StreamId = Livestream.Id;
            LiveBroadcast returnedBroadcast = liveBroadcastBind.Execute();
            returnedBroadcast.ContentDetails.EnableEmbed = true;

            return returnedBroadcast;

        }

        // Redo
        public async Task<LiveBroadcast> transitionBroadcast(LiveBroadcast broadcast, String streamId, tranRef.BroadcastStatusEnum broadcastStatusEnum)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            IList<LiveStream> streams = await listStream();

            for (int i = 0; i < streams.Count; i++)
            {
                if (streams[i].Id == streamId && streams[i].Status.StreamStatus == "active")
                {

                    LiveBroadcastsResource.TransitionRequest liveBroadcastTransition = youtube.LiveBroadcasts.Transition(broadcastStatusEnum, broadcast.Id, "id,snippet,contentDetails,status");

                    LiveBroadcast broadcastResponse = liveBroadcastTransition.Execute();

                    return broadcastResponse;

                }
            }

            return broadcast;

        }

        // needs to be tested
        public async Task<LiveBroadcast> updateBroadcast(String broadcastId, String snippetTitle, DateTime startTime, DateTime endTime, String privacyStatus)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            IList<LiveBroadcast> broadcasts = await listBroadcast();

            for (int i = 0; i < broadcasts.Count; i++)
            {
                if (broadcasts[i].Id == broadcastId)
                {
                    LiveBroadcast updateBroadcast = new LiveBroadcast();

                    // Set broadcast snippet 
                    updateBroadcast.Snippet = new LiveBroadcastSnippet();
                    updateBroadcast.Snippet.Title = snippetTitle;
                    updateBroadcast.Snippet.ScheduledStartTime = startTime;
                    updateBroadcast.Snippet.ScheduledEndTime = endTime;

                    //Set broadcast status
                    updateBroadcast.Status = new LiveBroadcastStatus();
                    updateBroadcast.Status.PrivacyStatus = privacyStatus;

                    LiveBroadcastsResource.UpdateRequest liveBroadcastUpdate = youtube.LiveBroadcasts.Update(updateBroadcast, "id, snippet,contentDetails,status");

                    LiveBroadcast broadcastResponse = liveBroadcastUpdate.Execute();

                    return broadcastResponse;

                }
            }
            throw new System.ArgumentException("Parameter must belong to user and not be null", "Invalid");
        }


        // Redo
        public async Task<String> deleteBroadcast(String broadcastId)
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            

            return broadcast;

        }

        // Lists Streams belonging to authenticated user
        public async Task<IList<LiveStream>> listStream()
        {
          youtube = new YouTubeService(await youtubeAuthen.getInitializer());

          LiveStreamsResource.ListRequest streamRequest = youtube.LiveStreams.List("id,snippet");
          streamRequest.Mine = true;

          LiveStreamListResponse streamResponse = streamRequest.Execute();
          
          IList<LiveStream> streamList = streamResponse.Items;

          return streamList;

        }

        // Lists broadcasts belonging to authenticated user
        public async Task<IList<LiveBroadcast>> listBroadcast()
        {
            youtube = new YouTubeService(await youtubeAuthen.getInitializer());

            LiveBroadcastsResource.ListRequest broadcastRequest = youtube.LiveBroadcasts.List("id,snippet");
            broadcastRequest.Mine = true;

            LiveBroadcastListResponse broadcastResponse = broadcastRequest.Execute();

            IList<LiveBroadcast> broadcastList = broadcastResponse.Items;

            return broadcastList;

        }
    }
}