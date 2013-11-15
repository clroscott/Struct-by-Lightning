using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Checklist.Models
{
    public class AnswerForm
    {
        public string publicComment { get; set; }
        public string privateComment { get; set; }
        public string managerOnDuty { get; set; }
        public string gm { get; set; }

        public IEnumerable<SiteAnswer> answerList { get; set; }
    }

    public class SiteAnswer
    {
        public Question question { get; set; }
        public int value { get; set; }
        public string comment { get; set; }
    }
}
