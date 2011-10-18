using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Tests.RepositoryMockups
{
    public class UserRepositoryMockup : IUserRepository
    {
        private List<User> Users { get; set; }
        public UserRepositoryMockup()
        {
            Users = new List<User>();
            Users.Add(new User { Email = @"nishihara@gmail.com", Password = "Teste" });
        }

        public IQueryable<User> All
        {
            get { return Users.AsQueryable(); }
        }

        public IQueryable<User> AllIncluding(params System.Linq.Expressions.Expression<Func<User, object>>[] includeProperties)
        {
            return Users.AsQueryable();
        }

        public User Find(int id)
        {
            return Users.Single(a => a.Id == id);
        }

        public void InsertOrUpdate(User user)
        {
            if (Users.Exists(a => a.Id == user.Id))
            {
                Users.Remove(Users.Single(a => a.Id == user.Id));
            }
            Users.Add(user);
        }

        public void Delete(int id)
        {
            Users.Remove(Users.Single(a => a.Id == id));
        }

        public void Save()
        {
    
        }
    }
}
