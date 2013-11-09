using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS_checklist.Models;

namespace WS_checklist.Controllers
{   
    public class SectionsController : Controller
    {
		private readonly IFormRepository formRepository;
		private readonly ISectionRepository sectionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SectionsController() : this(new FormRepository(), new SectionRepository())
        {
        }

        public SectionsController(IFormRepository formRepository, ISectionRepository sectionRepository)
        {
			this.formRepository = formRepository;
			this.sectionRepository = sectionRepository;
        }

        //
        // GET: /Sections/

        public ViewResult Index()
        {
            return View(sectionRepository.AllIncluding(section => section.Form, section => section.Questions));
        }

        //
        // GET: /Sections/Details/5

        public ViewResult Details(int id)
        {
            return View(sectionRepository.Find(id));
        }

        //
        // GET: /Sections/Create

        public ActionResult Create()
        {
			ViewBag.PossibleForms = formRepository.All;
            return View();
        } 

        //
        // POST: /Sections/Create

        [HttpPost]
        public ActionResult Create(Section section)
        {
            if (ModelState.IsValid) {
                sectionRepository.InsertOrUpdate(section);
                sectionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleForms = formRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Sections/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleForms = formRepository.All;
             return View(sectionRepository.Find(id));
        }

        //
        // POST: /Sections/Edit/5

        [HttpPost]
        public ActionResult Edit(Section section)
        {
            if (ModelState.IsValid) {
                sectionRepository.InsertOrUpdate(section);
                sectionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleForms = formRepository.All;
				return View();
			}
        }

        //
        // GET: /Sections/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(sectionRepository.Find(id));
        }

        //
        // POST: /Sections/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            sectionRepository.Delete(id);
            sectionRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                formRepository.Dispose();
                sectionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

