using System.Collections.Generic;

namespace SmartRoom.Web.Models
{
    public class DashBoard
    {
        public string LastLogin { get; set; }
        
    }

    public class TeacherDashBoard : DashBoard
    {
        public List<Course> CoursesTeaching { get; set; }
    }

    public class StudentDashBoard : DashBoard
    {
        public List<Course> EnrolldedCourses { get; set; }
    }
}