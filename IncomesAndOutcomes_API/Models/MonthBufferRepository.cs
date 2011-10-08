using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Models
{ 
    public class MonthBufferRepository : IMonthBufferRepository
    {
        IncomesAndOutcomes_APIContext context = new IncomesAndOutcomes_APIContext();

        public IQueryable<MonthBuffer> All
        {
            get { return context.MonthBuffers; }
        }

        public IQueryable<MonthBuffer> AllIncluding(params Expression<Func<MonthBuffer, object>>[] includeProperties)
        {
            IQueryable<MonthBuffer> query = context.MonthBuffers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public MonthBuffer Find(int id)
        {
            return context.MonthBuffers.Find(id);
        }

        public void InsertOrUpdate(MonthBuffer monthbuffer)
        {
            if (monthbuffer.MonthBufferId == default(int)) {
                // New entity
                context.MonthBuffers.Add(monthbuffer);
            } else {
                // Existing entity
                context.Entry(monthbuffer).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var monthbuffer = context.MonthBuffers.Find(id);
            context.MonthBuffers.Remove(monthbuffer);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IMonthBufferRepository
    {
        IQueryable<MonthBuffer> All { get; }
        IQueryable<MonthBuffer> AllIncluding(params Expression<Func<MonthBuffer, object>>[] includeProperties);
        MonthBuffer Find(int id);
        void InsertOrUpdate(MonthBuffer monthbuffer);
        void Delete(int id);
        void Save();
    }
}