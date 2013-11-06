using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS_checklist.Models;

namespace WS_checklist.Controllers
{   
    public class QuestionsController : Controller
    {
		private readonly ISectionRepository sectionRepository;
		private readonly IQuestionRepository questionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public QuestionsController() : this(new SectionRepository(), new QuestionRepository())
        {
        }

        public QuestionsController(ISectionRepository sectionRepository, IQuestionRepository questionRepository)
        {
			this.sectionRepository = sectionRepository;
			this.questionRepository = questionRepository;
        }

        //
        // GET: /Questions/

        public ViewResult Index()
        {
            return View(questionRepository.AllIncluding(question => question.Section, question => question.Answers));
        }

        //
        // GET: /Questions/Details/5

        public ViewResult Details(int id)
        {
            return View(questionRepository.Find(id));
        }

        //
        // GET: /Questions/Create

        public ActionResult Create()
        {
			ViewBag.PossibleSections = sectionRepository.All;
            return View();
        } 

        //
        // POST: /Questions/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid) {
                questionRepository.InsertOrUpdate(question);
                questionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSections = sectionRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Questions/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleSections = sectionRepository.All;
             return View(questionRepository.Find(id));
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid) {
                questionRepository.InsertOrUpdate(question);
                questionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSections = sectionRepository.All;
				return View();
			}
        }

        //
        // GET: /Questions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(questionRepository.Find(id));
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            questionRepository.Delete(id);
            questionRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sectionRepository.Dispose();
                questionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

