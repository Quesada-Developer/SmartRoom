using SmartRoom.Web.App_Start;
using System.Collections.Generic;

namespace SmartRoom.Web.Areas.Forms.Models
{
    public class CompleteSyllabusModel
    {
        public Syllabus syllabus { get; set; }
        public IEnumerable<GradeDistribution> gradeDistribution { get; set; }
        public IEnumerable<ClassDate> classDate { get; set; }
        public IEnumerable<Information> additionalInformation { get; set; }
    }
}