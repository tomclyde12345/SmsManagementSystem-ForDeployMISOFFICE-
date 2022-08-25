using SmsManagementSystem.Models;
using SmsManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;


namespace SmsManagementSystem.Controllers
{

    public class ManageController : Controller
    {

        // GET: Manage
        ilsEntities Db = new ilsEntities();


        public string Index()
        {

            return "HELLO";
        }

        public ActionResult UserRegistration()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            ViewBag.Offices = Db.CgppOffices.ToList();
            ViewBag.Division = Db.CgppDivisions.ToList();


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            {
                List<CgppOffice> OfficeList = Db.CgppOffices.ToList();
                ViewBag.Officelist = new SelectList(OfficeList, "Id", "Name");
            }
            return View();
        }

        public ActionResult GetDivisionList(int OfficeId)
        {

            Db.Configuration.ProxyCreationEnabled = false;
            List<CgppDivision> DivisionList = Db.CgppDivisions.Where(x => x.OfficeId == OfficeId).ToList();
            return Json(DivisionList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDivisionsList(int OfficeId)
        {

            Db.Configuration.ProxyCreationEnabled = false;
            List<CgppDivision> DivisionList = Db.CgppDivisions.Where(x => x.OfficeId == OfficeId).ToList();
            return Json(DivisionList, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult UserRegistration(User userdet)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            if ((int)Session["Role_Id"] != 1)
            {
                return RedirectToAction("logout", "Account");
            }
            if (ModelState.IsValid)
            {
                if (userdet.UserID == 0)
                {
                    Login log = new Login();
                    log.Username = userdet.UserName;
                    log.Password = encoder.Encode(userdet.Password);
                    log.RoleID = 2;
                    log.Isdeleted = false;
                    log.CreateOn = DateTime.Today.Date;
                    Db.Logins.Add(log);
                    userdet.LoginID = log.LoginID;//Db.Logins.Max(a => a.LoginID);
                    Db.Users.Add(userdet);
                    userdet.DateAdded = DateTime.Now;
                    userdet.FilePath = "/DefaultImage/city-hall.png";
                  
                }


                else
                {
                    var usersInDb = Db.Users.Single(c => c.UserID == userdet.UserID);
                    usersInDb.Name = userdet.Name;
                    usersInDb.DateAdded = DateTime.Now;
                    usersInDb.EmailID = userdet.EmailID;
                    usersInDb.CgppOffice.Name = userdet.CgppOffice.Name;
                    usersInDb.CgppDivision.Name = userdet.CgppDivision.Name;


            
                }
               
                Db.SaveChanges();

                TempData["Message"] = "SAVE SUCCESSFULLY";

                return RedirectToAction("UsersList", "Manage");
            }


            return View(userdet);
        }

        public JsonResult IsUsernameAvailble(string UserName)
        {
            return Json(!Db.Logins.Any(x => x.Username == UserName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UsersList()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var users = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;
            if ((int)Session["Role_Id"] != 1)
            {
                users = users.Where(d => d.UserID == userID).ToList();
            }

            return View(users);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var users = Db.Users.SingleOrDefault(c => c.UserID == id);
            if (users == null)
                return HttpNotFound();
            var viewModel = new ManageVM()
            {
                OfficeList = Db.CgppOffices.ToList(),
                DivisionList = Db.CgppDivisions.ToList(),
                

                Officeid = users.OfficeID.HasValue ? users.OfficeID.Value : 0,
                Divisionid = users.DivisionID.HasValue ? users.DivisionID.Value : 0,
                Username = users.Login.Username,
                Name = users.Name,
                EmailAddress = users.EmailID,
                Password = users.Login.Password,
                UserID = id,
                FilePath = users.FilePath,

            };
            TempData["Message"] = "SAVE SUCCESSFULLY";
            return View("Edit", viewModel);

        }
        [HttpPost]
        public ActionResult Edit(ManageVM userdet)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (ModelState.IsValid)
            {
                var usersInDb = Db.Users.Single(c => c.UserID == userdet.UserID);
                var loginDb = Db.Logins.Find(usersInDb.LoginID);
                usersInDb.Name = userdet.Name;
                usersInDb.DateAdded = DateTime.Now;
                usersInDb.EmailID = userdet.EmailAddress;
                usersInDb.CgppOffice = Db.CgppOffices.Find(userdet.Officeid);
                usersInDb.UserName = userdet?.Username;
                loginDb.Username = userdet?.Username; 
                usersInDb.Password = userdet?.Password;
                usersInDb.RPassword = userdet.Password;
                usersInDb.CgppDivision = Db.CgppDivisions.Find(userdet.Divisionid);

                Db.SaveChanges();

                foreach (HttpPostedFileBase file in userdet.files)  
                {
                    if (file != null)
                    {
                        var InputFileName = userdet.FilePath + DateTime.Now.Ticks +
                                            Path.GetFileName(file.FileName);
                        var FileNameGetter = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/ProfileImages/") + InputFileName);

                        file.SaveAs(ServerSavePath);

                        usersInDb.FilePath = "/ProfileImages/" + InputFileName;

                    }

                 


                }

             

                Db.SaveChanges();
            }

            TempData["Message"] = "SAVE SUCCESSFULLY";
            return RedirectToAction("UsersList");
        }
        public ActionResult UsersPartial() //IMAGE AND NAME VIEW IN DASHBOARD
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var users = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;
            if ((int)Session["Role_Id"] != 1)
            {
                users = users.Where(d => d.UserID == userID).ToList();
            }

            return PartialView(users);
        }

        public ActionResult PartialNameUser() // NAME VIEW IN DASHBOARD
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var username = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;
            if ((int)Session["Role_Id"] != 1)
            {
                username = username.Where(d => d.UserID == userID).ToList();
            }

            return PartialView(username);
        }

        public ActionResult AdminPartialView() //IMAGE AND NAME VIEW IN DASHBOARD
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var adminname = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;

            adminname = adminname.Where(d => d.UserID == userID).ToList();
            

            return PartialView(adminname);
        }


        public ActionResult AdminPartial() // NAME VIEW IN DASHBOARD
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var adminpartial = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;

            adminpartial = adminpartial.Where(d => d.UserID == userID).ToList();


            return PartialView(adminpartial);
        }


        public ActionResult AdminPartialHeaderpic() // NAME VIEW IN DASHBOARD
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var headpic = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;

            headpic = headpic.Where(d => d.UserID == userID).ToList();


            return PartialView(headpic);
        }

        public ActionResult UserPartialHeaderPic() // NAME VIEW IN DASHBOARD
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var userheadpic = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;
            if ((int)Session["Role_Id"] != 1)
            {
                userheadpic = userheadpic.Where(d => d.UserID == userID).ToList();
            }

            return PartialView(userheadpic);
        }


        public ActionResult ResetPasswordPicture() // NAME VIEW IN DASHBOARD
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            //if ((int)Session["Role_Id"] != 1)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var resetpic = Db.Users.ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;

            resetpic = resetpic.Where(d => d.UserID == userID).ToList();


            return PartialView(resetpic);
        }

        [HttpPost]
        public ActionResult ResetPassword(int UserID, string Password )
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (ModelState.IsValid)
            {
                var id = Db.Users.Single(c => c.UserID == UserID).LoginID;
                var usersInDb = Db.Logins.Find(id);

                var pw = encoder.Encode(Password);
                usersInDb.Password = pw;
 

                Db.SaveChanges();
            }


            TempData["Message"] = "SAVE SUCCESSFULLY";
            return RedirectToAction("UsersList");
        }


    }
}