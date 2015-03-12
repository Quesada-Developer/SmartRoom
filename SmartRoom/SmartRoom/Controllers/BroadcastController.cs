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
using System.IO;

namespace SmartRoom.Web.Controllers
{
    public class BroadcastController : Controller
    {
        //private readonly static string serviceAccountEmail = "1084733801830-qg01saqaeoicgtgv5c6l6qlduotsvnsu@developer.gserviceaccount.com";

        //private readonly static string[] scopes = new string[] {
        //        YouTubeService.Scope.Youtube
        //     };

        //private readonly static X509Certificate2 certificate = new X509Certificate2("C:\\Users\\jquesad\\Documents\\GitHub\\SmartRoom\\SmartRoom\\SmartRoom\\SmartRoom-97a44346405c.p12", "notasecret", X509KeyStorageFlags.Exportable);
        //private readonly static UserCredential credential = new UserCredential(
        //    new UserCredential
        //    {
        //        Scopes = scopes
        //    }.FromCertificate(certificate));
        //private readonly static YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
        //{
            
        //    HttpClientInitializer = credential,
        //    ApiKey = "AIzaSyDDnEMgDDPgQm1mOSktMciDsbnLq41rHcQ"
        //});

        //private static YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
        //{
        //    ApiKey = "AIzaSyDDnEMgDDPgQm1mOSktMciDsbnLq41rHcQ"
        //});

        public async Task<LiveBroadcast> createBroadcast(String kind, String snippetTitle, DateTime startTime, DateTime endTime, String privacyStatus)
        {

            UserCredential credential;
            using (var stream = new FileStream("C:\\Users\\scarver6\\Documents\\Visual Studio 2013\\Projects\\SmartRoom\\client_secret_1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.Youtube,
                        "https://www.googleapis.com/auth/youtube",  
                        "https://www.googleapis.com/auth/plus.login" },
                    "user",
                    CancellationToken.None
                );
            }


            var youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SmartRoom"
            });


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
            UserCredential credential;
            using (var stream = new FileStream("C:\\Users\\scarver6\\Documents\\Visual Studio 2013\\Projects\\SmartRoom\\client_secret_1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.Youtube,
                        "https://www.googleapis.com/auth/youtube",  
                        "https://www.googleapis.com/auth/plus.login" },
                    "user",
                    CancellationToken.None
                );
            }
            
            LiveStream liveStream = new LiveStream();

            var youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SmartRoom"
            });

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

            UserCredential credential;
            using (var stream = new FileStream("C:\\Users\\scarver6\\Documents\\Visual Studio 2013\\Projects\\SmartRoom\\client_secret_1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.Youtube,
                        "https://www.googleapis.com/auth/youtube",  
                        "https://www.googleapis.com/auth/plus.login" },
                    "user",
                    CancellationToken.None
                );
            }

            var youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SmartRoom"
            });
            
           
            LiveBroadcastsResource.BindRequest liveBroadcastBind = youtube.LiveBroadcasts.Bind(broadcast.Id, "id,contentDetails");
            liveBroadcastBind.StreamId = Livestream.Id;
            LiveBroadcast returnedBroadcast = liveBroadcastBind.Execute(); 
            returnedBroadcast.ContentDetails.EnableEmbed = true;
 
            return returnedBroadcast;

        }
    }
}