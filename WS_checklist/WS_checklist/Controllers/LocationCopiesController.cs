using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS_checklist.Models;

namespace WS_checklist.Controllers
{   
    public class LocationCopiesController : Controller
    {
		private readonly ILocationCopyRepository locationcopyRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public LocationCopiesController() : this(new LocationCopyRepository())
        {
        }

        public LocationCopiesController(ILocationCopyRepository locationcopyRepository)
        {
			this.locationcopyRepository = locationcopyRepository;
        }

        //
        // GET: /LocationCopies/

        public ViewResult Index()
        {
            return View(locationcopyRepository.AllIncluding(locationcopy => locationcopy.SiteVisits));
        }

        //
        // GET: /LocationCopies/Details/5

        public ViewResult Details(int id)
        {
            return View(locationcopyRepository.Find(id));
        }

        //
        // GET: /LocationCopies/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /LocationCopies/Create

        [HttpPost]
        public ActionResult Create(LocationCopy locationcopy)
        {
            if (ModelState.IsValid) {
                locationcopyRepository.InsertOrUpdate(locationcopy);
                locationcopyRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /LocationCopies/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(locationcopyRepository.Find(id));
        }

        //
        // POST: /LocationCopies/Edit/5

        [HttpPost]
        public ActionResult Edit(LocationCopy locationcopy)
        {
            if (ModelState.IsValid) {
                locationcopyRepository.InsertOrUpdate(locationcopy);
                locationcopyRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /LocationCopies/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(locationcopyRepository.Find(id));
        }

        //
        // POST: /LocationCopies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            locationcopyRepository.Delete(id);
            locationcopyRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                locationcopyRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

