using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SmartRoom.Web
{


    public class GoogleAuthentication
    {
        private ClientSecrets ClientSecret;
        private readonly string AuthURL = "https://accounts.google.com/o/oauth2/auth";
        private readonly string TokenURL = "https://accounts.google.com/o/oauth2/token";
        private readonly string ClientEmail = "1084733801830-4j2fje2ku2b6tkpa4v9v6cbbt08jeiql@developer.gserviceaccount.com";
        private UserCredential Credential;
        private string[] Scope;

        public GoogleAuthentication()
        {
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

    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// <example>await Account.UserManager.FindById(User.Identity.GetUserId()).GoogleAuthentication.GetInitializer()</example>
        /// </summary>
        [NotMapped]
        public GoogleAuthentication GoogleAuthentication { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            return userIdentity;
        }
        public virtual ICollection<UserRelationship> UserRelationships { get; set; }
    }
    public class SmartModel : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'SmartModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SmartRoom.Database.SmartModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SmartModel' 
        // connection string in the application configuration file.
        public SmartModel()
            : base("name=SmartModel", throwIfV1Schema: false)
        {
            base.Configuration.AutoDetectChangesEnabled = true;
            System.Data.Entity.Database.SetInitializer<SmartModel>(new SmartModelInitializer());
        }
        //public DbSet<Account> Accounts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserRelationship> ClassRoles { get; set; }
        public DbSet<CourseOption> CourseOptions { get; set; }
        public DbSet<YoutubeLiveDetail> YoutubeLiveDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(s=>s.Id)
                .Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Course>()
                .HasRequired(s => s.CourseOptions).WithRequiredPrincipal(s => s.Course);
            base.OnModelCreating(modelBuilder);
        }

        public static SmartModel Create()
        {
            return new SmartModel();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

}