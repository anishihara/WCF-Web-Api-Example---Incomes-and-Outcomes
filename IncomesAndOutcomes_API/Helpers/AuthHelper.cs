using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;
using IncomesAndOutcomes_API.Models;
using Microsoft.ApplicationServer.Http.Dispatcher;
using System.Net;

namespace IncomesAndOutcomes_API.Helpers
{
    public class AuthHelper
    {
        private readonly IUserSessionRepository userSessionRepository;

        public AuthHelper() : this(new UserSessionRepository()) { }

        public AuthHelper(IUserSessionRepository userSessionRepository)
        {
            this.userSessionRepository = userSessionRepository;
        }

        public UserSession Authenticate()
        {

            string stringGuid = WebOperationContext.Current.IncomingRequest.Headers.Get("Authorization");
            if (stringGuid == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            Guid userSessionId = new Guid(stringGuid);
            var userSession = userSessionRepository.Find(userSessionId);
            if (userSession != null)
            {
                if (userSession.EndSessionTime != null)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
                return userSession;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
    }
}