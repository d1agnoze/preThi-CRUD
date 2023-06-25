using preThi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace preThi.Controllers
{
    public class AccountController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext(global::System.Configuration.ConfigurationManager.ConnectionStrings["demo_sqlsvConnectionString"].ConnectionString);
        // GET: Account
        public ActionResult Login()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
           
 /*         var password = Request.Form["password"];
            var sdt = Request.Form["sdt"];
            sdt = sdt.Trim();
            password = password.Trim();*/
            if (ModelState.IsValid == true)
            {   
                if (AuthenticateUser(user.SDT, user.Password))
                {   
                    FormsAuthentication.SetAuthCookie(user.SDT.ToString(), false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }

            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList();

            // Log or display the validation errors
            ViewBag.Errors = errors;
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private bool AuthenticateUser(int sdt, string password)
        {
          
            if (string.IsNullOrEmpty(password) || sdt == null)
            {
                Session["Err"] = "sumthing went rong";
                return false;
            }
            var findUser = db.tbl_NguoiDungs.First(user => user.SDT == sdt);
            if (findUser == null)
            {
                Session["Error"] = "Something went wrong";
                return false;
            }
            if (findUser.Password == password)
            {
                Session["User"] = findUser.SDT;
                return true;
            }
            return false;
        }
    }
}