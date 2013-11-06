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
    public class TestController : Controller
    {
        private ChecklistEntities db = new ChecklistEntities();

        //
        // GET: /Test/

        public ActionResult Index()
        {
            var answers = db.Answers.Include(a => a.Question).Include(a => a.SiteVisit);
            return View(answers.ToList());
        }

        //
        // GET: /Test/Details/5

        public ActionResult Details(int id = 0)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "QuestionName");
            ViewBag.SiteVisitID = new SelectList(db.SiteVisits, "SiteVisitID", "CommentPublic");
            return View();
        }

        //
        // POST: /Test/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "QuestionName", answer.QuestionID);
            ViewBag.SiteVisitID = new SelectList(db.SiteVisits, "SiteVisitID", "CommentPublic", answer.SiteVisitID);
            return View(answer);
        }

        //
        // GET: /Test/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "QuestionName", answer.QuestionID);
            ViewBag.SiteVisitID = new SelectList(db.SiteVisits, "SiteVisitID", "CommentPublic", answer.SiteVisitID);
            return View(answer);
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "QuestionName", answer.QuestionID);
            ViewBag.SiteVisitID = new SelectList(db.SiteVisits, "SiteVisitID", "CommentPublic", answer.SiteVisitID);
            return View(answer);
        }

        //
        // GET: /Test/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        //
        // POST: /Test/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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