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

        public string officeLocation { get; set; }
        public string officePhone { get; set; }
        public string cellPhone { get; set; }
        public string emergencyPhone { get; set; }
        public string emailAddress { get; set; }

        public List<DayOfWeek> meetingDays { get; set; }
        public DateTime meetingTime { get; set; }


    }
}