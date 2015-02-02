using SmartRoom.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SmartRoom.DAL
{
    public class SmartRoomContext : DbContext
    {
        public SmartRoomContext() : base("SmartRoomContext")
        {

        }

        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<ClassroomOptions> ClassroomOptions { get; set; }
        public DbSet<ClassroomRole> ClassroomRole { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}