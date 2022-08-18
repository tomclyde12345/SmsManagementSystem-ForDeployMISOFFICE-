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
    public class OfficesController : Controller
    {
        ilsEntities db = new ilsEntities();

        // GET: Offices

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
            ViewBag.mssg = TempData["mssg"] as string;
            var office = db.CgppOffices.OrderByDescending(x => x.Id).ToList();
            return View(office.ToList());

        }

        // GET: Offices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppOffice cgppOffice = db.CgppOffices.Find(id);
            if (cgppOffice == null)
            {
                return HttpNotFound();
            }
            return View(cgppOffice);
        }
        [HttpGet]
        // GET: Offices/Create
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

        // POST: Offices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] CgppOffice cgppOffice)
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            if (ModelState.IsValid)
            {

                db.CgppOffices.Add(cgppOffice);
                db.SaveChanges();

                TempData["Message"] = "SAVE SUCCESSFULLY";

                return RedirectToAction("Index", "Offices");
            }

            return View(cgppOffice);
        }

        // GET: Offices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppOffice cgppOffice = db.CgppOffices.Find(id);
            if (cgppOffice == null)
            {
                return HttpNotFound();
            }
            return View(cgppOffice);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] CgppOffice cgppOffice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cgppOffice).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Message"] = "SAVE SUCCESSFULLY";
                return RedirectToAction("Index");
            }
            return View(cgppOffice);
        }

        // GET: Offices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppOffice cgppOffice = db.CgppOffices.Find(id);
            if (cgppOffice == null)
            {
                return HttpNotFound();
            }
            return View(cgppOffice);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CgppOffice cgppOffice = db.CgppOffices.Find(id);
            db.CgppOffices.Remove(cgppOffice);
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