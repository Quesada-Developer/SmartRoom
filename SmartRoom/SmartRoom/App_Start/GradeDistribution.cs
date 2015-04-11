using SmartRoom.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SmartRoom.Web.App_Start
{
    public class GradeDistribution
    {
        [Key]
        public int Id { get; set; }

        public int SyllabiId { get; set; }
        public virtual Syllabus Syllabi { get; set; }

        public LetterGrade Letter { get; set; }
        public double TopPercentage { get; set; }
        public double LowerPercentage { get; set; }
    }
}