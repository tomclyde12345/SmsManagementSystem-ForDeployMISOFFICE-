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
    public class DivisionsController : Controller
    {
        ilsEntities db = new ilsEntities();

        // GET: Divisions
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
            var cgppDivisions = db.CgppDivisions.Include(c => c.CgppOffice);
            return View(cgppDivisions.ToList());
        }

        // GET: Divisions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppDivision cgppDivision = db.CgppDivisions.Find(id);
            if (cgppDivision == null)
            {
                return HttpNotFound();
            }
            return View(cgppDivision);
        }

        // GET: Divisions/Create
        public ActionResult Create()
        {
            if (Session["Role_Id"] == null)
            {
                return RedirectToAction("logout", "Account");
            }
            ViewBag.Offices = db.CgppOffices.ToList();
            ViewBag.OfficeId = new SelectList(db.CgppOffices, "Id", "Name");
            if ((int)Session["Role_Id"] != 1)
            {
                return RedirectToAction("logout", "Account");
            }
            return View();
        }

        // POST: Divisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,OfficeId")] CgppDivision cgppDivision)
        {
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
                ViewBag.Offices = db.CgppOffices.ToList();
                db.CgppDivisions.Add(cgppDivision);
                db.SaveChanges();
                TempData["Message"] = "SAVE SUCCESSFULLY";
                return RedirectToAction("Index", "Divisions");
            }

            ViewBag.Offices = db.CgppOffices.ToList();
            ViewBag.OfficeId = new SelectList(db.CgppOffices, "Id", "Name", cgppDivision.OfficeId);
            return View(cgppDivision);
        }

        // GET: Divisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppDivision cgppDivision = db.CgppDivisions.Find(id);
            if (cgppDivision == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfficeId = new SelectList(db.CgppOffices, "Id", "Name", cgppDivision.OfficeId);
            return View(cgppDivision);
        }

        // POST: Divisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,OfficeId")] CgppDivision cgppDivision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cgppDivision).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Message"] = "SAVE SUCCESSFULLY";
                return RedirectToAction("Index");
            }
            ViewBag.OfficeId = new SelectList(db.CgppOffices, "Id", "Name", cgppDivision.OfficeId);
            return View(cgppDivision);
        }

        // GET: Divisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CgppDivision cgppDivision = db.CgppDivisions.Find(id);
            if (cgppDivision == null)
            {
                return HttpNotFound();
            }
            return View(cgppDivision);
        }

        // POST: Divisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CgppDivision cgppDivision = db.CgppDivisions.Find(id);
            db.CgppDivisions.Remove(cgppDivision);
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
