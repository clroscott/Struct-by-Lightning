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
    
    public partial class LocationCopy
    {
        public LocationCopy()
        {
            this.SiteActionItems = new HashSet<SiteActionItem>();
            this.SiteVisits = new HashSet<SiteVisit>();
        }
    
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string BusinessConsultant { get; set; }
        public string Province { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<SiteActionItem> SiteActionItems { get; set; }
        public virtual ICollection<SiteVisit> SiteVisits { get; set; }
    }
}