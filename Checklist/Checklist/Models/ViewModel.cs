using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Checklist.Models
{
    public class ViewModel
    {
        public Answer[] answerList { get; set; }

        public ViewModel()
        {
            answerList = new Answer[58];
        }

        public ViewModel(int count)
        {
            answerList = new Answer[count];
        }
    }
}
