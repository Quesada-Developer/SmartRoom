using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartRoom.Web.App_Start
{
    public class GoogleAuthentication
    {
        private ClientSecrets ClientSecret;
        private const string AuthURL = "https://accounts.google.com/o/oauth2/auth";
        private const string TokenURL = "https://accounts.google.com/o/oauth2/token";
        private const string ClientEmail = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql@developer.gserviceaccount.com";
        private UserCredential Credential;
        private string[] Scope;

        public GoogleAuthentication()
        {
            ClientSecret = new ClientSecrets();
            ClientSecret.ClientSecret = "WIeQIEArSTs1P_drjOQvsSiC";

            ClientSecret.ClientId = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com";
            Scope = new[] {
                    "https://www.googleapis.com/auth/youtube",  
                    "https://www.googleapis.com/auth/plus.login",
                    "https://www.googleapis.com/auth/calendar"
                };
        }

        private async void Authorize()
        {

            Credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                ClientSecret,
                Scope,
                "user",
                CancellationToken.None
                );
        }
        public async Task<BaseClientService.Initializer> GetInitializer()
        {
            if (Credential == null)
                Authorize();
            else if (Credential.Token.IsExpired(SystemClock.Default))
            {
                Boolean tokenRefreshed = await Credential.RefreshTokenAsync(CancellationToken.None);
            }

            return new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = Properties.Settings.Default.ApplicationName
            };
        }
    }

}