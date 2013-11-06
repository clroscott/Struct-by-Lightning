using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WS_checklist.Models
{ 
    public class SiteVisitRepository : ISiteVisitRepository
    {
        ChecklistEntities context = new ChecklistEntities();

        public IQueryable<SiteVisit> All
        {
            get { return context.SiteVisits; }
        }

        public IQueryable<SiteVisit> AllIncluding(params Expression<Func<SiteVisit, object>>[] includeProperties)
        {
            IQueryable<SiteVisit> query = context.SiteVisits;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SiteVisit Find(int id)
        {
            return context.SiteVisits.Find(id);
        }

        public void InsertOrUpdate(SiteVisit sitevisit)
        {
            if (sitevisit.SiteVisitID == default(int)) {
                // New entity
                context.SiteVisits.Add(sitevisit);
            } else {
                // Existing entity
                context.Entry(sitevisit).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var sitevisit = context.SiteVisits.Find(id);
            context.SiteVisits.Remove(sitevisit);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ISiteVisitRepository : IDisposable
    {
        IQueryable<SiteVisit> All { get; }
        IQueryable<SiteVisit> AllIncluding(params Expression<Func<SiteVisit, object>>[] includeProperties);
        SiteVisit Find(int id);
        void InsertOrUpdate(SiteVisit sitevisit);
        void Delete(int id);
        void Save();
    }
}