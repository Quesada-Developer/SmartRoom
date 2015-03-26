using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Google.Apis.Services;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using System;

namespace SmartRoom.Database
{/*
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            // Add custom user claims here
            if (Email.Contains("@students.kennesaw.edu"))
            {
                manager.AddToRole(Id, "Student");
            }
            else if (Email.Contains("@kennesaw.edu"))
            {
                manager.AddToRole(Id, "Teacher");
            }

            return userIdentity;
        }
        

    }

    public class GoogleAuthentication
    {
        ClientSecrets ClientSecret { get; }
        private readonly string AuthURL = "https://accounts.google.com/o/oauth2/auth";
        private readonly string TokenURL = "https://accounts.google.com/o/oauth2/token";
        private readonly string ClientEmail = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql@developer.gserviceaccount.com";
        private UserCredential Credential;
        private string[] Scope;

        public GoogleAuthentication()
        {
            Scope = new[] {
                    "https://www.googleapis.com/auth/youtube",  
                    "https://www.googleapis.com/auth/plus.login",
                    "https://www.googleapis.com/auth/calendar"
                };
            
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

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            IdentityResult roleResult;
            
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists("Student"))
            {
                roleResult = RoleManager.Create(new IdentityRole("Student"));
            }

            if (!RoleManager.RoleExists("Teacher"))
            {
                roleResult = RoleManager.Create(new IdentityRole("Teacher"));
            }
            base.Seed(context);
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            base.Configuration.AutoDetectChangesEnabled = true;
            System.Data.Entity.Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }*/
}