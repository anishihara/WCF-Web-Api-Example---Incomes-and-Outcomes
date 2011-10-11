using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IncomesAndOutcomes_API.Models;
using System.ServiceModel.Web;
using IncomesAndOutcomes_API.Resources;

namespace IncomesAndOutcomes_API.API
{
    [ServiceContract]
    public class AuthAPI
    {
        private readonly IUserRepository userRepository;
        private readonly IUserSessionRepository userSessionRepository;

        public AuthAPI()
            : this(new UserRepository(), new UserSessionRepository())
        {

        }

        public AuthAPI(IUserRepository userRepository, IUserSessionRepository userSessionRepository)
        {
            this.userRepository = userRepository;
            this.userSessionRepository = userSessionRepository;
        }

        [WebInvoke(Method = "POST", UriTemplate = "")]
        public Guid LogOn(UserResource userResource)
        {
            try
            {
                User user = userRepository.All.Single(a => a.Email == userResource.Email && a.Password == userResource.Password);
                if (user != null)
                {
                    UserSession userSession = new UserSession();
                    userSession.OpenSessionTime = DateTime.Now;
                    userSession.UserId = user.Id;
                    userSessionRepository.InsertOrUpdate(userSession);
                    UserSession previousUserSession;
                    try
                    {
                          previousUserSession = userSessionRepository.All.Single(a => a.User.Id == user.Id && !a.EndSessionTime.HasValue);
                          previousUserSession.EndSessionTime = DateTime.Now;
                          userSessionRepository.InsertOrUpdate(previousUserSession);
                    }
                    catch
                    {

                    }
                    userSessionRepository.Save();
                    return userSession.Id;

                }
                return Guid.Empty;
                
            }
            catch
            {
                return Guid.Empty;
            }

        }
    }
}