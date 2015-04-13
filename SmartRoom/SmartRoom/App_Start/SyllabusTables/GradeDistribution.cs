using SmartRoom.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web.App_Start
{
    public class GradeDistribution
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SyllabiId { get; set; }
        public virtual Syllabus Syllabi { get; set; }

        public LetterGrade Letter { get; set; }
        public double TopPercentage { get; set; }
        public double LowerPercentage { get; set; }
    }
}