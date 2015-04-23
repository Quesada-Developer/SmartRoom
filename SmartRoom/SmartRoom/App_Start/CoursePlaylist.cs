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
        [Display(Name = "Playlist Name")]
        public String PlaylistName { get; set; }
      

        public virtual List<UserRelationship> UserRelationships { get; set; }

    }
}