namespace SmartRoom.Database
{
    using SmartRoom.Database.Tables;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;


    public class ApplicationUser : IdentityUser
    {
        public string RandomName { get; set; }
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
    public class SmartModel : DbContext
    {
        // Your context has been configured to use a 'SmartModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SmartRoom.Database.SmartModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SmartModel' 
        // connection string in the application configuration file.
        public SmartModel()
            : base("name=SmartModel")
        {
            
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassRole> ClassRoles { get; set; }
        public DbSet<CourseOption> CourseOptions { get; set; }
        public DbSet<YoutubeLiveDetail> YoutubeLiveDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(s=>s.Id)
                .Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Course>()
                .HasRequired(s => s.CourseOptions).WithRequiredPrincipal(s => s.Course);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}