using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class Books
    {
        [Key]
        [Required]
        public int BookID { get; set; }

        //Foreign Key To BookCategories
        [ForeignKey("bookcategory")]
        public int CategoryID { get; set; }
        //Foreign Key to Shelf 
        [ForeignKey("shelfdetails")]
        public int ShelfID { get; set; }
        public BookCategories bookcategory { get; set; }
        public ShelfDetails shelfdetails { get; set; }       

    }
}
