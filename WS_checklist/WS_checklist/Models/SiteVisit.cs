//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS_checklist.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class SiteVisit
    {
        public SiteVisit()
        {
            this.Answers = new HashSet<Answer>();
            this.SiteActionItems = new HashSet<SiteActionItem>();
        }

        [Key]
        public int SiteVisitID { get; set; }
        public int LocationID { get; set; }
        public int FormID { get; set; }
        public Nullable<System.DateTime> dateOfVisit { get; set; }
        public Nullable<System.DateTime> dateModified { get; set; }
        public string CommentPublic { get; set; }
        public string CommentPrivate { get; set; }
    
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Form Form { get; set; }
        public virtual LocationCopy LocationCopy { get; set; }
        public virtual ICollection<SiteActionItem> SiteActionItems { get; set; }
    }
}
