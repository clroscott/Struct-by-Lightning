using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS_checklist.Models;

namespace WS_checklist.Controllers
{   
    public class AnswersController : Controller
    {
		private readonly ISiteVisitRepository sitevisitRepository;
		private readonly IQuestionRepository questionRepository;
		private readonly IAnswerRepository answerRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AnswersController() : this(new SiteVisitRepository(), new QuestionRepository(), new AnswerRepository())
        {
        }

        public AnswersController(ISiteVisitRepository sitevisitRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
			this.sitevisitRepository = sitevisitRepository;
			this.questionRepository = questionRepository;
			this.answerRepository = answerRepository;
        }

        //
        // GET: /Answers/

        public ViewResult Index()
        {
            return View(answerRepository.AllIncluding(answer => answer.SiteVisit, answer => answer.Question));
        }

        //
        // GET: /Answers/Details/5

        public ViewResult Details(int id)
        {
            return View(answerRepository.Find(id));
        }

        //
        // GET: /Answers/Create

        public ActionResult Create()
        {
			ViewBag.PossibleSiteVisits = sitevisitRepository.All;
			ViewBag.PossibleQuestions = questionRepository.All;
            return View();
        } 

        //
        // POST: /Answers/Create

        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            if (ModelState.IsValid) {
                answerRepository.InsertOrUpdate(answer);
                answerRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSiteVisits = sitevisitRepository.All;
				ViewBag.PossibleQuestions = questionRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Answers/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleSiteVisits = sitevisitRepository.All;
			ViewBag.PossibleQuestions = questionRepository.All;
             return View(answerRepository.Find(id));
        }

        //
        // POST: /Answers/Edit/5

        [HttpPost]
        public ActionResult Edit(Answer answer)
        {
            if (ModelState.IsValid) {
                answerRepository.InsertOrUpdate(answer);
                answerRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSiteVisits = sitevisitRepository.All;
				ViewBag.PossibleQuestions = questionRepository.All;
				return View();
			}
        }

        //
        // GET: /Answers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(answerRepository.Find(id));
        }

        //
        // POST: /Answers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            answerRepository.Delete(id);
            answerRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sitevisitRepository.Dispose();
                questionRepository.Dispose();
                answerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

