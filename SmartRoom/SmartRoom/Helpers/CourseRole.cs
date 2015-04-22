
using System.ComponentModel.DataAnnotations;
namespace SmartRoom.Web.Helpers
{

    //NotAuthorized is used in methods as a return value so thrrows are not used
    public enum CourseRole
    {
        [Display(Name = "Owner")]
        owner = 0,
        [Display(Name = "Professor")]
        professor = 1,
        [Display(Name = "Non-Student")]
        nonStudentRole = 2,
        [Display(Name = "Student")]
        student = 3,
        [Display(Name = "Visitor ")]
        visitor = 4,
        [Display(Name = "Not Authorized")]
        NotAuthorized = 404 
    }
}