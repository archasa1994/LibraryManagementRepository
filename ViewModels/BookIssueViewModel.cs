using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels
{
    public class BookIssueViewModel
    {
        public List<SelectListItem> UserID { get; set; }

        public List<SelectListItem> BookID { get; set; }
        [Required(ErrorMessage = "Please select Issue Date", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        public DateTime  IssueDate { get; set; }       
        public int SelectedUserID { get; set; }
        public int SelectedBookID { get; set; }

    }
}
