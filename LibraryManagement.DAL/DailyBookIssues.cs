using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class DailyBookIssues
    {
        [Key, Column(Order = 1) , ForeignKey("books")]
        public int UserID { get; set; }
        [Key, Column(Order = 2), ForeignKey("users")]
        public int BookID { get; set; }
        [Key, Column(Order = 3)]
        public DateTime IssueDate { get; set; }
        public bool IsFine { get; set; }
        public decimal fine { get; set; }
        public bool Returned { get; set; }
        public DateTime ReturnDate { get; set; }
        public Books books { get; set; }
        public Users users { get; set; }
    }
}
