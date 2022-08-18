using SmsManagementSystem.Models;
using SmsManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmsManagementSystem.Controllers
{
    public class SingleDraftsController : Controller
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
            var vm = new SingleDraftVM()
            {
                DraftList = drafts
            };
            return View("Create", vm);
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