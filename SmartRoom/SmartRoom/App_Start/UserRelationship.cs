using SmartRoom.Database.Helpers;
using SmartRoom.Web.App_Start;
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
        public UserRelationship()
        {
            CreateDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        public virtual ApplicationUser Account { get; set; }
        public virtual Course Course { get; set; }
        [Required]
        public int AccountRoleId { get; set; }
        public CourseRole AccountRole { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { get; set; }


        public int ModifedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
    }
}