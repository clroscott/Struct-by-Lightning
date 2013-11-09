using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WS_checklist.Models
{ 
    public class FormRepository : IFormRepository
    {
        ChecklistEntities context = new ChecklistEntities();

        public IQueryable<Form> All
        {
            get { return context.Forms; }
        }

        public IQueryable<Form> AllIncluding(params Expression<Func<Form, object>>[] includeProperties)
        {
            IQueryable<Form> query = context.Forms;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Form Find(int id)
        {
            return context.Forms.Find(id);
        }

        public void InsertOrUpdate(Form form)
        {
            if (form.FormID == default(int)) {
                // New entity
                context.Forms.Add(form);
            } else {
                // Existing entity
                context.Entry(form).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var form = context.Forms.Find(id);
            context.Forms.Remove(form);
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

    public interface IFormRepository : IDisposable
    {
        IQueryable<Form> All { get; }
        IQueryable<Form> AllIncluding(params Expression<Func<Form, object>>[] includeProperties);
        Form Find(int id);
        void InsertOrUpdate(Form form);
        void Delete(int id);
        void Save();
    }
}