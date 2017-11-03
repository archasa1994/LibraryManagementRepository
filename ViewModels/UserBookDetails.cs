using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserBookDetails
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public decimal Fine { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
