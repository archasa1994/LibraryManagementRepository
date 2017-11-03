using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class Employees
    {
        [Key]
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Roles { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
