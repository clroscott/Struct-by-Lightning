using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Checklist.Models;

namespace Checklist.Controllers
{
    public class CheckController : Controller
    {

        /**
         * The database entity
         */
        private ChecklistEntities checkDB = new ChecklistEntities();


        /**
         * Author: Clayton
         * Last Modified: Clayton
         * View to display Locations of the user logged in
         */
        public ActionResult LocationList()
        {
            ViewBag.Message = "List of Locations";//title of page

            string query = "SELECT * FROM LocationCopy "
                + "WHERE BusinessConsultant = '" + User.Identity.Name + "'";//The sql query to get information

            ViewBag.DB = checkDB.LocationCopies.SqlQuery(query);//Executes the query and puts result into the viewbag

            return View();
        }

        /**
         * Author: Clayton
         * Last Modified: Clayton
         * View to display information of chosen location
         */
        public ActionResult LocationInfo(string location)
        {
            ViewBag.Message = "Location Information";//title of page

            string query = "SELECT * FROM LocationCopy "
                + "WHERE LocationName = '" + location + "'";//The sql query to get information

            ViewBag.DB = checkDB.LocationCopies.SqlQuery(query);//Executes the query and puts result into the viewbag

            return View();
        }

        /**
         * Author: Clayton
         * Last Modified: Clayton
         * View to display previous checklists of a location
         */
        public ActionResult PreviousChecklists(string location)
        {
            ViewBag.Message = " Previous Checklists";//title of page
            ViewData["Location"] = location;

            string location_query = "SELECT * FROM LocationCopy "
                + "WHERE BusinessConsultant = '" + User.Identity.Name + "'" + " AND LocationName = '" + location + "'";//The sql query to get information

            ViewBag.LocationDB = checkDB.LocationCopies.SqlQuery(location_query);//Executes the query and puts result into the viewbag

            string siteVisit_query = "SELECT * FROM SiteVisit "
                + "ORDER BY dateOfVisit DESC";//The sql query to get information

            ViewBag.SiteVisitDB = checkDB.SiteVisits.SqlQuery(siteVisit_query);//Executes the query and puts result into the viewbag

            return View();
        }

        /**
         * Author: Clayton
         * Last Modified: Clayton
         * View to create a new checklists of a location
         */
        public ActionResult NewChecklist(string location)
        {
            ViewBag.Message = "New Checklist";//title of page
            ViewData["Location"] = location;

            string section_query = "SELECT * FROM Section "
                + "ORDER BY SectionOrder ";//The sql query to get sections

            ViewBag.SectionDB = checkDB.Sections.SqlQuery(section_query);//Executes the query and puts result into the viewbag

            string question_query = "SELECT * FROM Question "
                + "WHERE Active = 'true' "
                + "ORDER BY SectionID, QuestionOrder ";//The sql query to get the questions

            ViewBag.QuestionDB = checkDB.Questions.SqlQuery(question_query);//Executes the query and puts result into the viewbag



            return View();
        }

        /**
         * Author: Clayton
         * Last Modified: Clayton
         * View to display the selected previous checklist
         */
        public ActionResult OldChecklist()
        {
            ViewBag.Message = "Old Checklist";//title of page

            return View();
        }

        /**
         * Author: Clayton
         * Last Modified: Clayton
         * View to display a confirmation that the checklist was sent
         */
        public ActionResult SendConfirmation()
        {
            ViewBag.Message = "Send Confirmation";//title of page

            return View();
        }
        
        

    }
}
