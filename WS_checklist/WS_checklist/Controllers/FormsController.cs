using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS_checklist.Models;

namespace WS_checklist.Controllers
{   
    public class FormsController : Controller
    {
		private readonly IFormRepository formRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public FormsController() : this(new FormRepository())
        {
        }

        public FormsController(IFormRepository formRepository)
        {
			this.formRepository = formRepository;
        }

        //
        // GET: /Forms/

        public ViewResult Index()
        {
            return View(formRepository.AllIncluding(form => form.Sections, form => form.SiteVisits));
        }

        //
        // GET: /Forms/Details/5

        public ViewResult Details(int id)
        {
            return View(formRepository.Find(id));
        }

        //
        // GET: /Forms/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Forms/Create

        [HttpPost]
        public ActionResult Create(Form form)
        {
            if (ModelState.IsValid) {
                formRepository.InsertOrUpdate(form);
                formRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Forms/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(formRepository.Find(id));
        }

        //
        // POST: /Forms/Edit/5

        [HttpPost]
        public ActionResult Edit(Form form)
        {
            if (ModelState.IsValid) {
                formRepository.InsertOrUpdate(form);
                formRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Forms/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(formRepository.Find(id));
        }

        //
        // POST: /Forms/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            formRepository.Delete(id);
            formRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                formRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

