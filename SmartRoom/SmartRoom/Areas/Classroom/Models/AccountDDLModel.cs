using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Classroom.Models
{
    public class AccountDDLModel
    {
        [Display(Name = "Select User")]
        public int SelectedUser { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }


    public class EnrollViewModel
    {
        [Required]
        [Display(Name = "RegistrationCode")]
        public string RegistrationCode { get; set; }
    }
}