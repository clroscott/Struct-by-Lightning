using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS_checklist.Models;

namespace WS_checklist.Controllers
{   
    public class SiteVisitsController : Controller
    {
		private readonly IFormRepository formRepository;
		private readonly ISiteVisitRepository sitevisitRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SiteVisitsController() : this(new FormRepository(), new SiteVisitRepository())
        {
        }

        public SiteVisitsController(IFormRepository formRepository, ISiteVisitRepository sitevisitRepository)
        {
			this.formRepository = formRepository;
			this.sitevisitRepository = sitevisitRepository;
        }

        //
        // GET: /SiteVisits/

        public ViewResult Index()
        {
            return View(sitevisitRepository.AllIncluding(sitevisit => sitevisit.Form, sitevisit => sitevisit.Answers, sitevisit => sitevisit.SiteActionItems));
        }

        //
        // GET: /SiteVisits/Details/5

        public ViewResult Details(int id)
        {
            return View(sitevisitRepository.Find(id));
        }

        //
        // GET: /SiteVisits/Create

        public ActionResult Create()
        {
			ViewBag.PossibleForms = formRepository.All;
            return View();
        } 

        //
        // POST: /SiteVisits/Create

        [HttpPost]
        public ActionResult Create(SiteVisit sitevisit)
        {
            if (ModelState.IsValid) {
                sitevisitRepository.InsertOrUpdate(sitevisit);
                sitevisitRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleForms = formRepository.All;
				return View();
			}
        }
        
        //
        // GET: /SiteVisits/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleForms = formRepository.All;
             return View(sitevisitRepository.Find(id));
        }

        //
        // POST: /SiteVisits/Edit/5

        [HttpPost]
        public ActionResult Edit(SiteVisit sitevisit)
        {
            if (ModelState.IsValid) {
                sitevisitRepository.InsertOrUpdate(sitevisit);
                sitevisitRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleForms = formRepository.All;
				return View();
			}
        }

        //
        // GET: /SiteVisits/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(sitevisitRepository.Find(id));
        }

        //
        // POST: /SiteVisits/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            sitevisitRepository.Delete(id);
            sitevisitRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                formRepository.Dispose();
                sitevisitRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

