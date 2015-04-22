
using SmartRoom.Web.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web.App_Start
{
    public class UserRelationship
    {
        public UserRelationship()
        {
            CreateDate = DateTime.Now;
            IsActive = true;
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
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { get; set; }


        public string ModifedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}