using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRoom.Models
{
    public class ClassroomOptions
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Classroom Classroom { get; set; }
    }
}