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
    public class OutcomeRepository : IOutcomeRepository
    {
        IncomesAndOutcomes_APIContext context = new IncomesAndOutcomes_APIContext();

        public IQueryable<Outcome> All
        {
            get { return context.Outcomes; }
        }

        public IQueryable<Outcome> AllIncluding(params Expression<Func<Outcome, object>>[] includeProperties)
        {
            IQueryable<Outcome> query = context.Outcomes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Outcome Find(int id)
        {
            return context.Outcomes.Find(id);
        }

        public void InsertOrUpdate(Outcome outcome)
        {
            if (outcome.Id == default(int)) {
                // New entity
                context.Outcomes.Add(outcome);
            } else {
                // Existing entity
                context.Entry(outcome).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var outcome = context.Outcomes.Find(id);
            context.Outcomes.Remove(outcome);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IOutcomeRepository
    {
        IQueryable<Outcome> All { get; }
        IQueryable<Outcome> AllIncluding(params Expression<Func<Outcome, object>>[] includeProperties);
        Outcome Find(int id);
        void InsertOrUpdate(Outcome outcome);
        void Delete(int id);
        void Save();
    }
}