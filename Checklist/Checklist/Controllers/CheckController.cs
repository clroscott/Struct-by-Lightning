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
         * Author:Aleeza
         * Posts new action item.
         */
        [HttpPost]
        public ActionResult PostActionItem(SiteActionItem actionItem)
        {
            if (ModelState.IsValid)
            {
                checkDB.SiteActionItems.Add(actionItem);
                checkDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
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


            //*********Using viewbag for 1 item
            var location_query = from l in checkDB.ws_locationView
                                 where l.LocationId == locationId
                                 select l;//should be only 1 location

            ws_locationView location_result = location_query.ToList()[0];

            ViewBag.Location = location_result.LocationName;
            //**********



            //**passing location by viewbag
            ViewBag.LocationId = locationId;
            //**


            var form_query = from f in checkDB.Forms
                             where f.Concept.Equals(location_result.Concept)
                             select f;

            //**** LOOK INTO FIRST OR DEFAULT SO NO LOOP

            int formID = -1;
            foreach (var x in form_query)
            {
                formID = x.FormID;
            }

            var section_query = from s in checkDB.Sections
                                where s.FormID == formID
                                orderby s.SectionOrder
                                select s;


            AnswerForm answer_form = new AnswerForm();

            answer_form.locationID = locationId;
            answer_form.formID = formID;

            int i = 0;
            int a = 0;
            foreach (var sq in section_query)
            {
                var question_query = from q in checkDB.Questions
                                     where q.SectionID == sq.SectionID
                                     && q.Active == true
                                     orderby q.QuestionOrder
                                     select q;

                foreach (var qq in question_query)
                {
                    answer_form.answerList.Add(new SiteAnswer());
                    answer_form.answerList[a].sectionName = sq.SectionName;
                    answer_form.answerList[a].question = qq;
                    answer_form.answerList[a].questionID = qq.QuestionID;
                    ++a;
                }
                ++i;
            }


            return View(answer_form);
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
         * Partial view to display the radio buttons
         */
        [ChildActionOnly]
        public ActionResult Answers(int question)
        {
            ViewBag.questionID = question;

            int size = checkDB.Questions.Count();

            ViewModel ans = new ViewModel();
            ans.answerList = new List<Answer>();


            for (int i = 0; i <= size; i++)
            {
                ans.answerList.Add(new Answer());
                ans.answerList[i].QuestionID = question;
            }

            /*
            Answer ans = new Answer();
            */

            return PartialView("Answers", ans);
        }

        /**
         * Author: Clayton
         * 
         * POST: AnswerForm
         */
        [HttpPost]
        public ActionResult SendConfirmation(AnswerForm answer_form)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewChecklist");
            }

            if (answer_form.dateModified == null)
            {
                SiteVisit visit = new SiteVisit();

                visit.SiteVisitID = checkDB.SiteVisits.Count() + 1;
                visit.LocationID = answer_form.locationID;
                visit.FormID = answer_form.formID;

                //**change to doreens code/date picker
                visit.dateOfVisit = DateTime.Now;
                //**

                checkDB.SiteVisits.Add(visit);
            }
            else
            {
                var site_query = from answer in checkDB.Answers
                                 select answer;
                
                //******** NEED SITE VISIT ID AND THEN WE UPDATE BASED ON THAT

                //checkDB.Answers.Single(p => p.SiteVisit.SiteVisitID == answer_form.) = answer_form.managerOnDuty;
                //visit.GeneralManager = answer_form.generalManager;
                //visit.CommentPublic = answer_form.publicComment;
                //visit.CommentPrivate = answer_form.privateComment;
            }


            checkDB.SaveChanges();

            int i = 0;
            foreach (var item in answer_form.answerList)
            {
                Answer temp_ans = new Answer();

                if (answer_form.dateModified == null)
                {

                    temp_ans.AnswerID = checkDB.Answers.Count() + 1;
                    //*********************************************************
                    //temp_ans.SiteVisitID = visit.SiteVisitID;
                    temp_ans.QuestionID = answer_form.answerList[i].questionID;
                    temp_ans.Rating = answer_form.answerList[i].value;
                    temp_ans.Comment = answer_form.answerList[i].comment;

                    checkDB.Answers.Add(temp_ans);
                }
                else
                {
                    checkDB.Answers.Single(p => p.AnswerID == temp_ans.AnswerID).Rating = answer_form.answerList[i].value;
                    checkDB.Answers.Single(p => p.AnswerID == temp_ans.AnswerID).Comment = answer_form.answerList[i].comment;
                }

                checkDB.SaveChanges();
                ++i;
            }

            return View();
        }


        /**
         * Author: Clayton
         * Modified by: Clayton
         * View to display the selected previous checklist
         */
        [HttpPost]
        public ActionResult OldChecklist(int siteID)
        {
            ViewBag.Message = "Old Checklist";//title of page

            int locationId = 1;


            SiteVisit current_site = (from sv in checkDB.SiteVisits
                                      where sv.SiteVisitID == siteID
                                      select sv).FirstOrDefault();

            //*********Using viewbag for 1 item
            ws_locationView location_result = (from l in checkDB.ws_locationView
                                               where l.LocationId == current_site.LocationID
                                               select l).FirstOrDefault();//should be only 1 location


            ViewBag.Location = location_result.LocationName;
            //**********



            //**passing location by viewbag
            ViewBag.LocationId = locationId;
            //**


            Form form = (from f in checkDB.Forms
                         where f.Concept.Equals(location_result.Concept)
                         select f).FirstOrDefault();

            //**** LOOK INTO FIRST OR DEFAULT SO NO LOOP

            int formID = form.FormID;


            var section_query = from s in checkDB.Sections
                                where s.FormID == formID
                                orderby s.SectionOrder
                                select s;


            AnswerForm answer_form = new AnswerForm();

            answer_form.locationID = locationId;
            answer_form.formID = formID;

            answer_form.generalManager = current_site.GeneralManager;
            answer_form.managerOnDuty = current_site.ManagerOnDuty;
            answer_form.publicComment = current_site.CommentPublic;
            answer_form.privateComment = current_site.CommentPrivate;
            answer_form.dateCreated = (DateTime)current_site.dateOfVisit;

            List<Answer> ans_query = (from aa in checkDB.Answers
                                      where aa.SiteVisit.SiteVisitID == current_site.SiteVisitID
                                      orderby aa.AnswerID
                                      select aa).ToList();



            int i = 0;
            int a = 0;
            foreach (var sq in section_query)
            {
                var question_query = from q in checkDB.Questions
                                     where q.SectionID == sq.SectionID
                                     && q.Active == true
                                     orderby q.QuestionOrder
                                     select q;

                foreach (var qq in question_query)
                {
                    answer_form.answerList.Add(new SiteAnswer());
                    answer_form.answerList[a].sectionName = sq.SectionName;
                    answer_form.answerList[a].question = qq;
                    answer_form.answerList[a].questionID = qq.QuestionID;
                    answer_form.answerList[a].value = ans_query[a].Rating;
                    answer_form.answerList[a].comment = ans_query[a].Comment;
                    ++a;
                }
                ++i;
            }


            return View(answer_form);
        }


    }
}
