using SmartRoom.Web.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Classroom.Models
{
    public class AccountDDLModel
    {
        [Display(Name = "Select User")]
        public int SelectedUser { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}