using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.Threading;
using SmartRoom.Web.Properties;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Database.Tables
{
    class SmartRoomIdentityConfig
    {
    }


    public class GoogleAuthentication
    {
        enum AuthenScopePackage { All = 0, YouTube = 1, Calendar = 2 }
        ClientSecrets ClientSecret { get; }
        private readonly string AuthURL = "https://accounts.google.com/o/oauth2/auth";
        private readonly string TokenURL= "https://accounts.google.com/o/oauth2/token";
        private readonly string ClientEmail = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql@developer.gserviceaccount.com";
        private UserCredential Credential;
        private string[] Scope;

        public GoogleAuthentication(string[] Scopes)
        {
            ClientSecret.ClientSecret = "WIeQIEArSTs1P_drjOQvsSiC";
            ClientSecret.ClientId = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com";
            Scope = Scopes;
        }

        public GoogleAuthentication(AuthenScopePackage ScopePackage)
        {
            if(ScopePackage == null)
                throw new ArgumentNullException("AuthenScopePackage has no value.");
            switch(ScopePackage)
            {
                case AuthenScopePackage.YouTube:
                    Scope = new[] { 
                        "https://www.googleapis.com/auth/youtube",  
                        "https://www.googleapis.com/auth/plus.login" 
                    };
                    break;
                case AuthenScopePackage.Calendar: 
                    Scope = new[] {
                    "https://www.googleapis.com/auth/plus.login",
                    "https://www.googleapis.com/auth/calendar"
                };
                    break;
                case AuthenScopePackage.All:
                    Scope = new[] {
                    "https://www.googleapis.com/auth/youtube",  
                    "https://www.googleapis.com/auth/plus.login",
                    "https://www.googleapis.com/auth/calendar"
                };
                    break;
                default:
                    throw new ArgumentException("AuthenScopePackage is not valid.");
            }
            ClientSecret.ClientSecret = "WIeQIEArSTs1P_drjOQvsSiC";
            ClientSecret.ClientId = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql.apps.googleusercontent.com";
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
            Authorize();
            return new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = SmartRoom.Web.Properties.Settings.Default.ApplicationName
            };
        }
    }

    [ComVisible(true)]
    public interface ISmartRoomIdentity : IIdentity
    {

        string AuthenticationType { get; }
        bool IsAuthenticated { get; }
        string Name { get; }
        [NotMapped]
        GoogleAuthentication GooglAuth { get; } 
    }



    public interface ISmartRoomPrincipal : IPrincipal
    {
        ISmartRoomIdentity Identity { get; }

        bool IsInRole(string role);
        Guid UserId { get; }
        string EmailAddress { get; }
    }

    public class SmartRoomIdentity : ISmartRoomIdentity
    {
        string AuthenticationType { get; }
        bool IsAuthenticated { get; }
        string Name { get; }
        [NotMapped]
        GoogleAuthentication GooglAuth { get; } 
    }

    public class SmartRoomPrincipal : ISmartRoomPrincipal
    {
        
    }
}
