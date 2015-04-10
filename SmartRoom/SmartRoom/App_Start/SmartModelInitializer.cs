using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartRoom.Web.App_Start;
using System.Data.Entity;

namespace SmartRoom.Web
{
    class SmartModelInitializer : DropCreateDatabaseIfModelChanges<SmartModel>
    {
        protected override void Seed(SmartModel context)
        {
            var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));//<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists("Admin"))
            {
                RoleManager.Create(new IdentityRole("Admin"));
            }

            if (!RoleManager.RoleExists("Student"))
            {
                RoleManager.Create(new IdentityRole("Student"));
            }

            if (!RoleManager.RoleExists("Teacher"))
            {
                RoleManager.Create(new IdentityRole("Teacher"));
            }

/*
            var accounts = new List<ApplicationUser> {
            new ApplicationUser{ UserName="Francisco Quesada", Email="jquesada@students.kennesaw.edu"},
            new ApplicationUser{ UserName="Steven Carver", Email="scarver6@students.kennesaw.edu"},
            new ApplicationUser{ UserName="Christopher Nordike", Email="cnordike@students.kennesaw.edu"},
            new ApplicationUser{ UserName="Elizabeth Bever", Email="ebevers@students.kennesaw.edu"},
            new ApplicationUser{ UserName="Brandon Bell", Email="bbell31@students.kennesaw.edu"},
            };
            accounts.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            accounts.ForEach(s => UserManager.CreateAsync(s));

            var courses = new List<Course> {
            new Course{Subject="CHEML", CourseNumber=3500, Title="Biochemistry Lab", Section="01", StartDate=DateTime.Parse("01/05/2015"), EndDate=DateTime.Parse("05/01/2015"), Location=Campus.Kennesaw, Term=Terms.Spring, CreatedById = accounts[0].Id},
            new Course{Subject="CHEML", CourseNumber=3500, Title="Lab Project", Section="01", StartDate=DateTime.Parse("01/05/2015"), EndDate=DateTime.Parse("05/01/2015"), Location=Campus.Kennesaw, Term=Terms.Spring, CreatedById = accounts[1].Id},
            new Course{Subject="CS", CourseNumber=4850, Title="Senior Project", Section="01", StartDate=DateTime.Parse("01/05/2015"), EndDate=DateTime.Parse("05/01/2015"), Location=Campus.Kennesaw, Term=Terms.Spring, CreatedById = accounts[2].Id},
            new Course{Subject="Math", CourseNumber=3500, Title="Adding 101", Section="01", StartDate=DateTime.Parse("01/05/2015"), EndDate=DateTime.Parse("05/01/2015"), Location=Campus.Kennesaw, Term=Terms.Spring, CreatedById = accounts[3].Id},
            new Course{Subject="CS", CourseNumber=3500, Title="It MVC with C#", Section="01", StartDate=DateTime.Parse("01/05/2015"), EndDate=DateTime.Parse("05/01/2015"), Location=Campus.Kennesaw, Term=Terms.Spring, CreatedById = accounts[4].Id}
            };

            var _UserRelationship = new List<UserRelationship> {
            new UserRelationship() { AccountId = courses[0].CreatedById, AccountRole = Database.Helpers.CourseRole.owner },
            new UserRelationship() { AccountId = courses[1].CreatedById, AccountRole = Database.Helpers.CourseRole.owner },
            new UserRelationship() { AccountId = courses[2].CreatedById, AccountRole = Database.Helpers.CourseRole.owner },
            new UserRelationship() { AccountId = courses[3].CreatedById, AccountRole = Database.Helpers.CourseRole.owner },
            new UserRelationship() { AccountId = courses[4].CreatedById, AccountRole = Database.Helpers.CourseRole.owner }
        };
            _UserRelationship.ForEach(s => context.UserRelationships.Add(s));
            context.SaveChanges();
            for(int i = 0; i < courses.Count; i++)
            {
                var AllUsers = new List<UserRelationship>();
                accounts.ForEach( obj => AllUsers.Add(new UserRelationship(){ AccountId = obj.Id, AccountRole = CourseRole.student}));
                AllUsers.Add(_UserRelationship[i]);

                courses[i].UserRelationships.AddRange(AllUsers); context.SaveChanges();
            }
            
            
*/            
            //accounts.ForEach(s => s.Courses.AddRange(context.UserRelationships.Where(obj => obj.Account.Id.Equals(s.Id)).Select(obj => obj.Course).ToList()));
//            context.SaveChanges();
//            context.Courses.AddRange(courses);
            //courses.ForEach(s => context.Courses.Add(s));
/*            context.SaveChanges();
context.Users.ToList().ForEach(
                u =>
                    u.Courses.AddRange(context.UserRelationships.ToList().Where(obj => obj.Account.Id.Equals(u.Id)).Select(o2 => o2.Course))

                ); context.SaveChanges();
 * */
            //courses.ForEach(c => c.UserRelationships)
            /*
            var roles = new List<ClassRole> {
            new ClassRole{ AccountId=1, ClassroomId=1, AccountRole=Role.professor, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new ClassRole{ AccountId=2, ClassroomId=1, AccountRole=Role.student, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new ClassRole{ AccountId=3, ClassroomId=1, AccountRole=Role.student, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new ClassRole{ AccountId=4, ClassroomId=1, AccountRole=Role.nonStudentRole, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new ClassRole{ AccountId=5, ClassroomId=1, AccountRole=Role.professor, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            };
            var options = new List<CourseOption> {
            new CourseOption{Id=1, YoutubeLive=true, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now}
            };
            
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            //accounts.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();
            roles.ForEach(s => context.ClassRoles.Add(s));
            context.SaveChanges();
            options.ForEach(s => context.CourseOptions.Add(s));
*/
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
