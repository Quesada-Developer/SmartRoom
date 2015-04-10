using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartRoom.Database.Helpers;
using SmartRoom.Web.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SmartRoom.Web
{
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
            //base.Configuration.LazyLoadingEnabled = true;
            System.Data.Entity.Database.SetInitializer<SmartModel>(new SmartModelInitializer());
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseOption> CourseOptions { get; set; }
        public DbSet<YoutubeLiveDetail> YoutubeLiveDetails { get; set; }
        public DbSet<UserRelationship> UserRelationships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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