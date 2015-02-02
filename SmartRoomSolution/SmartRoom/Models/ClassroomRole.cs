using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRoom.Models
{
    public enum Role
    {
        Teacher, Student
    }

    public class ClassroomRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassroomId { get; set; }
        public Role Role { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual User User { get; set; }
        public virtual Classroom Classroom { get; set; }
    }
}