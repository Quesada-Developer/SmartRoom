
using SmartRoom.Web.App_Start;
using SmartRoom.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SmartRoom.Web
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
        public string RegistrationCode { get; private set; }
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
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Campus Location { get; set; }
        [Required]
        public Terms Term { get; set; }
        

        public string CreatedById { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { get; set; }

        public string ModifedById { get; set; }
        public virtual ApplicationUser ModifedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }


        public int CourseOptionsId { get; set; }
        public virtual CourseOption CourseOptions { get; set; }
        public virtual ICollection<YoutubeLiveDetail> YoutubeLiveDetails { get; set; }

        public virtual List<UserRelationship> UserRelationships { get; set; }
    }
}