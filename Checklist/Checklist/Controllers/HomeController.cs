using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Checklist.Models;

namespace Checklist.Controllers
{
    public class HomeController : Controller
    {

        private ChecklistEntities checkDB = new ChecklistEntities();

        public ActionResult LocationList()
        {
            ViewBag.Message = "List of Locations.";


            var test = checkDB;


            return View(test);
        }

        public ActionResult LocationInfo()
        {
            ViewBag.Message = "Location Information.";

            return View();
        }

        public ActionResult PreviousChecklists()
        {
            ViewBag.Message = " Previous Checklists.";

            return View();
        }

        public ActionResult NewChecklist()
        {
            ViewBag.Message = "New Checklist.";

            return View();
        }

        public ActionResult OldChecklist()
        {
            ViewBag.Message = "Old Checklist.";

            return View();
        }

        public ActionResult SendConfirmation()
        {
            ViewBag.Message = "Send Confirmation.";

            return View();
        }

    }
}
