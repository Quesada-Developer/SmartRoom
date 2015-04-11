using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web.App_Start
{
    public class CourseOption
    {
        [Key]
        public int Id { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        public bool YoutubeLive { get; set; }

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