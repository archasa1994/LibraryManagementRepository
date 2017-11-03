using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class ShelfCategory
    {
        [Key, Column(Order = 1), ForeignKey("category")]
        public int CategoryID { get; set; }
        [Key, Column(Order = 2), ForeignKey("shelf")]
        public int ShelfID { get; set; }
        public BookCategories category { get; set; }
        public ShelfDetails shelf { get; set; }
    }
}
