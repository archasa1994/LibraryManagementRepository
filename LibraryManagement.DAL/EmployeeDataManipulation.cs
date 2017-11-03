using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraryManagement.DAL
{
    public class EmployeeDataManipulation
    {
        //DISPLAYING EMPLOYEEES
        public List<Employees> GetEmployees()
        {
            var employees = new List<Employees>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                employees = entity.Employee.ToList();
            }
            return employees;
        }

        //DROP DOWN BINDING OF ROLE 
        public List<SelectListItem> GetRoleList()
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem
            {
                Text = "Department Head",
                //Value = "DptHead"
                Value = "1"
            });
            roleList.Add(new SelectListItem
            {
                Text = "Senior Librarian",
                //Value = "SenLibrarian"
                Value = "2"
            });
            roleList.Add(new SelectListItem
            {
                Text = "Junior Librarian",
                //Value = "JunLibrarian"
                Value = "3"
            });

            return roleList;
        }

        //CHECKING IF USER ALREADY PRESENT
        public bool CheckIfUserAlreadyPresent(string userName)
        {
            bool isPresent;
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var user = entity.Employee.SingleOrDefault(m => m.EmployeeName == userName);
                if (user == null)
                {
                    isPresent = false;
                }
                else
                {
                    isPresent = true;
                }
            }
            return isPresent;
        }

        //ADDING NEW EMPLOYEE
        public string AddNewEmployee(Employees newEmployee)
        { 
            try
            {
                using (LibraryDatabase entity = new LibraryDatabase())
                {
                    entity.Employee.Add(newEmployee);
                    entity.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            return "Success";
          
        }

        //DELETING AN EMPLOYEE
        public string DeleteEmployee(int employeeID)
        {
            try
            {
                using (LibraryDatabase entity = new LibraryDatabase())
                {
                    var employee = entity.Employee.Single(s => s.EmployeeID == employeeID);
                    entity.Employee.Remove(employee);
                    entity.SaveChanges();

                }               
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            return "Success";
    }
  }
}
