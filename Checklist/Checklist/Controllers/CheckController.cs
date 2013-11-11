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


            var location_query = from l in checkDB.ws_locationView
                                 where l.LocationId == locationId
                                 select l;


            foreach (var l in location_query)
            {
                ViewBag.Location = l.LocationName;
            }


            ViewBag.LocationId = locationId;
            


            var manager_query = from q in checkDB.Questions
                                where q.SectionID == 1
                                orderby q.QuestionOrder
                                select q;

            ViewBag.Manager = manager_query;



            var form_query = from f in checkDB.Forms
                             select f;

            return View(form_query);
        }


        /**
         * Author: Clayton
         * Modified by: Clayton
         * Partial view to display the sections of a form
         */
        [ChildActionOnly]
        public ActionResult Sections(int form)
        {
            var query = from s in checkDB.Sections
                        where s.SectionID != 1
                        && s.FormID == form
                        orderby s.SectionOrder
                        select s;

            return PartialView("Sections", query);
        }

        /**
         * Author: Clayton
         * Modified by: Clayton
         * Partial view to display the questions in a section
         */
        [ChildActionOnly]
        public ActionResult Questions(int section)
        {
            var query = from q in checkDB.Questions
                        where q.SectionID == section
                        && q.Active == true
                        orderby q.QuestionOrder
                        select q;

            return PartialView("Questions", query);
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
