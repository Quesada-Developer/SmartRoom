using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartRoom.Web.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartRoom.Web.App_Start
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Courses = new List<Course>();
        }
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

        public virtual List<Course> Courses { get; set; }
        public virtual List<Syllabus> Syllabi { get; set; }
        public IEnumerable<Course> CoursesByRole(CourseRole Role)
        {
            return Courses.Where(obj => obj.UserRelationships.All(obj2 => obj2.AccountId.Equals(Id)));
        }

        /// <summary>
        /// Returns the <see cref="CourseRole"/> for the Course sent. If user can not access the course returned is <see cref="CourseRole.NotAuthorized"/>
        /// </summary>
        /// <param name="Course"></param>
        /// <returns>Returns CourseRole for Course or <see cref="CourseRole.NotAuthorized"/></returns>
        public CourseRole RoleFromCourse(Course Course)
        {
            var list = Courses[Course.Id].UserRelationships.Where(obj => obj.AccountId == Id).ToList();
            if (list != null && list.Count > 0)
                return list.FirstOrDefault().AccountRole;
            return CourseRole.NotAuthorized;
        }
    }
}