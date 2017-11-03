using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class ShelfDetails
    {
        [Key]
        [Required]
        public int ShelfID { get; set; }
        public int ShelfCapacity { get; set; }

        public ICollection<Books> books { get; set; }
    }
}
