using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS_checklist.Models;

namespace WS_checklist.Controllers
{   
    public class SiteActionItemsController : Controller
    {
		private readonly ISiteVisitRepository sitevisitRepository;
		private readonly ISiteActionItemRepository siteactionitemRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SiteActionItemsController() : this(new SiteVisitRepository(), new SiteActionItemRepository())
        {
        }

        public SiteActionItemsController(ISiteVisitRepository sitevisitRepository, ISiteActionItemRepository siteactionitemRepository)
        {
			this.sitevisitRepository = sitevisitRepository;
			this.siteactionitemRepository = siteactionitemRepository;
        }

        //
        // GET: /SiteActionItems/

        public ViewResult Index()
        {
            return View(siteactionitemRepository.AllIncluding(siteactionitem => siteactionitem.SiteVisit));
        }

        //
        // GET: /SiteActionItems/Details/5

        public ViewResult Details(int id)
        {
            return View(siteactionitemRepository.Find(id));
        }

        //
        // GET: /SiteActionItems/Create

        public ActionResult Create()
        {
			ViewBag.PossibleSiteVisits = sitevisitRepository.All;
            return View();
        } 

        //
        // POST: /SiteActionItems/Create

        [HttpPost]
        public ActionResult Create(SiteActionItem siteactionitem)
        {
            if (ModelState.IsValid) {
                siteactionitemRepository.InsertOrUpdate(siteactionitem);
                siteactionitemRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSiteVisits = sitevisitRepository.All;
				return View();
			}
        }
        
        //
        // GET: /SiteActionItems/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleSiteVisits = sitevisitRepository.All;
             return View(siteactionitemRepository.Find(id));
        }

        //
        // POST: /SiteActionItems/Edit/5

        [HttpPost]
        public ActionResult Edit(SiteActionItem siteactionitem)
        {
            if (ModelState.IsValid) {
                siteactionitemRepository.InsertOrUpdate(siteactionitem);
                siteactionitemRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSiteVisits = sitevisitRepository.All;
				return View();
			}
        }

        //
        // GET: /SiteActionItems/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(siteactionitemRepository.Find(id));
        }

        //
        // POST: /SiteActionItems/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            siteactionitemRepository.Delete(id);
            siteactionitemRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sitevisitRepository.Dispose();
                siteactionitemRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

