using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AddNewUserViewModel
    {
        [Required(ErrorMessage = "Please provide User Name", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please provide Email", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
