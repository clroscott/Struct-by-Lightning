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

            IEnumerable<ws_locationView> location_query;
            if (User.Identity.Name == "admin")
            {
                location_query = from l in ctx.ws_locationView
                                 select l;
            }
            else
            {
                location_query = from l in ctx.ws_locationView
                                 where l.BusinessConsultant == User.Identity.Name
                                 select l;
            }


            List<KeyValuePair<DateTime, ws_locationView>> dates = new List<KeyValuePair<DateTime, ws_locationView>>();
            ws_locationView[] locations = new ws_locationView[location_query.Count()];
            ViewModel viewmodel = new ViewModel();
            int i = 0;
            foreach (var item in location_query)
            {
                locations[i] = item;
                var date_query = from v in ctx.SiteVisits
                                 where v.LocationID == item.LocationId
                                 orderby v.dateOfVisit descending
                                 select v;
                DateTime date;
                if (date_query.FirstOrDefault() != null)
                {
                    date = (DateTime)date_query.FirstOrDefault().dateOfVisit;
                }
                else
                {
                    date = Convert.ToDateTime("1/1/0001");
                }

                KeyValuePair<DateTime, ws_locationView> temp_key = new KeyValuePair<DateTime, ws_locationView>(date, item);

                dates.Add(temp_key);
                ++i;
            }


            dates = dates.OrderBy(x => x.Key).ToList();

            i = 0;
            ws_locationView[] result = new ws_locationView[location_query.Count()];//i think this array does nothing
            foreach (var item in dates)
            {
                result[i] = item.Value;//i think this array does nothing
                viewInfo temp_info = new viewInfo();
                temp_info.location = item.Value;
                temp_info.lastVisit = item.Key;
                viewmodel.viewList.Add(temp_info);
                ++i;
            }

            return View(viewmodel);
        }




        /**
         * Author: Clayton
         * Modified by: Clayton, Aaron
         * View to display information of chosen location
         */
        public ActionResult LocationInfo(int locationId)
        {
            ViewBag.Message = "Location Information";//title of page

            //Query to grab the location information
            var location_query = from a in ctx.ws_locationView
                                 where a.LocationId == locationId
                                 select a;



            return View(location_query);
        }



        /**
         * Author: Clayton
         * Modified by: Clayton, Jung
         * View to display previous checklists of a location
         */
        public ActionResult PreviousChecklists(int locationId)
        {
            ViewBag.Message = " Previous Checklists";//title of page

            //**** fix this to be not a viewbag
            ViewBag.locationid = locationId;
            //*****

            var siteVistQuery = from s in ctx.SiteVisits
                                where s.LocationID == locationId
                                orderby s.dateOfVisit descending
                                select s;


            return View(siteVistQuery);
        }


        /**
         * Author: Clayton
         * Modified by: Clayton, Aleeza
         * View to create a new checklists of a location
         */
        public ActionResult NewChecklist(int locationId)
        {
            ViewBag.Message = "New Checklist";//title of page


            var location_query = from l in ctx.ws_locationView
                                 where l.LocationId == locationId
                                 select l;//should be only 1 location

            ws_locationView location_result = location_query.FirstOrDefault();

            ViewBag.Location = location_result.LocationName; //Location name to be displayed



            var form_query = from f in ctx.Forms
                             where f.Concept.Equals(location_result.Concept)
                             select f;

            int formID = form_query.FirstOrDefault().FormID;


            var section_query = from s in ctx.Sections
                                where s.FormID == formID
                                orderby s.SectionOrder
                                select s;


            AnswerForm answer_form = new AnswerForm();

            //values needed to be saved after the form is created
            answer_form.siteVisitID = ctx.SiteVisits.Count() + 1;
            answer_form.locationID = locationId;
            answer_form.formID = formID;
            answer_form.dateCreatedString = DateTime.Now.ToString("dd/MM/yyyy");

            int a = 0;
            foreach (var sq in section_query) //adds all section and question information to the model
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

                visit.dateOfVisit = Convert.ToDateTime(answer_form.dateCreatedString);

                ctx.SiteVisits.Add(visit);
            }
            else
            {
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).ManagerOnDuty = answer_form.managerOnDuty;
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).GeneralManager = answer_form.generalManager;
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).CommentPublic = answer_form.publicComment;
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).CommentPrivate = answer_form.privateComment;
                ctx.SiteVisits.Single(p => p.SiteVisitID == answer_form.siteVisitID).dateModified = DateTime.Now;
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

            foreach (var action in answer_form.actionItems)
            {
                if (action.Description == null)
                {
                    continue;
                }

                SiteActionItem temp = new SiteActionItem();

                temp.ActionID = ctx.SiteActionItems.Count() + 1;
                temp.Complete = false;
                temp.DateCreated = Convert.ToDateTime(answer_form.dateCreatedString);
                temp.SiteVisitID = answer_form.siteVisitID;
                temp.LocationID = answer_form.locationID;
                temp.Description = action.Description;

                ctx.SiteActionItems.Add(temp);
                ctx.SaveChanges();
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
                String mailToAddress = email;

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

                body.AppendLine("<br /><h3>Follow-up items that require attention</h3>");


                var action_query = from aq in ctx.SiteActionItems
                                   where aq.Complete == false
                                   && aq.LocationID == answer_form.locationID
                                   select aq;

                body.AppendLine("<table>");

                int j = 1;

                foreach (var act in action_query)
                {
                    body.AppendLine("<tr>" + j + ". " + act.Description + "</tr>");
                    j++;
                }
                body.AppendLine("</table>");

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



            return View(location);
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


            ws_locationView location_result = (from l in ctx.ws_locationView
                                               where l.LocationId == current_site.LocationID
                                               select l).FirstOrDefault();//should be only 1 location


            ViewBag.Location = location_result.LocationName;  //Location name to be displayed

            int locationId = location_result.LocationId;



            Form form = (from f in ctx.Forms
                         where f.Concept.Equals(location_result.Concept)
                         select f).FirstOrDefault();


            int formID = form.FormID;


            var section_query = from s in ctx.Sections
                                where s.FormID == formID
                                orderby s.SectionOrder
                                select s;


            AnswerForm answer_form = new AnswerForm();

            answer_form.locationID = locationId;
            answer_form.formID = formID;

            DateTime temp_date = Convert.ToDateTime("1/1/0001");

            if (current_site.dateModified != null)
            {
                temp_date = (DateTime)current_site.dateModified;
            }


            if (temp_date.Year > 1000)
            {
                answer_form.dateModified = (DateTime)current_site.dateModified;
            }
            else
            {
                answer_form.dateModified = Convert.ToDateTime("1/1/0001");
            }
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



            int a = 0;
            foreach (var sq in section_query)
            {
                var question_query = from q in ctx.Questions
                                     where q.SectionID == sq.SectionID
                                     orderby q.QuestionOrder
                                     select q;
                


                foreach (var qq in question_query)
                {
                    if (ans_query.Count <= a)
                    {
                        break;
                    }
                    if (ans_query[a].QuestionID != qq.QuestionID)
                    {
                        continue;
                    }

                    answer_form.answerList.Add(new SiteAnswer());
                    answer_form.answerList[a].sectionName = sq.SectionName;
                    answer_form.answerList[a].question = qq;
                    answer_form.answerList[a].questionID = qq.QuestionID;
                    answer_form.answerList[a].value = ans_query[a].Rating;
                    answer_form.answerList[a].comment = ans_query[a].Comment;
                    ++a;
                }
            }

            var action_query = from ai in ctx.SiteActionItems
                               where ai.SiteVisitID == current_site.SiteVisitID
                               select ai;

            int i = 0;
            foreach (var action in action_query)
            {
                answer_form.actionItems[i] = action;
                i++;
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
                        && l.Complete == true
                        select l;
            Session["loc"] = loc;
            if (query.Count() > 0)
            {
                Session["count"] = 5;
            }
            else
            {
                Session["count"] = 0;
            }
            return PartialView("_ActionItemsComplete");
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
                Console.WriteLine(e.StackTrace);
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
            ctx.SiteActionItems.Single(i => i.ActionID == actionItem.ActionID).DateComplete = DateTime.Now;
            ctx.SaveChanges();
            IEnumerable<SiteActionItem> items = ctx.SiteActionItems.Where(i => (i.LocationID == actionItem.LocationID && i.Complete == false)).ToList();
            return PartialView("_ActionItems", items);
        }

        public PartialViewResult completeAction(int loc)
        {
            List<SiteActionItem> items = ctx.SiteActionItems.Where(i => (i.LocationID == loc && i.Complete == true)).ToList();

            items = items.OrderByDescending(x => x.DateComplete).ToList();

            return PartialView("_DisplayCompleteItems", items);
        }

        public PartialViewResult hideAction()
        {
            return PartialView("_ActionItemsComplete");
        }


        [ChildActionOnly]
        public PartialViewResult completeActionNew(int locationID)
        {
            var action_query = from sa in ctx.SiteActionItems
                               where sa.LocationID == locationID
                               && sa.Complete == false
                               select sa;



            return PartialView("_CompleteActionNew", action_query);
        }

        public PartialViewResult actionRow(SiteActionItem actionItem)
        {
            return PartialView("_actionRow", actionItem);
        }

        public PartialViewResult ajaxFinish(int actID, int locID)
        {
            ctx.SiteActionItems.Single(i => i.ActionID == actID).Complete = true;
            ctx.SiteActionItems.Single(i => i.ActionID == actID).DateComplete = DateTime.Now;
            ctx.SaveChanges();

            var action_query = from sa in ctx.SiteActionItems
                               where sa.LocationID == locID
                               && sa.Complete == false
                               select sa;


            return PartialView("_CompleteActionNew", action_query);
        }
        /**
         * Author: Aaron
         * Modified By:
         * Increments counter for number of action items to
         * display and send the list back to the view for
         * page redisplay
         */
        public PartialViewResult showMore(int loc)
        {
            
            List<SiteActionItem> items = ctx.SiteActionItems.Where(i => (i.LocationID == loc && i.Complete == true)).ToList();

            items = items.OrderByDescending(x => x.DateComplete).ToList();

            if (items.Count() > 0)
            {
                Session["count"] = 5 + (int)Session["count"];
            }

            return PartialView("_DisplayCompleteItems", items);

        }

    }
}
