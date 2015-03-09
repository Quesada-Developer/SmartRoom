using SmartRoom.Database.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRoom.Database.Tables
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 2)]
        public string Subject { get; set; }
        [Required]
        public int CourseNumber { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2)]
        public String Section { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Campus Location { get; set; }
        [Required]
        public Terms Term { get; set; }

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

        public virtual ICollection<ClassRole> ClassRoles { get; set; }

        public CourseOption CourseOptions { get; set; }
    }
}