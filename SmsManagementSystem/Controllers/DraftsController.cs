using SmsManagementSystem.Models;
using SmsManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace SmsManagementSystem.Controllers
{
    public class DraftsController : Controller
    {
        ilsEntities Db = new ilsEntities();

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
        }
        // GET: Drafts
        public ActionResult Index()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }

            //}drafts_arr
            return View();


        }
        [HttpPost]
        public ActionResult LoadData()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            //List<Draft> draftlist = new List<Draft>();
            using (ilsEntities Db = new ilsEntities())
            {
                IQueryable<Draft> draftlist = Db.Drafts;//.ToList<Draft>();

                var sess_id = (int)Session["LoginID"];
                var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;
                if ((int)Session["Role_Id"] != 1)

                {
                    draftlist = draftlist.Where(d => d.CgppDrafts.Count > 0 && d.CgppDrafts.FirstOrDefault().User != null && d.CgppDrafts.FirstOrDefault().User.UserID == userID);
                }


                int totalrows = draftlist.Count();
                //custom filtering
                if (!string.IsNullOrEmpty(Request["columns[0][search][value]"]))
                    draftlist = draftlist.Where(x => x.draftID.ToString().Contains(Request["columns[0][search][value]"].ToString()));//.ToList<Draft>();

                if (!string.IsNullOrEmpty(Request["columns[1][search][value]"]))
                {
                    var s = Request["columns[1][search][value]"].ToLowerInvariant();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().Draft != null && x.msg.ToString().Contains(s));
                }


                if (!string.IsNullOrEmpty(Request["columns[2][search][value]"]))
                {
                    var s = Request["columns[2][search][value]"].ToLowerInvariant();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().Draft != null && x.tag.ToString().Contains(s));
                }

                if (!string.IsNullOrEmpty(Request["columns[3][search][value]"]))
                {
                    var s = Request["columns[3][search][value]"].ToLowerInvariant();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().CgppOffice != null && x.CgppDrafts.FirstOrDefault().CgppOffice.Name.ToLower().Contains(s));
                }


                if (!string.IsNullOrEmpty(Request["columns[4][search][value]"]))
                {
                    var s = Request["columns[4][search][value]"].ToLowerInvariant();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().CgppDivision != null && x.CgppDrafts.FirstOrDefault().CgppDivision.Name.ToLower().Contains(s));//.ToList<Draft>();
                }

                if (!string.IsNullOrEmpty(Request["columns[5][search][value]"]))
                {
                    var s = Request["columns[5][search][value]"].ToLowerInvariant();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().User != null && x.CgppDrafts.FirstOrDefault().User.Name.ToLower().Contains(s));//.ToList<Draft>();
                }

                if (!string.IsNullOrEmpty(Request["columns[6][search][value]"]))
                {
                    var s = Request["columns[6][search][value]"].ToLower();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().DateAdded != null && x.CgppDrafts.FirstOrDefault().DateAdded.ToString().Contains(s));//.ToList<Draft>();
                }

                if (!string.IsNullOrEmpty(Request["columns[7][search][value]"]))
                {
                    var s = Request["columns[7][search][value]"].ToLowerInvariant();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().Draft != null && x.Sendto.ToLower().Contains(s));
                }

                if (!string.IsNullOrEmpty(Request["columns[8][search][value]"]))
                {
                    var s = Request["columns[8][search][value]"].ToLowerInvariant();
                    draftlist = draftlist.Where(x => x.CgppDrafts.Count > 0 && x.CgppDrafts.FirstOrDefault().CgppPhonebook.FullName != null && x.CgppDrafts.FirstOrDefault().CgppPhonebook.FullName.ToLower().Contains(s));
                }






                int totalrowsafterfiltering = draftlist.Count();
                //sorting
                draftlist = draftlist.OrderBy(sortColumnName + " " + sortDirection).OrderByDescending(u => u.draftID);//.ToList<Draft>();

                //paging
                draftlist = draftlist.Skip(start).Take(length);//.ToList<Draft>();


                var drafts_arr = draftlist.Select(draft => new DraftListVM()
                {
                    oficeName = (
                        draft.CgppDrafts.Count > 0
                    ) && (
                        draft.CgppDrafts.FirstOrDefault().CgppOffice != null
                    ) ? draft.CgppDrafts.FirstOrDefault().CgppOffice.Name : "N/A",

                    divisionName = (
                        draft.CgppDrafts.Count > 0
                    ) && (
                        draft.CgppDrafts.FirstOrDefault().CgppDivision != null
                    ) ? draft.CgppDrafts.FirstOrDefault().CgppDivision.Name : "N/A",

                    username = (
                        draft.CgppDrafts.Count > 0
                    ) && (
                        draft.CgppDrafts.FirstOrDefault().User != null
                    ) ? draft.CgppDrafts.FirstOrDefault().User.Name : "N/A",

                    Dateadded = (
                        draft.CgppDrafts.Count > 0
                    ) && (
                        draft.CgppDrafts.FirstOrDefault().DateAdded.HasValue
                    ) ? draft.CgppDrafts.FirstOrDefault().DateAdded.Value : (DateTime?)null,

                    msg = draft.msg,
                    draftId = draft.draftID,
                    tag = draft.tag,
                    cellphonenum = draft.Sendto ?? "N/A",
                    sendToName = Db.CgppPhonebooks.FirstOrDefault(p => p.CellphoneNum == (draft.Sendto ?? "")).FullName ?? "N/A",
                }).ToList();

                return Json(new { data = drafts_arr, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);


            }

        }

        public ActionResult Create()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            var draft = Db.Drafts.ToList();
            var phone = Db.CgppPhonebooks.ToList();

            //if user //Hide Select cellnumber
            if ((int)Session["Role_Id"] == 2)
            {
                var sess_id = (int)Session["LoginID"];
                var userID = Db.Users.FirstOrDefault(o => o.LoginID == sess_id)?.UserID;
                phone = phone.Where(x => x.UsersId == userID).ToList();
            }



            var vm = new DraftVM()
            {
                DraftList = draft,
                Phonebooklist = phone,
                Draft = new Draft { draftID = 0 }
            };

            return View("Create", vm);
        }



        public ActionResult Save(DraftVM draft)

        {
            if (draft.draftId == 0)
            {

                var loginid = (int)Session["LoginID"];
                foreach (var sendTo in draft.sendTo)
                {
                    var newDraft = new Draft
                    {
                        tag = 0,
                        msg = draft.msg,
                        Sendto = sendTo
                    };
                    Db.Drafts.Add(newDraft);
                    Db.SaveChanges();

                    Db.CgppDrafts.Add(new CgppDraft
                    {
                        DateAdded = DateTime.Now,
                        DraftsId = newDraft.draftID,
                        OfficeId = Db.Users.FirstOrDefault(o => o.LoginID == loginid)?.OfficeID,
                        DivisionId = Db.Users.FirstOrDefault(o => o.LoginID == loginid)?.DivisionID,
                        UsersId = Db.Users.FirstOrDefault(o => o.LoginID == loginid)?.UserID,

                    });
                    Db.SaveChanges();
                }




            }
            TempData["Message"] = "SAVE SUCCESSFULLY";

            return RedirectToAction("Index", "Drafts");
        }


    }
}