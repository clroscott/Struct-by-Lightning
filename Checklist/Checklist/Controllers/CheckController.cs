using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Checklist.Models;
using System.Data.Entity.Infrastructure;

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
         * Modified by: Clayton, Aaron
         * View to display Locations of the user logged in
         */
        public ActionResult LocationList()
        {
            ViewBag.Message = "List of Locations";//title of page

            //string query = "SELECT * FROM LocationCopy "
            //    + "WHERE BusinessConsultant = '" + User.Identity.Name + "'";//The sql query to get information
            

           // ViewBag.DB = checkDB.LocationCopies.SqlQuery(query);//Executes the query and puts result into the viewbag

           //ViewBag.DB = query;


            var query = from l in checkDB.ws_locationView
                        where l.BusinessConsultant == User.Identity.Name
                        select l;

            return View(query);
        }


        /**
         * Author:Aaron
         * Modified by: Aaron
         * Partial view to display action items
         */
        [ChildActionOnly]
        public ActionResult DisplayActionItems(int loc)
        {
            var query = from l in checkDB.SiteActionItems
                        where l.LocationID == loc
                        && l.Complete == false
                        select l;

            return PartialView("DisplayActionItems", query);
        }





        /**
         * Author: Clayton
         * Modified by: Clayton, Aaron
         * View to display information of chosen location
         */
        public ActionResult LocationInfo(int locationId)
        {
            ViewBag.Message = "Location Information";//title of page

            //SQL query to grab the location information
            var queryLocationInfo = from a in checkDB.ws_locationView
                                    where a.LocationId == locationId
                                    select a;


            //ViewBag.LocationInformation = queryLocationInfo;//Executes the query and puts result into the viewbag

            return View(queryLocationInfo);
        }



        /**
         * Author: Clayton
         * Modified by: Clayton, Jung
         * View to display previous checklists of a location
         */
        public ActionResult PreviousChecklists(int locationId)
        {
            ViewBag.Message = " Previous Checklists";//title of page
            ViewBag.LocationId = locationId;


            /*string query = "SELECT * FROM SiteVisit "
                + "WHERE LocationID = '" + locationId + "' "
                + "ORDER BY dateOfVisit"; 

            ViewBag.SiteVisitDB = checkDB.SiteVisits.SqlQuery(query);*/

            var siteVistQuery = from s in checkDB.SiteVisits
                                where s.LocationID == locationId
                                orderby s.dateOfVisit
                                select s;


            return View(siteVistQuery);
        }

        /**
         * Author: Clayton
         * Modified by: Clayton
         * View to create a new checklists of a location
         */
        public ActionResult NewChecklist(int locationId)
        {
            ViewBag.Message = "New Checklist";//title of page

            
            /*var location_query = from l in checkDB.ws_locationView
                                 where l.LocationId == locationId
            */



            /*string location_query = "SELECT * FROM ws_locationView "
                + "WHERE LocationId = " + locationId;

            var loc = checkDB.ws_locationView.SqlQuery(location_query);
            

            foreach (var l in loc)
            {
                ViewBag.Location = l.LocationName;
            }
            */


            ViewBag.LocationId = locationId;
            

            string manager_query = "SELECT * FROM Question "
                + "WHERE SectionID = 1 "
                + "ORDER BY QuestionOrder ";//The sql query to get the manager questions


            ViewBag.Manager = checkDB.Questions.SqlQuery(manager_query);//Executes the query and puts result into the viewbag


            var form_query = from f in checkDB.Forms
                             select f;

            return View(form_query);
        }


        /**
         * Author: Clayton
         * Modified by: Clayton
         * Partial view to display action items
         */
        [ChildActionOnly]
        public ActionResult Sections(int loc)
        {
            var query = from l in checkDB.SiteActionItems
                        where l.LocationID == loc
                        && l.Complete == false
                        select l;

            return PartialView("Sections", query);


            /*
            string section_query_rest = "SELECT * FROM Section "
                + "WHERE NOT (SectionID = 1) "
                + "ORDER BY SectionOrder ";//The sql query to get sections


            ViewBag.SectionDB = checkDB.Sections.SqlQuery(section_query_rest);//Executes the query and puts result into the viewbag


            string question_query_rest = "SELECT * FROM Question "
                + "WHERE Active = 'true' "
                + "AND NOT (SectionID = 1) "
                + "ORDER BY SectionID, QuestionOrder ";//The sql query to get the questions

            ViewBag.QuestionDB = checkDB.Questions.SqlQuery(question_query_rest);//Executes the query and puts result into the viewbag
            */


        }





        /**
         * Author: Clayton
         * Modified by: Clayton
         * View to display the selected previous checklist
         */
        public ActionResult OldChecklist(int locationId)
        {
            ViewBag.Message = "Old Checklist";//title of page

            ViewBag.LocationId = locationId;

            return View();
        }

        /**
         * Author: Clayton
         * Modified by: Clayton
         * View to display a confirmation that the checklist was sent
         */
        public ActionResult SendConfirmation(int locationId)
        {
            ViewBag.Message = "Send Confirmation";//title of page

            ViewBag.LocationId = locationId;

            return View();
        }
        
        

    }
}
