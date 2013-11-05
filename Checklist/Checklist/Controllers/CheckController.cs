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
         * Modified by: Clayton
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
         * Modified by: Clayton
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
         * Modified by: Clayton, Jung
         * View to display previous checklists of a location
         */
        public ActionResult PreviousChecklists(string location)
        {
            ViewBag.Message = " Previous Checklists";//title of page
            ViewData["Location"] = location;

            /*string location_query = "SELECT * FROM LocationCopy "
                + "WHERE BusinessConsultant = '" + User.Identity.Name + "'" + " AND LocationName = '" + location + "'";//The sql query to get information

            ViewBag.LocationDB = checkDB.LocationCopies.SqlQuery(location_query);//Executes the query and puts result into the viewbag

            string siteVisit_query = "SELECT * FROM SiteVisit "
                + "ORDER BY dateOfVisit DESC";//The sql query to get information

            ViewBag.SiteVisitDB = checkDB.SiteVisits.SqlQuery(siteVisit_query);//Executes the query and puts result into the viewbag
            */



            string query = "SELECT * FROM SiteVisit s "
                + "JOIN LocationCopy l "
                + "ON s.LocationId = l.LocationId "
                + "WHERE l.LocationName = '" + location + "' "
                + "ORDER BY s.dateOfVisit"; 

            ViewBag.SiteVisitDB = checkDB.SiteVisits.SqlQuery(query);




            return View();
        }

        /**
         * Author: Clayton
         * Modified by: Clayton
         * View to create a new checklists of a location
         */
        public ActionResult NewChecklist(string location)
        {
            ViewBag.Message = "New Checklist";//title of page
            ViewData["Location"] = location;




            string manager_query = "SELECT * FROM Question "
                + "WHERE SectionID = 1 "
                + "ORDER BY QuestionOrder ";//The sql query to get the manager questions


            ViewBag.Manager = checkDB.Questions.SqlQuery(manager_query);//Executes the query and puts result into the viewbag





            string section_query_rest = "SELECT * FROM Section "
                + "WHERE NOT (SectionID = 1) "
                + "ORDER BY SectionOrder ";//The sql query to get sections


            ViewBag.SectionDB = checkDB.Sections.SqlQuery(section_query_rest);//Executes the query and puts result into the viewbag


            string question_query_rest = "SELECT * FROM Question "
                + "WHERE Active = 'true' "
                + "AND NOT (SectionID = 1) "
                + "ORDER BY SectionID, QuestionOrder ";//The sql query to get the questions

            ViewBag.QuestionDB = checkDB.Questions.SqlQuery(question_query_rest);//Executes the query and puts result into the viewbag



            return View();
        }

        /**
         * Author: Clayton
         * Modified by: Clayton
         * View to display the selected previous checklist
         */
        public ActionResult OldChecklist(string location)
        {
            ViewBag.Message = "Old Checklist";//title of page

            ViewData["Location"] = location;

            return View();
        }

        /**
         * Author: Clayton
         * Modified by: Clayton
         * View to display a confirmation that the checklist was sent
         */
        public ActionResult SendConfirmation(String location)
        {
            ViewBag.Message = "Send Confirmation";//title of page

            ViewData["Location"] = location;

            return View();
        }
        
        

    }
}
