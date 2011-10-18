using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IncomesAndOutcomes_API.Models;
using System.ServiceModel.Web;
using IncomesAndOutcomes_API.Resources;
using Microsoft.ApplicationServer.Http.Dispatcher;

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

        [WebInvoke(Method = "POST", UriTemplate = "LogOn")]
        public Guid LogOn(UserResource userResource)
        {
                User user = userRepository.All.SingleOrDefault(a => a.Email == userResource.Email && a.Password == userResource.Password);
      
                if (user != null)
                {
                    if (user.Password == userResource.Password)
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

                }
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden);
                

        }
        [WebInvoke(Method = "POST", UriTemplate = "LogOff")]
        public bool LogOff(Guid UserSession)
        {

                var userSession = userSessionRepository.Find(UserSession);
                if (userSession != null)
                {
                    if (userSession.EndSessionTime == null)
                    {
                        userSession.EndSessionTime = DateTime.Now;
                        userSessionRepository.InsertOrUpdate(userSession);
                        userSessionRepository.Save();
                        return true;
                    }
                }
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden);

        }
    }
}