using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartRoom.Web.Areas.YouTube.Models
{
    public class Model
    {
        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; }
    }
}