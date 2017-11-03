using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels
{
    public class AddNewBookViewModel
    {       
        public List<SelectListItem> CategoryID { get; set; }
        public List<SelectListItem> ShelfID { get; set; }
        [Required(ErrorMessage = "Please provide Book Name", AllowEmptyStrings = false)]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Please provide Author", AllowEmptyStrings = false)]
        public string Author { get; set; }
        [Required(ErrorMessage = "Please select a category", AllowEmptyStrings = false)]
        public int SelectedCategory { get; set; }
        [Required(ErrorMessage = "Please select a shelf", AllowEmptyStrings = false)]
        public int SelectedShelf { get; set; }
    }
}
