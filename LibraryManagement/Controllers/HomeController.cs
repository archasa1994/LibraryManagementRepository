using LibraryManagement.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ViewModels;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult LoginRequired()
        {
            return View();
        }

        #region PDF GENERATION
        public ActionResult GeneratePDF()
        {
            return new Rotativa.ActionAsPdf("GetDailyIssualReport");
        }
        #endregion

        #region MANAGING EMPLOYEES
        //MANAGING EMPLOYEES-DEPARTMENT HEAD
        //[Authorize(Roles = "DptHead")]
        [CustomAuthorization(Roles = "DptHead")]
        public ActionResult ManageEmployees()
        {
            EmployeeManager manager = new EmployeeManager();
            return View(manager.GetEmployees());
        }
        #endregion

        #region ADDING NEW EMPLOYEE
        //ADDING AN EMPLOYEE
        //[Authorize(Roles = "DptHead")]
        [CustomAuthorization(Roles = "DptHead")]
        public ActionResult AddNewEmployee()
        {
            EmployeeManager manager = new EmployeeManager();
            return View(manager.NewEmployee());
        }

        [HttpPost]
        public ActionResult AddNewEmployee(AddNewEmployeeViewModel newEmployee)
        {
            EmployeeManager manager = new EmployeeManager();
            bool userPresent = manager.CheckIfUserAlreadyPresent(newEmployee.EmployeeName);
            if (userPresent)
            {
                return View("UserAlreadyPresent");
            }
            else
            {
                string result = manager.AddNewEmployee(newEmployee);
                if(result == "Success")
                {
                    return RedirectToAction("ManageEmployees", "Home");
                }
                else
                {
                    return View("ERROR");
                }
                
            }           
        }
        #endregion

        #region DELETING AN EMPLOYEE
        //DELETING AN EMPLOYEE    
        //[Authorize(Roles = "DptHead")]
        [CustomAuthorization(Roles = "DptHead")]
        public ActionResult DeleteEmployee(int employeeID, string employeeName)
        {
            DeleteEmployeeViewModel employee = new DeleteEmployeeViewModel();
            employee.EmployeeID = employeeID;
            employee.EmployeeName = employeeName;
            return PartialView("_deletemp", employee);
        }

        [HttpPost]
        public ActionResult DeleteEmployee(DeleteEmployeeViewModel employee)
        {
            EmployeeManager manager = new EmployeeManager();
            string result = manager.DeleteEmployee(employee.EmployeeID);
            if(result == "Success")
            {
                return RedirectToAction("ManageEmployees", "Home");
            }
            else
            {
                return View("ERROR");
            }            
        }
        #endregion

        #region MANAGING LOCKED USERS
        //MANAGING LOCKED USERS-DEPARTMENT HEAD
        //[Authorize(Roles = "DptHead")]
        [CustomAuthorization(Roles = "DptHead")]
        public ActionResult ManageLockedUsers()
        {
            UserManager manager = new UserManager();
            return View(manager.GetLockedUsers());
        }

        public ActionResult UnlockUser(int userID, string userName)
        {
            UserManager manager = new UserManager();
            manager.UnlockUser(userID, userName);
            return RedirectToAction("ManageLockedUsers", "Home");
        }
        #endregion

        #region DAILY REPORT WITH PDF CONVERSION
        //DAILY ISSUAL REPORT-DEPARTMENT HEAD/SENIOR LIBRARIAN
        //[Authorize(Roles = "DptHead,SenLibrarian")]
        [CustomAuthorization(Roles = "DptHead,SenLibrarian")]
        public ActionResult GetDailyIssualReport()
        {
            BookManager manager = new BookManager();
            return View(manager.GetDailyIssueReport());
        }

        #endregion

        #region MANAGING BOOKS
        //MANAGING BOOKS-SENIOR LIBRARIAN
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult ManageBooks()
        {
            BookManager manager = new BookManager();
            return View(manager.ManageBooks());
        }
        #endregion

        #region ADDING A BOOK
        //ADDING NEW BOOK
        [Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult AddNewBook()
        {
            BookManager manager = new BookManager();
            return View(manager.NewBook());
        }

        //SHELF DROP DOWN BINDING BASED ON CATEGORY
        public ActionResult GetShelfList(int ID)
        {
            BookManager manager = new BookManager();
            return Json(manager.GetShelfList(ID),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNewBook(AddNewBookViewModel newbook)
        {
            BookManager manager = new BookManager();
            manager.AddNewBook(newbook);
            return RedirectToAction("ManageBooks", "Home");
        }
        #endregion

        #region DELETING A BOOK
        //DELETING A BOOK
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult DeleteBook(string bookName, int bookID)
        {
            DeleteBookViewModel book = new DeleteBookViewModel();
            book.BookName = bookName;
            book.BookID = bookID;
            return View(book);
        }

        [HttpPost]
        public ActionResult DeleteBook(DeleteBookViewModel book)
        {
            BookManager manager = new BookManager();
            manager.DeleteBook(book);
            return RedirectToAction("ManageBooks", "Home");
        }
        #endregion

        #region MANAGING CATEGORIES
        //MANAGING CATEGORIES-SENIOR LIBRARIAN
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult ManageCategories()
        {
            BookManager manager = new BookManager();
            return View(manager.ManageCategories());
        }
        #endregion

        #region ADDING CATEGORY
        //ADDING NEW CATEGORY
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult AddNewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCategory(AddNewCategoryViewModel category)
        {
            BookManager manager = new BookManager();
            manager.AddNewCategory(category);
            return RedirectToAction("ManageCategories", "Home");
        }
        #endregion

        #region MANAGING SHELFS
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult ViewShelfs()
        {
            BookManager manager = new BookManager();
            return View(manager.GetCategoryList());
        }
        #endregion

        #region ADDING SHELF
        //ADDING NEW SHELF
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult AddNewShelf()
        {
            BookManager manager = new BookManager();
            return View(manager.NewShelf());
        }

        [HttpPost]
        public ActionResult AddNewShelf(AddNewShelfViewModel newshelf)
        {
            BookManager manager = new BookManager();
            manager.AddNewShelf(newshelf);
            return RedirectToAction("ManageShelfs", "Home");
        }
        #endregion

        #region MANAGING USERS
        //MANAGE USERS-SENIOR LIBRARIAN
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult ManageUsers()
        {
            UserManager manager = new UserManager();
            return View(manager.ManageUsers());
        }
        #endregion

        #region GET SINGLE USER DETAILS
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult GetUserDetails(int userID)
        {
            UserManager manager = new UserManager();
            return View(manager.GetUserDetails(userID));
        }
        #endregion

        #region DELETING USER
        //DELETE USERS-SENIOR LIBRARIAN
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult DeleteUser(int userID, string userName)
        {
            DeleteUserViewModel user = new DeleteUserViewModel();
            user.UserID = userID;
            user.UserName = userName;
            return View(user);
        }
        [HttpPost]
        public ActionResult DeleteUser(DeleteUserViewModel user)
        {
            UserManager manager = new UserManager();
            manager.DeleteUser(user);
            return RedirectToAction("ManageUsers", "Home");
        }
        #endregion

        #region ADDING NEW USER
        //ADDING USER-SENIOR LIBRARIAN
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult AddNewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewUser(AddNewUserViewModel newUser)
        {
            UserManager manager = new UserManager();
            manager.AddNewUser(newUser);
            return RedirectToAction("ManageUsers", "Home");
        }
        #endregion

        #region SENDING EMAIL TO USERS
        //SENDING EMAIL-SENIOR LIBRARIAN
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult SendEmail(int userID, string to)
        {
            UserManager manager = new UserManager();
            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
            email.To.Add(to);
            email.From = new MailAddress(WebConfigurationManager.AppSettings["emailid"]);
            email.Subject = "Fine for books Due";
            string Body = "You are supposed to pay a fine of Rs." + (manager.GetUserFine(userID).ToString());
            email.Body = Body;
            email.IsBodyHtml = true;
            email.Priority = System.Net.Mail.MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["emailid"], WebConfigurationManager.AppSettings["password"]);// Enter senders User name and password
            smtp.EnableSsl = true;
            smtp.Send(email);
            //return View();
            return RedirectToAction("HomePage", "Home");
        }
        #endregion

        #region FINE CALCULATION
        //FINE CALCULATION-SENIOR LIBRARIAN
        //[Authorize(Roles = "SenLibrarian")]
        [CustomAuthorization(Roles = "SenLibrarian")]
        public ActionResult CalculateFine()
        {
            UserManager manager = new UserManager();
            manager.CalculateFine();
            return RedirectToAction("ManageUsers", "Home");
        }
        #endregion

        #region SENDING EMAIL TO DEPARTMENT HEAD
        public ActionResult SendMailToDH()
        {
            UserManager manager = new UserManager();
            return View(manager.GetUsers());
        }
        #endregion

        #region BOOK ISSUAL
        //BOOK ISSUAL-JUNIOR LIBRARIAN
        [Authorize(Roles = "JunLibrarian")]
        public ActionResult BookIssual()
        {
            BookManager manager = new BookManager();
            return View(manager.BookIssual());
        }

        [HttpPost]
        public ActionResult BookIssual(BookIssueViewModel bookIssued)
        {
            BookManager manager = new BookManager();
            if(manager.EnterBookIssued(bookIssued) == "success")
            {
                return RedirectToAction("BookIssual", "Home");
            }
            else
            {
                return View("ERROR");
            }
            
        }
        #endregion       
    }
}