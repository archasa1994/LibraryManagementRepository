using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DailyIssueReportListViewModel
    {
        public List<DailyIssueReportViewModel> dailyIssueList { get; set; }
        public DateTime Today { get; set; }
    }
}
