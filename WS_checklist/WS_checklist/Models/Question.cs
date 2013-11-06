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
    
    public partial class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }
    
        [Key]
        public int QuestionID { get; set; }
        public int SectionID { get; set; }
        public string QuestionName { get; set; }
        public bool Active { get; set; }
        public Nullable<int> QuestionOrder { get; set; }
    
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Section Section { get; set; }
    }
}
