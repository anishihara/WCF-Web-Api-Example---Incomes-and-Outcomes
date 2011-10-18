using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Tests.RepositoryMockups
{
    class UserSessionRepositoryMockup : IUserSessionRepository
    {
        private List<UserSession> UserSessions { get; set; }
        private List<UserSession> UserSessionsToAdd { get; set; }
        public UserSessionRepositoryMockup()
        {
            UserSessions = new List<UserSession>();
            UserSessionsToAdd = new List<UserSession>();

        }

        public IQueryable<UserSession> All
        {
            get { return UserSessions.AsQueryable<UserSession>(); }
        }

        public IQueryable<UserSession> AllIncluding(params System.Linq.Expressions.Expression<Func<UserSession, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public UserSession Find(Guid id)
        {
            try
            {
                return UserSessions.SingleOrDefault(a => a.Id == id);
            }
            catch
            {
                return new UserSession { Id = Guid.Empty };
            }
        }

        public void InsertOrUpdate(UserSession usersession)
        {

            if (UserSessions.Exists(a => a.Id == usersession.Id))
            {
                UserSessions.Remove(UserSessions.Single(a => a.Id == usersession.Id));

            }
            if (usersession.Id == Guid.Empty)
                usersession.Id = Guid.NewGuid();
            UserSessions.Add(usersession);
        }

        public void Delete(Guid id)
        {
            try
            {
                UserSessions.Remove(UserSessions.Single(a => a.Id == id));
            }
            catch
            { }
        }

        public void Save()
        {

        }
    }
}
