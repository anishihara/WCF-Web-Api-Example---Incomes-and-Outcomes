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
    public class BudgetRepository : IBudgetRepository
    {
        IncomesAndOutcomes_APIContext context = new IncomesAndOutcomes_APIContext();

        public IQueryable<Budget> All
        {
            get { return context.Budgets; }
        }

        public IQueryable<Budget> AllIncluding(params Expression<Func<Budget, object>>[] includeProperties)
        {
            IQueryable<Budget> query = context.Budgets;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Budget Find(int id)
        {
            return context.Budgets.Find(id);
        }

        public void InsertOrUpdate(Budget budget)
        {
            if (budget.Id == default(int)) {
                // New entity
                context.Budgets.Add(budget);
            } else {
                // Existing entity
                context.Entry(budget).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var budget = context.Budgets.Find(id);
            context.Budgets.Remove(budget);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IBudgetRepository
    {
        IQueryable<Budget> All { get; }
        IQueryable<Budget> AllIncluding(params Expression<Func<Budget, object>>[] includeProperties);
        Budget Find(int id);
        void InsertOrUpdate(Budget budget);
        void Delete(int id);
        void Save();
    }
}