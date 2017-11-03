using LibraryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace LibraryManagement.BAL
{
    public class LibraryManager
    {
        public bool CheckUser(LoginViewModel user)
        {
            LibraryFunctioning libraryfunctions = new LibraryFunctioning();
            Employees employee = new Employees();
            employee.EmployeeName = user.UserName;
            employee.Password = user.Password;
            bool isallowed = libraryfunctions.CheckUser(employee);
            return isallowed;
        }

        public string GetRole(string employeeName)
        {
            LibraryFunctioning getrole = new LibraryFunctioning();
            return (getrole.GetRole(employeeName));
        }
    }
}
