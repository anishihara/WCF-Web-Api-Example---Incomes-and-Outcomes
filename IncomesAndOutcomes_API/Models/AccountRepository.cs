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
    public class AccountRepository : IAccountRepository
    {
        IncomesAndOutcomes_APIContext context = new IncomesAndOutcomes_APIContext();

        public IQueryable<Account> All
        {
            get { return context.Accounts; }
        }

        public IQueryable<Account> AllIncluding(params Expression<Func<Account, object>>[] includeProperties)
        {
            IQueryable<Account> query = context.Accounts;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Account Find(int id)
        {
            return context.Accounts.Find(id);
        }

        public void InsertOrUpdate(Account account)
        {
            if (account.Id == default(int)) {
                // New entity
                context.Accounts.Add(account);
            } else {
                // Existing entity
                context.Entry(account).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var account = context.Accounts.Find(id);
            context.Accounts.Remove(account);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IAccountRepository
    {
        IQueryable<Account> All { get; }
        IQueryable<Account> AllIncluding(params Expression<Func<Account, object>>[] includeProperties);
        Account Find(int id);
        void InsertOrUpdate(Account account);
        void Delete(int id);
        void Save();
    }
}