using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WS_checklist.Models
{ 
    public class SiteActionItemRepository : ISiteActionItemRepository
    {
        ChecklistEntities context = new ChecklistEntities();

        public IQueryable<SiteActionItem> All
        {
            get { return context.SiteActionItems; }
        }

        public IQueryable<SiteActionItem> AllIncluding(params Expression<Func<SiteActionItem, object>>[] includeProperties)
        {
            IQueryable<SiteActionItem> query = context.SiteActionItems;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SiteActionItem Find(int id)
        {
            return context.SiteActionItems.Find(id);
        }

        public void InsertOrUpdate(SiteActionItem siteactionitem)
        {
            if (siteactionitem.ActionID == default(int)) {
                // New entity
                context.SiteActionItems.Add(siteactionitem);
            } else {
                // Existing entity
                context.Entry(siteactionitem).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var siteactionitem = context.SiteActionItems.Find(id);
            context.SiteActionItems.Remove(siteactionitem);
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

    public interface ISiteActionItemRepository : IDisposable
    {
        IQueryable<SiteActionItem> All { get; }
        IQueryable<SiteActionItem> AllIncluding(params Expression<Func<SiteActionItem, object>>[] includeProperties);
        SiteActionItem Find(int id);
        void InsertOrUpdate(SiteActionItem siteactionitem);
        void Delete(int id);
        void Save();
    }
}