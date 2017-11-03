using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels
{
    public class AddNewShelfViewModel
    {

        public List<SelectListItem> Category { get; set; }
        [Required(ErrorMessage = "Please Select Category", AllowEmptyStrings = false)]
        public int SelectedCategory { get; set; }
        [Required(ErrorMessage = "Please provide Shelf capacity", AllowEmptyStrings = false)]
        public int ShelfCapacity { get; set; }
    }
}
