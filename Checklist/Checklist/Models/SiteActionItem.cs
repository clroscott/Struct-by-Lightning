//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Checklist.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiteActionItem
    {
        public int ActionID { get; set; }
        public Nullable<int> SiteVisitID { get; set; }
        public int LocationID { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public bool Complete { get; set; }
        public Nullable<System.DateTime> DateComplete { get; set; }
    
        public virtual SiteVisit SiteVisit { get; set; }
    }
}
