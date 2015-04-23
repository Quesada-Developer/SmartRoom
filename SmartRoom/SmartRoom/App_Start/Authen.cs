using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SmartRoom.Web.App_Start
{
    public class Authen
    {/*
        private readonly string AUTH_URL = "https://accounts.google.com/o/oauth2/auth";
        private ClientSecrets CLIENT_SECRET = new ClientSecrets();
        private readonly string TOKEN_URL = "https://accounts.google.com/o/oauth2/token";
        private readonly string CLIENT_EMAIL = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql@developer.gserviceaccount.com";
        private UserCredential credential;
        private string[] scope;
        
        public Authen(string[] scopes)
        {
            CLIENT_SECRET.ClientSecret="WIeQIEArSTs1P_drjOQvsSiC";
            CLIENT_SECRET.ClientId="1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com";
            scope = scopes;
        }
        public async Task<BaseClientService.Initializer> getInitializer()
        {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                CLIENT_SECRET,
                scope,
                "user",
                CancellationToken.None
                );

            return new BaseClientService.Initializer(){
                HttpClientInitializer = credential,
                ApplicationName = Properties.Settings.Default.ApplicationName
            };
        }*/
    }
}