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
    public class IncomeRepository : IIncomeRepository
    {
        IncomesAndOutcomes_APIContext context = new IncomesAndOutcomes_APIContext();

        public IQueryable<Income> All
        {
            get { return context.Incomes; }
        }

        public IQueryable<Income> AllIncluding(params Expression<Func<Income, object>>[] includeProperties)
        {
            IQueryable<Income> query = context.Incomes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Income Find(int id)
        {
            return context.Incomes.Find(id);
        }

        public void InsertOrUpdate(Income income)
        {
            if (income.IncomeId == default(int)) {
                // New entity
                context.Incomes.Add(income);
            } else {
                // Existing entity
                context.Entry(income).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var income = context.Incomes.Find(id);
            context.Incomes.Remove(income);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IIncomeRepository
    {
        IQueryable<Income> All { get; }
        IQueryable<Income> AllIncluding(params Expression<Func<Income, object>>[] includeProperties);
        Income Find(int id);
        void InsertOrUpdate(Income income);
        void Delete(int id);
        void Save();
    }
}