using SmartRoom.Database.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRoom.Web
{
    public class UserRelationship
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        public virtual ApplicationUser Accounts { get; set; }
        [Required]
        public int ClassroomId { get; set; }
        public virtual Course Classrooms { get; set; }
        [Required]
        public Role AccountRole { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { get; set; }
        [Required]
        public int ModifedBy { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
    }
}