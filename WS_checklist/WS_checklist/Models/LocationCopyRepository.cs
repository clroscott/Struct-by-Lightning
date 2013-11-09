using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WS_checklist.Models
{ 
    public class LocationCopyRepository : ILocationCopyRepository
    {
        ChecklistEntities context = new ChecklistEntities();

        public IQueryable<LocationCopy> All
        {
            get { return context.LocationCopies; }
        }

        public IQueryable<LocationCopy> AllIncluding(params Expression<Func<LocationCopy, object>>[] includeProperties)
        {
            IQueryable<LocationCopy> query = context.LocationCopies;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public LocationCopy Find(int id)
        {
            return context.LocationCopies.Find(id);
        }

        public void InsertOrUpdate(LocationCopy locationcopy)
        {
            if (locationcopy.LocationId == default(int)) {
                // New entity
                context.LocationCopies.Add(locationcopy);
            } else {
                // Existing entity
                context.Entry(locationcopy).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var locationcopy = context.LocationCopies.Find(id);
            context.LocationCopies.Remove(locationcopy);
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

    public interface ILocationCopyRepository : IDisposable
    {
        IQueryable<LocationCopy> All { get; }
        IQueryable<LocationCopy> AllIncluding(params Expression<Func<LocationCopy, object>>[] includeProperties);
        LocationCopy Find(int id);
        void InsertOrUpdate(LocationCopy locationcopy);
        void Delete(int id);
        void Save();
    }
}