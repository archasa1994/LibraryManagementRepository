using LibraryManagement.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewModels;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel user, string returnUrl)
        {
            LibraryManager manager = new LibraryManager();

            if (ModelState.IsValid)
            {
                bool uservalid = manager.CheckUser(user);

                if (uservalid)
                {

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    if (Url.IsLocalUrl(returnUrl))
                    {

                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {

                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }

            }
            return View(user);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage", "Home");
        }
    }
}