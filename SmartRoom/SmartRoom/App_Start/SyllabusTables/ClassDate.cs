using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRoom.Web.App_Start
{
    public class ClassDate
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string LectureTopic { get; set; }
        public string LectureInformation { get; set; }

        public bool isActive { get; set; }

        public int InfoId { get; set; }
        public virtual AdditionalInformation Info{ get; set; }

    }
}