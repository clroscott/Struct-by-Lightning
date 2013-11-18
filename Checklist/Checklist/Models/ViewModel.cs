using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Checklist.Models
{
    public class ViewModel
    {
        public ViewModel()
        {
            viewList = new List<viewInfo>();
        }

        public List<viewInfo> viewList { get; set; }
    }

    public class viewInfo
    {
        public ws_locationView location { get; set; }
        public DateTime lastVisit { get; set; }
    }
}
