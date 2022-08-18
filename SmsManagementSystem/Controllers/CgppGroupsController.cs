using SmsManagementSystem.Models;
using SmsManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmsManagementSystem.Controllers
{
    public class CgppGroupsController : Controller
    {
        ilsEntities Db = new ilsEntities();
        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
        }
        // GET: CgppGroups
        public ActionResult Index()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }

            var drafts = Db.Drafts.ToList();


            return View(drafts);

        }
        public ActionResult Create()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            var drafts = Db.Drafts.ToList();
            var offices = Db.CgppOffices.ToList();
            var group = Db.CgppGroups.ToList();
            var phone = Db.CgppPhonebooks.ToList();
            var vm = new GroupMessageVM()
            {
                DraftList = drafts,
                OfficeList = offices,
                GroupList = group,
                PhonebookList = phone,

            };
            List<CgppGroup> cgppGroupslist = Db.CgppGroups.ToList();
            ViewBag.Groupslist = new SelectList(cgppGroupslist, "GroupId", "GroupName");
            return View("Create", vm);

        }
        public JsonResult GetPhonebookList(int GroupId)
        {
            Db.Configuration.ProxyCreationEnabled = false;
            List<CgppPhonebook> PhoneList = Db.CgppPhonebooks.Where(x => x.GroupId == GroupId).ToList();
            return Json(PhoneList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Save(Draft draft)
        {
            if (draft.draftID == 0)
            {

                draft.Sendto = draft.Sendto;
                draft.msg = draft.msg;

                Db.Drafts.Add(draft);
            }

            Db.SaveChanges();
            TempData["Message"] = "SAVE SUCCESSFULLY";
            return RedirectToAction("Index", "Drafts");
        }
    }
}