using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class BookDetails
    {
        [Key]
        [Required(ErrorMessage = "Please provide book name", AllowEmptyStrings = false)]
        public string BookName { get; set; }
        public string Author { get; set; }

        //Foreign Key to BookID
        [ForeignKey("book")]
        public int BookID { get; set; }
        public Books book { get; set; }
    }
}
