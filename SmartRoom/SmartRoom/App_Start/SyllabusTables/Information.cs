using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRoom.Web.App_Start
{
    public class Information
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string InformationTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string InformationText { get; set; }
    }
}