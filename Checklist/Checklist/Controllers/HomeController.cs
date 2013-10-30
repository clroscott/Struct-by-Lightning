using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Checklist.Models;
using System.Data.SqlClient;

/**
 * Authour: Clayton
 * Modified: Clayton
 * Conroller for the main webpages
 */
namespace Checklist.Controllers
{
    public class HomeController : Controller
    {
        //
        private ChecklistEntities checkDB = new ChecklistEntities();

        public ActionResult LocationList()
        {
            ViewBag.Message = "List of Locations";

            string current_user = "Brett Hertzog";

            string query = "SELECT * FROM LocationCopy "
                + "WHERE BusinessConsultant = '" + current_user + "'";

            ViewBag.DB = checkDB.LocationCopies.SqlQuery(query); 


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
