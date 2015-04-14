using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web.App_Start
{
    public class Syllabus
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public string ProfessorId { get; set; }
        public virtual ApplicationUser Professor { get; set; }

        public string OfficeLocation { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string OfficePhone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string EmergencyPhone { get; set; }
        [DataType(DataType.EmailAddress)]
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
        public virtual List<ClassDate> ClassDates { get; set; }

        public bool isActive { get; set; }
    }
}