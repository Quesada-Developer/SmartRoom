using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web.App_Start
{
    public class Announcement
    {
        public Announcement()
        {
            CreateDate = DateTime.Now;
            IsActive = true;
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? PublishOn { get; set; }

        public string NewsTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string NewsText { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { get; set; }
        public string ModifedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}