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
        public string OfficePhone { get; set; }
        public string CellPhone { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmailAddress { get; set; }

        public List<DayOfWeek> MeetingDays { get; set; }
        public DateTime MeetingTime { get; set; }

        public string GeneralInformation { get; set; }
        public string CourseDescription { get; set; }
        public string AttendancePolicy { get; set; }
        public string AssignmentInformation { get; set; }
        public string AcademicIntegrityStatement { get; set; }
        public List<string> AdditionalInformation { get; set; }

        public List<string> RequiredMaterial { get; set; }
        public List<GradeDistribution> GradeDistributions { get; set; }
        public List<ClassDate> ClassDates { get; set; }
    }
}