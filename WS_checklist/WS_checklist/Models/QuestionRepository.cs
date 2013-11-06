using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WS_checklist.Models
{ 
    public class QuestionRepository : IQuestionRepository
    {
        ChecklistEntities context = new ChecklistEntities();

        public IQueryable<Question> All
        {
            get { return context.Questions; }
        }

        public IQueryable<Question> AllIncluding(params Expression<Func<Question, object>>[] includeProperties)
        {
            IQueryable<Question> query = context.Questions;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Question Find(int id)
        {
            return context.Questions.Find(id);
        }

        public void InsertOrUpdate(Question question)
        {
            if (question.QuestionID == default(int)) {
                // New entity
                context.Questions.Add(question);
            } else {
                // Existing entity
                context.Entry(question).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var question = context.Questions.Find(id);
            context.Questions.Remove(question);
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

    public interface IQuestionRepository : IDisposable
    {
        IQueryable<Question> All { get; }
        IQueryable<Question> AllIncluding(params Expression<Func<Question, object>>[] includeProperties);
        Question Find(int id);
        void InsertOrUpdate(Question question);
        void Delete(int id);
        void Save();
    }
}