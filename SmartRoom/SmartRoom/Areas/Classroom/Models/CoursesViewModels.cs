using SmartRoom.Web.App_Start;
using SmartRoom.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartRoom.Web.Areas.Classroom.Models
{
    public class CoursesViewModels
    {
    }
    public class AddUserToCourse
    {
        public int CourseId { get; set; }
        [Required]
        public string AccountId { get; set; }
        public virtual ApplicationUser Account { get; set; }
        [Required]
        public CourseRole? AccountRole { get; set; }

    }
    public class RemoveUserFromCourse
    {
        public int CourseId { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string RequestorAccountId { get; set; }
    }
}