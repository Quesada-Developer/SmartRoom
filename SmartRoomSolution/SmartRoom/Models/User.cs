using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRoom.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ClassroomRole> ClassroomRoles { get; set; }
    }
}