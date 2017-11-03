using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserDetailsViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal Fine { get; set; }
        public List<UserBookDetails> BookList { get; set; }
    }
}
