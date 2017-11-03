using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels
{
    public class AddNewEmployeeViewModel
    {
        [Required(ErrorMessage = "Please provide Employee Name", AllowEmptyStrings = false)]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please provide Employee Role", AllowEmptyStrings = false)]
        public List<SelectListItem> Role { get; set; }        
        public int SelectedRole { get; set; }
    }
}
