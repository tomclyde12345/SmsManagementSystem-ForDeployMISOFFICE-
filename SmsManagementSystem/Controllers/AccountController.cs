using SmsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Scrypt;
namespace SmsManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        ilsEntities Db = new ilsEntities();

        public ActionResult Login()
        {
            return View();
            ViewBag.Message = "";
        }
        [HttpPost]
        public ActionResult Login(Login log)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            var result = Db.Logins.SingleOrDefault(a => a.Username == log.Username);



          

            if (result != null)
            {
                if(!encoder.Compare(log.Password, result.Password))
                {
                    ViewBag.Message = "Incorrect Username or Password";
                    return View(log);
                }

                Session["LoginID"] = result.LoginID;
                Session["Role_Id"] = result.RoleID;
                FormsAuthentication.SetAuthCookie(result.Username, false);

                //IF admin
                if (result.RoleID == 1)
                {
                    TempData["Message"] = "WELCOME ADMINISTRATOR";
                    return RedirectToAction("Index", "Admin");

                }
             
                //IF USER
                if (result.RoleID == 2)
                {
                    TempData["Message"] = "WELCOME USER";
                    return RedirectToAction("Index", "User");
                }

            }
            else
            {
                ViewBag.Message = "Incorrect Username or Password";
            }

            return View(log);
        }
        public ActionResult Logout()
        {
            Session["LoginID"] = null;
            Session["Role_Id"] = null;
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }

    }


}
