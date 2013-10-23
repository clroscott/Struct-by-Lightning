using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Checklist.Models;

namespace Checklist.Controllers
{
    public class ConsultantController : Controller
    {
        private ConsultantContext db = new ConsultantContext();

        //
        // GET: /Consultant/

        public ActionResult Index()
        {
            return View(db.qEntry.ToList());
        }

        //
        // GET: /Consultant/Details/5

        public ActionResult Details(int id = 0)
        {
            LocationCopy locationcopy = db.qEntry.Find(id);
            if (locationcopy == null)
            {
                return HttpNotFound();
            }
            return View(locationcopy);
        }

        //
        // GET: /Consultant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Consultant/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationCopy locationcopy)
        {
            if (ModelState.IsValid)
            {
                db.qEntry.Add(locationcopy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locationcopy);
        }

        //
        // GET: /Consultant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LocationCopy locationcopy = db.qEntry.Find(id);
            if (locationcopy == null)
            {
                return HttpNotFound();
            }
            return View(locationcopy);
        }

        //
        // POST: /Consultant/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocationCopy locationcopy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationcopy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locationcopy);
        }

        //
        // GET: /Consultant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LocationCopy locationcopy = db.qEntry.Find(id);
            if (locationcopy == null)
            {
                return HttpNotFound();
            }
            return View(locationcopy);
        }

        //
        // POST: /Consultant/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationCopy locationcopy = db.qEntry.Find(id);
            db.qEntry.Remove(locationcopy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}