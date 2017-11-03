using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class BookCategories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public ICollection<Books> books { get; set; }
    }
}
