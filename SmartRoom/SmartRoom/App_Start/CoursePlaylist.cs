using SmartRoom.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SmartRoom.Web.App_Start
{
    public class CoursePlaylist
    {

        private readonly SmartModel db = new SmartModel();

        public CoursePlaylist() {

            CreateDate = DateTime.Now;
            UserRelationships = new List<UserRelationship>();

        }

        [Key]
        public int CoursePlaylistId { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [Display(Name = "Course")]
        public virtual Course Course { get; set; }
        [Required]
        
        public String PlaylistId { get; set; }
        [Required]
      
        [Display(Name = "Created By")]
        public string CreatedById { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        [Required]
        [Display(Name = "Created")]
        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Modified Last By")]
        public string ModifedById { get; set; }
        public virtual ApplicationUser ModifedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        public virtual List<UserRelationship> UserRelationships { get; set; }

    }
}