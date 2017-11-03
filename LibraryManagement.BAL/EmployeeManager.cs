using LibraryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace LibraryManagement.BAL
{
    public class EmployeeManager
    {
        public EmployeeListViewModel GetEmployees()
        {
            EmployeeDataManipulation manipulate = new EmployeeDataManipulation();
            List<Employees> employees = manipulate.GetEmployees();
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            foreach (var employee in employees)
            {
                EmployeeViewModel employeeModel = new EmployeeViewModel();
                employeeModel.EmployeeID = employee.EmployeeID;
                employeeModel.EmployeeName = employee.EmployeeName;
                employeeModel.Roles = employee.Roles;
                employeeModel.Email = employee.Email;
                employeeList.Add(employeeModel);
            }
            employeeListViewModel.employeeList = employeeList;
            return employeeListViewModel;
        }

        public AddNewEmployeeViewModel NewEmployee()
        {
            EmployeeDataManipulation manipulate = new EmployeeDataManipulation();
            AddNewEmployeeViewModel newEmployee = new AddNewEmployeeViewModel();
            newEmployee.Role = manipulate.GetRoleList();
            return newEmployee;
        }

        public bool CheckIfUserAlreadyPresent(string userName)
        {
            EmployeeDataManipulation manipulate = new EmployeeDataManipulation();
            return (manipulate.CheckIfUserAlreadyPresent(userName));
        }

        public string AddNewEmployee(AddNewEmployeeViewModel newemployee)
        {

            EmployeeDataManipulation manipulate = new EmployeeDataManipulation();
            Employees employee = new Employees();
            employee.EmployeeName = newemployee.EmployeeName;
            employee.Email = newemployee.Email;
            if (newemployee.SelectedRole == 1)
            {
                employee.Roles = "DptHead";
            }
            else if (newemployee.SelectedRole == 2)
            {
                employee.Roles = "SenLibrarian";
            }
            else if (newemployee.SelectedRole == 3)
            {
                employee.Roles = "JunLibrarian";
            }
            employee.Password = newemployee.Password;
            return(manipulate.AddNewEmployee(employee));
        }

        public string DeleteEmployee(int employeeID)
        {
            EmployeeDataManipulation manipulate = new EmployeeDataManipulation();
            return (manipulate.DeleteEmployee(employeeID));            
        }
    }
}
