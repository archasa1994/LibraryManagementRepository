using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class LibraryFunctioning
    {
        public bool CheckUser(Employees employee)
        {
            bool isallowed;
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                isallowed = entity.Employee.Any(m => m.EmployeeName == employee.EmployeeName && m.Password == employee.Password);
            }
            return isallowed;
        }

        public string GetRole(string employeeName)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                Employees employee = entity.Employee.SingleOrDefault(m => m.EmployeeName == employeeName);
                return employee.Roles;
            }
        }
    }
}
