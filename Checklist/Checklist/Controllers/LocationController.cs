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
    public class LocationController : Controller
    {
        private ChecklistEntities db = new ChecklistEntities();

        //
        // GET: /Location/

        public ActionResult Index()
        {
            return View(db.ws_locationView.ToList());
        }

        //
        // GET: /Location/Details/5

        public ActionResult Details(int id = 0)
        {
            ws_locationView ws_locationview = db.ws_locationView.Find(id);
            if (ws_locationview == null)
            {
                return HttpNotFound();
            }
            return View(ws_locationview);
        }

        //
        // GET: /Location/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Location/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ws_locationView ws_locationview)
        {
            if (ModelState.IsValid)
            {
                db.ws_locationView.Add(ws_locationview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ws_locationview);
        }

        //
        // GET: /Location/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ws_locationView ws_locationview = db.ws_locationView.Find(id);
            if (ws_locationview == null)
            {
                return HttpNotFound();
            }
            return View(ws_locationview);
        }

        //
        // POST: /Location/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ws_locationView ws_locationview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ws_locationview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ws_locationview);
        }

        //
        // GET: /Location/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ws_locationView ws_locationview = db.ws_locationView.Find(id);
            if (ws_locationview == null)
            {
                return HttpNotFound();
            }
            return View(ws_locationview);
        }

        //
        // POST: /Location/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ws_locationView ws_locationview = db.ws_locationView.Find(id);
            db.ws_locationView.Remove(ws_locationview);
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