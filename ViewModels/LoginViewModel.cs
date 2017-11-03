using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please provide User Name", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
    }
}
