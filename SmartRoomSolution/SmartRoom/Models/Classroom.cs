using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRoom.Models
{

    public enum Term
    {
        Spring, MayMester, Summer, Fall
    }
    public enum Campus
    {
        KSU, PSU
    }
    public class Classroom
    {
        public int Id { get; set; }
        public Term Term { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public int Year { get; set; }
        public Campus Campus { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ClassroomRole> ClassroomRoles { get; set; }
        public virtual ClassroomOptions ClassroomOption { get; set; }
    }
}