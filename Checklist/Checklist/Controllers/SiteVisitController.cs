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
    public class SiteVisitController : Controller
    {
        private ChecklistEntities db = new ChecklistEntities();

        //
        // GET: /SiteVisit/

        public ActionResult Index()
        {
            var sitevisits = db.SiteVisits.Include(s => s.Form);
            return View(sitevisits.ToList());
        }

        //
        // GET: /SiteVisit/Details/5

        public ActionResult Details(int id = 0)
        {
            SiteVisit sitevisit = db.SiteVisits.Find(id);
            if (sitevisit == null)
            {
                return HttpNotFound();
            }
            return View(sitevisit);
        }

        //
        // GET: /SiteVisit/Create

        public ActionResult Create(int id)
        {
            SiteVisit newSiteVisit = 
            ViewBag.FormID = new SelectList(db.Forms, "FormID", "FormID");
            return View();
        }

        //
        // POST: /SiteVisit/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SiteVisit sitevisit)
        {
            if (ModelState.IsValid)
            {
                db.SiteVisits.Add(sitevisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FormID = new SelectList(db.Forms, "FormID", "FormID", sitevisit.FormID);
            return View(sitevisit);
        }

        //
        // GET: /SiteVisit/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SiteVisit sitevisit = db.SiteVisits.Find(id);
            if (sitevisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormID = new SelectList(db.Forms, "FormID", "FormID", sitevisit.FormID);
            return View(sitevisit);
        }

        //
        // POST: /SiteVisit/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteVisit sitevisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sitevisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormID = new SelectList(db.Forms, "FormID", "FormID", sitevisit.FormID);
            return View(sitevisit);
        }

        //
        // GET: /SiteVisit/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SiteVisit sitevisit = db.SiteVisits.Find(id);
            if (sitevisit == null)
            {
                return HttpNotFound();
            }
            return View(sitevisit);
        }

        //
        // POST: /SiteVisit/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteVisit sitevisit = db.SiteVisits.Find(id);
            db.SiteVisits.Remove(sitevisit);
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