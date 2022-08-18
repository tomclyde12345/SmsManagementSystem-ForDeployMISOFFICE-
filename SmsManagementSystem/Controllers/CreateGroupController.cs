using SmsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmsManagementSystem.Controllers
{
    public class CreateGroupController : Controller
    {
        private ilsEntities db = new ilsEntities();

        // GET: CreateGroup
        public ActionResult Index()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            if ((int)Session["Role_Id"] != 1)
            {
                return RedirectToAction("logout", "Account");

            }

            return View(db.CgppGroups.ToList());
        }

        // GET: CreateGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppGroup cgppGroup = db.CgppGroups.Find(id);
            if (cgppGroup == null)
            {
                return HttpNotFound();
            }
            return View(cgppGroup);
        }

        // GET: CreateGroup/Create
        public ActionResult Create()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            if ((int)Session["Role_Id"] != 1)
            {
                return RedirectToAction("logout", "Account");

            }
            return View();
        }

        // POST: CreateGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,GroupName")] CgppGroup cgppGroup)
        {

            if (ModelState.IsValid)
            {
                if (Session["Role_Id"] == null)
                {
                    return RedirectToAction("logout", "Account");
                }
                if ((int)Session["Role_Id"] != 1)
                {
                    return RedirectToAction("logout", "Account");

                }
                db.CgppGroups.Add(cgppGroup);
                db.SaveChanges();
                TempData["Message"] = "SAVE SUCCESSFULLY";
                return RedirectToAction("Index");
            }

            return View(cgppGroup);
        }

        // GET: CreateGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppGroup cgppGroup = db.CgppGroups.Find(id);
            if (cgppGroup == null)
            {
                return HttpNotFound();
            }
            return View(cgppGroup);
        }

        // POST: CreateGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,GroupName")] CgppGroup cgppGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cgppGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cgppGroup);
        }

        // GET: CreateGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppGroup cgppGroup = db.CgppGroups.Find(id);
            if (cgppGroup == null)
            {
                return HttpNotFound();
            }
            return View(cgppGroup);
        }

        // POST: CreateGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CgppGroup cgppGroup = db.CgppGroups.Find(id);
            db.CgppGroups.Remove(cgppGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
