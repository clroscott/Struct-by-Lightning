using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Checklist.Models;
using System.Data.Entity.Infrastructure;
using System.Net.Mail;
using System.Text;

namespace Checklist.Controllers
{
    public class CheckController : Controller
    {

        /**
         * The database entity
         */
        private ChecklistEntities ctx = new ChecklistEntities();


        /**
         * Author: Clayton
         * Modified by: Clayton, Aaron
         * View to display Locations of the user logged in
         */
        public ActionResult LocationList()
        {
            ViewBag.Message = "List of Locations";//title of page


            var query = from l in ctx.ws_locationView
                        where l.BusinessConsultant == User.Identity.Name
                        select l;

            return View(query);
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
            var queryLocationInfo = from a in ctx.ws_locationView
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


            var siteVistQuery = from s in ctx.SiteVisits
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
            var location_query = from l in ctx.ws_locationView
                                 where l.LocationId == locationId
                                 select l;//should be only 1 location

            ws_locationView location_result = location_query.ToList()[0];

            ViewBag.Location = location_result.LocationName;
            //**********



            //**passing location by viewbag
            ViewBag.LocationId = locationId;
            //**

            

            var form_query = from f in ctx.Forms
                             where f.Concept.Equals(location_result.Concept)
                             select f;

            //**** LOOK INTO FIRST OR DEFAULT SO NO LOOP

            int formID = -1;
            foreach (var x in form_query)
            {
                formID = x.FormID;
            }

            var section_query = from s in ctx.Sections
                                where s.FormID == formID
                                orderby s.SectionOrder
                                select s;


            AnswerForm answer_form = new AnswerForm();
            
            //values needed to be saved after the form is created
            answer_form.siteVisitID = ctx.SiteVisits.Count() + 1;
            answer_form.locationID = locationId;
            answer_form.formID = formID;

            int i = 0;
            int a = 0;
            foreach (var sq in section_query)
            {
                var question_query = from q in ctx.Questions
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


            SiteVisit visit = new SiteVisit();

            visit.SiteVisitID = answer_form.siteVisitID;
            visit.dateModified = answer_form.dateModified;


            //a null date has the year value of 1
            if (answer_form.dateModified.Year < 1000)
            {
                visit.LocationID = answer_form.locationID;
                visit.FormID = answer_form.formID;
                visit.ManagerOnDuty = answer_form.managerOnDuty;
                visit.GeneralManager = answer_form.generalManager;
                visit.CommentPublic = answer_form.publicComment;
                visit.CommentPrivate = answer_form.privateComment;

                //**change to doreens code/date picker
                visit.dateOfVisit = DateTime.Now;
                //**

                ctx.SiteVisits.Add(visit);
            }
            else
            {                
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).ManagerOnDuty = answer_form.managerOnDuty;
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).GeneralManager = answer_form.generalManager;
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).CommentPublic = answer_form.publicComment;
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).CommentPrivate = answer_form.privateComment;
            }


            ctx.SaveChanges();

            int i = 0;
            foreach (var item in answer_form.answerList)
            {
                Answer temp_ans = new Answer();

                if (answer_form.dateModified.Year < 1000)
                {
                    temp_ans.AnswerID = ctx.Answers.Count() + 1;
                    temp_ans.SiteVisitID = answer_form.siteVisitID;
                    temp_ans.QuestionID = answer_form.answerList[i].questionID;
                    temp_ans.Rating = answer_form.answerList[i].value;
                    temp_ans.Comment = answer_form.answerList[i].comment;

                    ctx.Answers.Add(temp_ans);
                }
                else
                {
                    var temp_ans_query = (from ans in ctx.Answers
                                    where ans.SiteVisitID == answer_form.siteVisitID
                                    select ans).ToList();

                    temp_ans.AnswerID = temp_ans_query[i].AnswerID;

                    ctx.Answers.Single(p => p.AnswerID == temp_ans.AnswerID).Rating = answer_form.answerList[i].value;
                    ctx.Answers.Single(p => p.AnswerID == temp_ans.AnswerID).Comment = answer_form.answerList[i].comment;
                }

                ctx.SaveChanges();

                ++i;
            }

            //email code starts here
            ws_locationView location = (from l in ctx.ws_locationView
                                        where l.LocationId == answer_form.locationID
                                        select l).FirstOrDefault();

            string email = location.Email;
            string businessConsultant = location.BusinessConsultant;
            string locationName = location.LocationName;

            SiteVisit sitevist = (from s in ctx.SiteVisits
                                  where s.SiteVisitID == visit.SiteVisitID
                                  select s).FirstOrDefault();

            String dateCreated = ((DateTime)sitevist.dateOfVisit).ToString("MM-dd-yyy");
            //DateTime dateTimeModified;

            Boolean isModified = false;

            //if (dateTimeModified.Year > 1000)
            //{
            //dateTimeModified = (DateTime)sitevist.dateModified;
            //String dateModified = dateTimeModified.ToString("MM-dd-yyy");
            //isModified = true;
            //}

            string managerOnDuty = sitevist.ManagerOnDuty;
            string generalManager = sitevist.GeneralManager;
            string publicComment = sitevist.CommentPublic;


            int formID = (from f in ctx.Forms
                          where f.Concept.Equals(location.Concept)
                          select f).FirstOrDefault().FormID;

            var section_query = from s in ctx.Sections
                                where s.FormID == formID
                                orderby s.SectionOrder
                                select s;

            List<Answer> ans_query = (from aa in ctx.Answers
                                      where aa.SiteVisit.SiteVisitID == sitevist.SiteVisitID
                                      orderby aa.AnswerID
                                      select aa).ToList();

            using (var smtpClient = new SmtpClient())
            {
                String mailFromAddress = "testing2013101@gmail.com";//email we made to create smtp server, change in web.config if nessesary
                String mailToAddress = "doreency@gmail.com"; //String mailToAddress = email; *******THIS CODE TO MAKE IT NOT HARD CODED*******

                StringBuilder body = new StringBuilder()
                .AppendLine("<html><body><h1 style='font-family:georgia;'>WhiteSpot Site Visit Report</h1>")
                .AppendLine("<table>")
                .AppendLine("<tr><td width='215px'><b>Location: </b></td><td>" + locationName + "</td></tr>")
                .AppendLine("<tr><td><b>Date of Site Visit: </b></td><td>" + dateCreated + "</td></tr>");
                //if (isModified)
                //{
                //body.AppendLine("<tr><td><b>Date Modifed: </b></td><td>" + dateModified + "</td></tr>");
                //}
                body.AppendLine("<tr><td><b>Business Consultant: </b></td><td>" + businessConsultant + "</td></tr>")
                .AppendLine("<tr><td><b>Manager on Duty: </b></td><td>" + managerOnDuty + "</td></tr>")
                .AppendLine("<tr><td><b>General Manager: </b></td><td>" + generalManager + "</td></tr>")
                .AppendLine("</table>");
                String[] rating = { "", "Poor", "Good", "Excellent", "N/A" };
                int a = 0;
                foreach (var sq in section_query)
                {
                    var question_query = from q in ctx.Questions
                                         where q.SectionID == sq.SectionID
                                         && q.Active == true
                                         orderby q.QuestionOrder
                                         select q;
                    body.AppendLine("<table>");
                    body.AppendLine("<tr><td width='215px'><h3>" + sq.SectionName + "</h3></td><td></td><td></td></tr>");

                    foreach (var qq in question_query)
                    {
                        body.AppendLine("<tr><td><b>" + qq.QuestionName + ": </b></td>");
                        body.AppendLine("<td width='100px'>" + rating[ans_query[a].Rating] + "</td>");
                        body.AppendLine("<td>" + ans_query[a].Comment + "</td></tr>");
                        ++a;
                    }
                    body.AppendLine("</table>");
                }

                body.AppendLine("<br /><h3>Overall Comments:</h3><br /> " + publicComment);
                body.AppendLine("</body></html>");

                MailMessage mailMessage = new MailMessage(mailFromAddress, mailToAddress);
                if (isModified)
                {
                    mailMessage.Subject = "*MODIFICATION* WhiteSpot Site Visit Report: " + location + " " + dateCreated;
                }
                else
                {
                    mailMessage.Subject = "WhiteSpot Site Visit Report: " + locationName + " " + dateCreated;
                }

                mailMessage.Body = body.ToString();
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
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

            


            SiteVisit current_site = (from sv in ctx.SiteVisits
                                      where sv.SiteVisitID == siteID
                                      select sv).FirstOrDefault();

            //*********Using viewbag for 1 item
            ws_locationView location_result = (from l in ctx.ws_locationView
                                               where l.LocationId == current_site.LocationID
                                               select l).FirstOrDefault();//should be only 1 location


            ViewBag.Location = location_result.LocationName;
            //**********

            int locationId = location_result.LocationId;

            //**passing location by viewbag
            ViewBag.LocationId = locationId;
            //**


            Form form = (from f in ctx.Forms
                         where f.Concept.Equals(location_result.Concept)
                         select f).FirstOrDefault();

            //**** LOOK INTO FIRST OR DEFAULT SO NO LOOP

            int formID = form.FormID;


            var section_query = from s in ctx.Sections
                                where s.FormID == formID
                                orderby s.SectionOrder
                                select s;


            AnswerForm answer_form = new AnswerForm();

            answer_form.locationID = locationId;
            answer_form.formID = formID;
            answer_form.dateModified = DateTime.Now;
            answer_form.siteVisitID = siteID;

            answer_form.generalManager = current_site.GeneralManager;
            answer_form.managerOnDuty = current_site.ManagerOnDuty;
            answer_form.publicComment = current_site.CommentPublic;
            answer_form.privateComment = current_site.CommentPrivate;
            answer_form.dateCreated = (DateTime)current_site.dateOfVisit;

            List<Answer> ans_query = (from aa in ctx.Answers
                                      where aa.SiteVisit.SiteVisitID == current_site.SiteVisitID
                                      orderby aa.AnswerID
                                      select aa).ToList();



            int i = 0;
            int a = 0;
            foreach (var sq in section_query)
            {
                var question_query = from q in ctx.Questions
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



        /**
         * Author:Aaron
         * Modified by: Aaron, Aleeza
         * Partial view to display action items.
         */
        public PartialViewResult _ActionItemsPartial(int loc)
        {
            var query = from l in ctx.SiteActionItems
                        where l.LocationID == loc
                        && l.Complete == false
                        select l;
            Session["loc"] = loc;
            return PartialView("_ActionItems", query);
        }

        /**
         * Author:Aleeza
         * Partial view for the displaying and hiding of
         * completed action items.
         */
        public PartialViewResult _ActionItemsComplete(int loc)
        {
            var query = from l in ctx.SiteActionItems
                        where l.LocationID == loc
                        && l.Complete == false
                        select l;
            Session["loc"] = loc;
            return PartialView("_ActionItemsComplete", query);
        }

        /**
        * Author: Aaron
        * Modified by: Aaron
        * Creates an Action Item for the partial view on location info page
        */
        public PartialViewResult createAction(int loc)
        {

            SiteActionItem temp = new SiteActionItem() { LocationID = loc };

            return PartialView("_AddActionItems", temp);
        }

        /**
         * Author: Aaron
         * Modified by: Aaron, Aleeza
         * Submits action item into database from location info page
         */
        [ValidateAntiForgeryToken]
        public PartialViewResult ActionItemSubmit(SiteActionItem actionItem)
        {
            actionItem.ActionID = ctx.SiteActionItems.Count() + 1;
            actionItem.DateCreated = DateTime.Now;
            actionItem.SiteVisitID = 0;
            try
            {
                ctx.SiteActionItems.Add(actionItem);
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                IEnumerable<SiteActionItem> itemsCaught = ctx.SiteActionItems.Where(i => (i.LocationID == actionItem.LocationID && i.Complete == false)).ToList();
                return PartialView("_ActionItems", itemsCaught);
            }
            IEnumerable<SiteActionItem> items = ctx.SiteActionItems.Where(i => (i.LocationID == actionItem.LocationID && i.Complete == false)).ToList();

            return PartialView("_ActionItems", items);
        }

        public PartialViewResult listAction(SiteActionItem actionItem)
        {
            return PartialView("_DisplayActionItems", actionItem);
        }

        /**
         * Author: Aaron
         * Modified By: Aaron
         * Ajax operation to complete an action item
         * returns partial view of the action items as well as the
         * updated list of incomplete action items
         */
        [ValidateAntiForgeryToken]
        public PartialViewResult ActionItemComplete(SiteActionItem actionItem)
        {
            ctx.SiteActionItems.Single(i => i.ActionID == actionItem.ActionID).Complete = true;
            ctx.SaveChanges();
            IEnumerable<SiteActionItem> items = ctx.SiteActionItems.Where(i => (i.LocationID == actionItem.LocationID && i.Complete == false)).ToList();
            return PartialView("_ActionItems", items);
        }

        public PartialViewResult completeAction(int loc)
        {
            IEnumerable<SiteActionItem> items = ctx.SiteActionItems.Where(i => (i.LocationID == loc && i.Complete == true)).ToList();
            return PartialView("_DisplayCompleteItems", items);
        }

        public PartialViewResult hideAction()
        {
            return PartialView("_HideCompleteItems");
        }


    }
}
