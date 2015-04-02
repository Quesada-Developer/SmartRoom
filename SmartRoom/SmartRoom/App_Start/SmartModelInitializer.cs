using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartRoom.Database.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SmartRoom.Web
{
    class SmartModelInitializer : DropCreateDatabaseIfModelChanges<SmartModel>
    {
        protected override void Seed(SmartModel context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
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


            /*var courses = new List<Course> {
            new Course{Subject="CHEML", CourseNumber=3500, Title="Biochemistry Lab", Section="01", StartDate=DateTime.Parse("01/05/2015"), EndDate=DateTime.Parse("05/01/2015"), Location=Campus.Kennesaw, Term=Terms.Spring},
            new Course{Subject="CS", CourseNumber=4850, Title="Senior Project", Section="01", StartDate=DateTime.Parse("01/05/2015"), EndDate=DateTime.Parse("05/01/2015"), Location=Campus.Kennesaw, Term=Terms.Spring}
            };
            var accounts = new List<Account> {
            new Account{ FirstName="Francisco", LastName="Quesada", Email="jquesada@students.kennesaw.edu", isActive=true, isAdmin=true, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new Account{ FirstName="Steven", LastName="Carver", Email="scarver6@students.kennesaw.edu", isActive=true, isAdmin=true, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new Account{ FirstName="Christopher", LastName="Nordike", Email="cnordike@students.kennesaw.edu", isActive=true, isAdmin=true, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new Account{ FirstName="Elizabeth", LastName="Bever", Email="ebevers@students.kennesaw.edu", isActive=true, isAdmin=true, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            new Account{ FirstName="Brandon", LastName="Bell", Email="bbell31@students.kennesaw.edu", isActive=true, isAdmin=true, CreatedBy=1, CreateDate=DateTime.Now, ModifedBy=1, ModifiedDate=DateTime.Now},
            };
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
