using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AddNewCategoryViewModel
    {
        [Required(ErrorMessage = "Please provide Cetegory Name", AllowEmptyStrings = false)]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please provide Shelf Capacity", AllowEmptyStrings = false)]
        public int ShelfCapacity { get; set; }
    }
}
