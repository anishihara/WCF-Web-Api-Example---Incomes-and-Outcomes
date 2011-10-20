using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IncomesAndOutcomes_API.Models;
using IncomesAndOutcomes_API.Resources;
using System.ServiceModel.Web;
using System.Net.Http;
using Microsoft.ApplicationServer.Http.Dispatcher;
using System.Net;

namespace IncomesAndOutcomes_API.API
{
    [ServiceContract]
    public class AccountsAPI
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUserSessionRepository userSessionRepository;
        private readonly IAccountBalanceRepository accountBalanceRepository;
        private readonly IIncomeRepository incomeRepository;
        private readonly IMonthBufferRepository monthBufferRepository;

        public AccountsAPI()
            : this(new AccountRepository(), new UserSessionRepository(),
                new AccountBalanceRepository(), new IncomeRepository(), new MonthBufferRepository())
        {
        }

        public AccountsAPI(IAccountRepository accountRepository, IUserSessionRepository userSessionRepository,
            IAccountBalanceRepository accountBalanceRepository, IIncomeRepository incomeRepository,
            IMonthBufferRepository monthBufferRepository)
        {
            this.accountRepository = accountRepository;
            this.userSessionRepository = userSessionRepository;
            this.accountBalanceRepository = accountBalanceRepository;
            this.incomeRepository = incomeRepository;
            this.monthBufferRepository = monthBufferRepository;
        }

        // TODO: deve voltar um resource, nao um model
        [WebGet(UriTemplate = "")]
        public List<Account> Get()
        {
            try
            {
                Guid userSessionId = new Guid(WebOperationContext.Current.IncomingRequest.Headers.Get("Authorization"));
                var userSession = userSessionRepository.Find(userSessionId);
                if (userSession != null)
                {
                    if (userSession.EndSessionTime == null)
                    {
                        return accountRepository.All.Where(a => a.UserId == userSession.UserId).ToList();
                    }
                    else
                    {
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

            }
            catch
            {
                return null;
            }
        }

        // TODO: deve voltar um resource, nao um model
        [WebGet(UriTemplate = "/{id}")]
        public Account GetAccount(int id)
        {
            try
            {
                Guid userSessionId = new Guid(HttpRequestHeader.Authorization.ToString());
                var userSession = userSessionRepository.Find(userSessionId);
                if (userSession != null)
                {
                    if (userSession.EndSessionTime == null)
                    {
                        return accountRepository.All.SingleOrDefault(a => a.Id == id && a.UserId == userSession.UserId);
                    }
                    else
                    {
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }
            catch
            {
                return null;
            }
        }

        //Na verdade, implementar o account resource
        [WebInvoke(UriTemplate = "", Method = "POST")]
        // todo: melhorar o retorno de statuscode na exception
        public AccountResource Post(AccountPostInput AccountInputPost)
        {

            UserSession userSession = userSessionRepository.All.SingleOrDefault(a => a.Id == AccountInputPost.UserSession &&
                a.EndSessionTime != null);
            if (userSession != null)
            {
                DateTime today = DateTime.Now;
                Account account = accountRepository.All.SingleOrDefault(a => a.Name == AccountInputPost.Name &&
                    a.UserId == userSession.UserId && a.IsDeleted == false);
                if (account != null)
                {
                    throw new HttpResponseException(HttpStatusCode.Conflict);
                }
                account = new Account { Name = AccountInputPost.Name, UserId = userSession.UserId };
                accountRepository.InsertOrUpdate(account);
                accountRepository.Save();
                AccountBalance accountBalance = new AccountBalance
                {
                    AccountId = account.Id,
                    Balance = AccountInputPost.InitialValue,
                    Date = today,
                };

                accountBalanceRepository.InsertOrUpdate(accountBalance);
                accountBalanceRepository.Save();


                MonthBuffer thisMonthBuffer = monthBufferRepository.All.SingleOrDefault(a => a.Month == today.Month && a.Year == today.Year);
                if (thisMonthBuffer == null)
                {
                    thisMonthBuffer = new MonthBuffer
                    {
                        Month = today.Month,
                        Year = today.Year,
                    };

                }
                thisMonthBuffer.Amount += AccountInputPost.InitialValue;
                monthBufferRepository.InsertOrUpdate(thisMonthBuffer);
                monthBufferRepository.Save();

                Income income = new Income
                {
                    Date = today,
                    Amount = AccountInputPost.InitialValue,
                    Memo = String.Format("Valor inicial da conta \"{0}\"", AccountInputPost.Name),
                    AccountBalanceId = accountBalance.Id,
                    MonthBufferId = thisMonthBuffer.Id
                };
                incomeRepository.InsertOrUpdate(income);
                incomeRepository.Save();
                var accountResponse = new AccountResource { Id = account.Id, Name = account.Name, Amount = AccountInputPost.InitialValue };
                return accountResponse;

            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }


        }
    }

    public class AccountPostInput
    {
        public Guid UserSession { get; set; }
        public string Name { get; set; }
        public float InitialValue { get; set; }
    }
}