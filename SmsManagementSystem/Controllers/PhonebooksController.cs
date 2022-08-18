using SmsManagementSystem.Models;
using SmsManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmsManagementSystem.Controllers
{
    public class PhonebooksController : Controller
    {
        ilsEntities Db = new ilsEntities();
        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
        }
        // GET: Phonebooks
        public ActionResult Index()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            var phonebook = Db.CgppPhonebooks.OrderByDescending(x => x.PhoneId).ToList();

            var sess_id = (int)Session["LoginID"];
            var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;
            if ((int)Session["Role_Id"] != 1)
            {
                phonebook = phonebook.Where(d => d.UsersId == userID).ToList();
            }

            return View(phonebook);
        }





        public ActionResult Create()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }

            ViewBag.Group = Db.CgppGroups.ToList();
            var phoneb = Db.CgppPhonebooks.ToList();

            var group = Db.CgppGroups.ToList();
            var vm = new PhonebookVM()
            {
                PhonebookList = phoneb,
                GroupList = group

            };
            return View("Create", vm);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Group = Db.CgppGroups.ToList();
            var phonebook = Db.CgppPhonebooks.SingleOrDefault(c => c.PhoneId == id); //EDIT METHOD
            if (phonebook == null)
                return HttpNotFound();
            var viewModel = new PhonebookVM()
            {

                Phonebook = phonebook,
                PhonebookList = Db.CgppPhonebooks.ToList(),
                GroupList = Db.CgppGroups.ToList(),

            };

            return View("Edit", viewModel);
        }
        public ActionResult Save(CgppPhonebook phonebook)
        {
            if (phonebook.PhoneId == 0)
            {
                var loginid = (int)Session["LoginID"];

                phonebook.OfficeId = Db.Users.FirstOrDefault(o => o.LoginID == loginid)?.OfficeID;
                phonebook.DivisionId = Db.Users.FirstOrDefault(o => o.LoginID == loginid)?.DivisionID;
                phonebook.UsersId = Db.Users.FirstOrDefault(o => o.LoginID == loginid)?.UserID;
                phonebook.DateAdded = DateTime.Now;
                Db.CgppPhonebooks.Add(phonebook);
            }
            else
            {
                var usersInDb = Db.CgppPhonebooks.Single(c => c.PhoneId == phonebook.PhoneId);

                usersInDb.PhoneId = phonebook.PhoneId;
                usersInDb.CellphoneNum = phonebook.CellphoneNum;
                usersInDb.FullName = phonebook.FullName;
                usersInDb.DateAdded = DateTime.Now;
                usersInDb.Position = phonebook.Position;
                usersInDb.Remarks = phonebook.Remarks;
                usersInDb.GroupId = phonebook.GroupId;


                //usersInDb.CgppDivision.Name = phonebook.CgppDivision.Name;
                //usersInDb.User.Name = phonebook.User.Name;
                //usersInDb.CgppGroup.GroupName = phonebook.CgppGroup.GroupName;
            }

            Db.SaveChanges();
            TempData["Message"] = "SAVE SUCCESSFULLY";
            return RedirectToAction("Index", "Phonebooks");
        }

    }
}