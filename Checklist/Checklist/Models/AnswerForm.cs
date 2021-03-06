﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
    public class AnswerForm
    {
        public AnswerForm()
        {
            answerList = new List<SiteAnswer>();
            actionItems = new List<SiteActionItem>();
            for (int i = 0; i < 5; ++i)
            {
                actionItems.Add(new SiteActionItem());
            }
        }

        public string dateCreatedString { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }

        public int siteVisitID { get; set; }
        public int locationID { get; set; }
        public int formID { get; set; }

        public string publicComment { get; set; }
        public string privateComment { get; set; }
        public string managerOnDuty { get; set; }
        public string generalManager { get; set; }

        public List<SiteAnswer> answerList { get; set; }
        public List<SiteActionItem> actionItems { get; set; }
    }

    public class SiteAnswer
    {
        public int questionID { get; set; }
        public int siteAnswerID { get; set; }
        public string sectionName { get; set; }
        public Question question { get; set; }

        [Required(ErrorMessage = "Required")]
        public int value { get; set; }

        public string comment { get; set; }
    }
}