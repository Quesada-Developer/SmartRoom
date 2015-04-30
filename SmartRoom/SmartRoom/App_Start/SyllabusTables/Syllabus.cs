using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web.App_Start
{
    public class Syllabus
    {

        public Syllabus()
        {
            AdditionalInformation = new List<Information>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Course Name")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Display(Name = "Professir Name")]
        public string ProfessorId { get; set; }
        public virtual ApplicationUser Professor { get; set; }

        [Display(Name = "Office Location")]
        public string OfficeLocation { get; set; }
        [Display(Name = "Office Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4}).*$", ErrorMessage = "Not a valid number")]
        public string OfficePhone { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4}).*$", ErrorMessage = "Not a valid number")]
        [Display(Name = "Personal Cell Number")]
        public string CellPhone { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4}).*$", ErrorMessage = "Not a valid number")]
        [Display(Name = "Emergency Phone Number")]
        public string EmergencyPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        [DataType(DataType.Time)]
        [Display(Name = "Class Meeting Time")]
        public DateTime MeetingTime { get; set; }

        //[DataType(DataType.MultilineText)]
        //public string GeneralInformation { get; set; }
        //[DataType(DataType.MultilineText)]
        //public string CourseDescription { get; set; }
        //[DataType(DataType.MultilineText)]
        //public string AttendancePolicy { get; set; }
        //[DataType(DataType.MultilineText)]
        //public string AssignmentInformation { get; set; }
        //[DataType(DataType.MultilineText)]
        //public string AcademicIntegrityStatement { get; set; }

        public virtual List<Information> AdditionalInformation { get; set; }
        public virtual List<GradeDistribution> GradeDistributions { get; set; }
        //public virtual List<ClassDate> ClassDates { get; set; }

        public bool isActive { get; set; }
    }
}