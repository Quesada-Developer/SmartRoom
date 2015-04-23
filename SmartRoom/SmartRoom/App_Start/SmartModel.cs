using Microsoft.AspNet.Identity.EntityFramework;

using System.Data.Entity;

namespace SmartRoom.Web.App_Start
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
        public DbSet<Syllabus> Syllabi { get; set; }
        public DbSet<GradeDistribution> GradeDistributions { get; set; }
        public DbSet<ClassDate> ClassDates { get; set; }
        public DbSet<Information> AdditionalInformations { get; set; }
        public DbSet<CoursePlaylist> CoursePlaylists { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

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
    }

}