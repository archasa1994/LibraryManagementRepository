using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal Fine { get; set; }
        public bool IsActive { get; set; }        

    }
}
