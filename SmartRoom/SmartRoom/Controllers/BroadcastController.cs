using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis;
using Google.Apis.Http;
using Google.Apis.Requests;
using Google.Apis.Util.Store;
using Google.Apis.Json;
using Google.Apis.Services;

using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using System.Security.Cryptography.X509Certificates;

namespace SmartRoom.Web.Controllers
{
    public class BroadcastController : Controller
    {
        private readonly static string serviceAccountEmail = "1084733801830-qg01saqaeoicgtgv5c6l6qlduotsvnsu@developer.gserviceaccount.com";

        private readonly static string[] scopes = new string[] {
                YouTubeService.Scope.Youtube,
                YouTubeService.Scope.YoutubeReadonly,
                YouTubeService.Scope.YoutubeUpload
             };

        private readonly static X509Certificate2 certificate = new X509Certificate2("C:\\Users\\scarver6\\Documents\\Visual Studio 2013\\Projects\\SmartRoom\\SmartRoom-361d028c817a.p12", "notasecret", X509KeyStorageFlags.Exportable);
        private readonly static ServiceAccountCredential credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes
            }.FromCertificate(certificate));
        private readonly static YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
        {
            
            HttpClientInitializer = credential,
            ApiKey = "AIzaSyDDnEMgDDPgQm1mOSktMciDsbnLq41rHcQ"
        });

        //private static YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
        //{
        //    ApiKey = "AIzaSyDDnEMgDDPgQm1mOSktMciDsbnLq41rHcQ"
        //});

        public LiveBroadcast createBroadcast(String kind, String snippetTitle, DateTime startTime, DateTime endTime, String privacyStatus)
        {
            
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

        public LiveStream createStream(String kind, String snippetTitle, String CDNFormat, String CDNIngestionType)
        {
            LiveStream stream = new LiveStream();

            // Set stream kind
            stream.Kind = kind;

            // Set stream's snippet and title.
            stream.Snippet = new LiveStreamSnippet();
            stream.Snippet.Title = snippetTitle;

            //Set stream's Cdn
            stream.Cdn = new CdnSettings();
            stream.Cdn.Format = CDNFormat;
            stream.Cdn.IngestionType = CDNIngestionType;

            String url = stream.Cdn.IngestionInfo.IngestionAddress;

            LiveStream returnedStream = youtube.LiveStreams.Insert(stream, "snippet,cdn").Execute();

            return returnedStream;
        }

        public LiveBroadcast bindBroadcast(LiveBroadcast broadcast, LiveStream stream)
        {

            LiveBroadcastsResource.BindRequest liveBroadcastBind = youtube.LiveBroadcasts.Bind(broadcast.Id, "id,contentDetails");
            liveBroadcastBind.StreamId = stream.Id;
            LiveBroadcast returnedBroadcast = liveBroadcastBind.Execute();

            return returnedBroadcast;

        }
    }
}