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
    public class AccountBalanceRepository : IAccountBalanceRepository
    {
        IncomesAndOutcomes_APIContext context = new IncomesAndOutcomes_APIContext();

        public IQueryable<AccountBalance> All
        {
            get { return context.AccountBalances; }
        }

        public IQueryable<AccountBalance> AllIncluding(params Expression<Func<AccountBalance, object>>[] includeProperties)
        {
            IQueryable<AccountBalance> query = context.AccountBalances;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AccountBalance Find(int id)
        {
            return context.AccountBalances.Find(id);
        }

        public void InsertOrUpdate(AccountBalance accountbalance)
        {
            if (accountbalance.AccountBalanceId == default(int)) {
                // New entity
                context.AccountBalances.Add(accountbalance);
            } else {
                // Existing entity
                context.Entry(accountbalance).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var accountbalance = context.AccountBalances.Find(id);
            context.AccountBalances.Remove(accountbalance);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IAccountBalanceRepository
    {
        IQueryable<AccountBalance> All { get; }
        IQueryable<AccountBalance> AllIncluding(params Expression<Func<AccountBalance, object>>[] includeProperties);
        AccountBalance Find(int id);
        void InsertOrUpdate(AccountBalance accountbalance);
        void Delete(int id);
        void Save();
    }
}