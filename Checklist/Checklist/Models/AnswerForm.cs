using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Checklist.Models
{
    public class AnswerForm
    {
        public int locationID { get; set; }
        public int formID { get; set; }
        public string publicComment { get; set; }
        public string privateComment { get; set; }
        public string managerOnDuty { get; set; }
        public string generalManager { get; set; }
        public List<SiteAnswer> answerList { get; set; }
    }

    public class SiteAnswer
    {
        public string sectionName { get; set; }
        public Question question { get; set; }
        public int value { get; set; }
        public string comment { get; set; }
    }
}