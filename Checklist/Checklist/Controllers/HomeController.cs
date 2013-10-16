using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Checklist.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LocationList()
        {
            ViewBag.Message = "List of Locations.";

            return View();
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
