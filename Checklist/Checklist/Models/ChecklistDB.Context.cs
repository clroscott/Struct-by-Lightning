﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ChecklistEntities : DbContext
    {
        public ChecklistEntities()
            : base("name=ChecklistEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<LocationCopy> LocationCopies { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SiteActionItem> SiteActionItems { get; set; }
        public DbSet<SiteVisit> SiteVisits { get; set; }
        public DbSet<ws_locations> ws_locations { get; set; }
        public DbSet<ws_locationView> ws_locationView { get; set; }
    }
}
