//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartRoomSolution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClassroomRoles
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ClassroomId { get; set; }
        public string Role { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Person People { get; set; }
        public virtual Classroom Classroom { get; set; }
    }
}
