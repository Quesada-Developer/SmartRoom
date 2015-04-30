
using SmartRoom.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SmartRoom.Web.App_Start
{
    public class Course
    {
        
        private readonly SmartModel db = new SmartModel();
        public Course()
        {
            CreateDate = DateTime.Now;
            UserRelationships = new List<UserRelationship>();
            CreateRegistrationCode();
        }


        public void CreateRegistrationCode()
        {
            //No I,L,O,0,1 look to much the same
            var chars = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            if (db.Courses.Any(obj => obj.RegistrationCode.Equals(result)))
                CreateRegistrationCode();
            else
                RegistrationCode = result;
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Registration Code")]
        public string RegistrationCode { get; private set; }
        [Required]
        [StringLength(8, MinimumLength = 2)]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Course Number")]
        public int CourseNumber { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2)]
        public String Section { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }
        [Required]
        public Campus Location { get; set; }
        [Required]
        public Terms Term { get; set; }
        [Required]
        public bool isActive { get; set; }
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


        public int CourseOptionsId { get; set; }
        public virtual CourseOption CourseOptions { get; set; }
        public virtual ICollection<YoutubeLiveDetail> YoutubeLiveDetails { get; set; }
        public virtual ICollection<CoursePlaylist> CoursePlaylist { get; set; }
        public virtual List<Announcement> Announcements { get; set; }
        public virtual List<UserRelationship> UserRelationships { get; set; }
        public virtual ICollection<Syllabus> Syllabi { get; set; }
    }
}