using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace IncomesAndOutcomes_API.Models
{ 
    public class UserSessionRepository : IUserSessionRepository
    {
        IncomesAndOutcomes_APIContext context = new IncomesAndOutcomes_APIContext();

        public IQueryable<UserSession> All
        {
            get { return context.UserSessions; }
        }

        public IQueryable<UserSession> AllIncluding(params Expression<Func<UserSession, object>>[] includeProperties)
        {
            IQueryable<UserSession> query = context.UserSessions;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public UserSession Find(System.Guid id)
        {
            return context.UserSessions.Find(id);
        }

        public void InsertOrUpdate(UserSession usersession)
        {
            if (usersession.Id == default(System.Guid)) {
                // New entity
                usersession.Id = Guid.NewGuid();
                context.UserSessions.Add(usersession);
            } else {
                // Existing entity
                context.Entry(usersession).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var usersession = context.UserSessions.Find(id);
            context.UserSessions.Remove(usersession);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IUserSessionRepository
    {
        IQueryable<UserSession> All { get; }
        IQueryable<UserSession> AllIncluding(params Expression<Func<UserSession, object>>[] includeProperties);
        UserSession Find(System.Guid id);
        void InsertOrUpdate(UserSession usersession);
        void Delete(System.Guid id);
        void Save();
    }
}