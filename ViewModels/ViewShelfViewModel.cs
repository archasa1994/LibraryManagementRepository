using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels
{
    public class ViewShelfViewModel
    {
        public List<SelectListItem> Category { get; set; }
        public int SelectedCategory { get; set; }
    }
}
