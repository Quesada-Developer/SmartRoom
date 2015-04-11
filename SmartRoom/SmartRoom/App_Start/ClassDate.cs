using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartRoom.Web.App_Start
{
    public class ClassDate
    {
        [Key]
        public int Id { get; set; }

        public int SyllabiId{get;set;}
        public virtual Syllabus Syllabi{get;set;}

        public DateTime Date { get; set; }
        public string LectureTopic { get; set; }
        public string LectureInformation { get; set; }
        public string AdditionalInformation { get; set; }
    }
}