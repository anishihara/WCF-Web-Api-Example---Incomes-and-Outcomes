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
using IncomesAndOutcomes_API.Helpers;

namespace IncomesAndOutcomes_API.API
{
    [ServiceContract]
    public class AccountsAPI
    {
        private readonly IAccountRepository accountRepository;
        private readonly IAccountBalanceRepository accountBalanceRepository;
        private readonly IIncomeRepository incomeRepository;
        private readonly IMonthBufferRepository monthBufferRepository;

        public AccountsAPI()
            : this(new AccountRepository(),
                new AccountBalanceRepository(), new IncomeRepository(), new MonthBufferRepository())
        {
        }

        public AccountsAPI(IAccountRepository accountRepository,
            IAccountBalanceRepository accountBalanceRepository, IIncomeRepository incomeRepository,
            IMonthBufferRepository monthBufferRepository)
        {
            this.accountRepository = accountRepository;
            this.accountBalanceRepository = accountBalanceRepository;
            this.incomeRepository = incomeRepository;
            this.monthBufferRepository = monthBufferRepository;
        }

        // TODO: deve voltar um resource, nao um model
        [WebGet(UriTemplate = "")]
        public HttpResponseMessage<List<Account>> Get()
        {
            var userSession = new AuthHelper().Authenticate();
            if (userSession != null)
            {

                return new HttpResponseMessage<List<Account>>(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<Account>>(accountRepository.All.Where(a => a.UserId == userSession.UserId && !a.IsDeleted).ToList()),
                };
            }
            return null;


        }

        [WebGet(UriTemplate = "/{id}")]
        public HttpResponseMessage<AccountResource> GetAccount(int id)
        {
            var userSession = new AuthHelper().Authenticate();
            var account = accountRepository.All.SingleOrDefault(a => a.Id == id
                && a.UserId == userSession.UserId
                && !a.IsDeleted);
            if (account != null)
            {
                var accountBalance = accountBalanceRepository.All.SingleOrDefault(a => a.IsActive && !a.IsDeleted && a.AccountId == account.Id);
                if (accountBalance != null)
                {
                    return new HttpResponseMessage<AccountResource>(HttpStatusCode.Found)
                    {
                        Content = new ObjectContent<AccountResource>(new AccountResource { Name = account.Name, Id = account.Id, Amount = accountBalance.Balance }),
                    };
                }
            }
            return new HttpResponseMessage<AccountResource>(HttpStatusCode.NotFound);
        }

        [WebInvoke(UriTemplate = "/{id}", Method = "PUT")]
        public HttpResponseMessage<AccountResource> EditAccount(int id, AccountResource AccountResource)
        {
            var userSession = new AuthHelper().Authenticate();
            var account = accountRepository.All.SingleOrDefault(a => a.Id == id && !a.IsDeleted && a.UserId == userSession.UserId);
            if (account != null)
            {
                var accountBalance = accountBalanceRepository.All.SingleOrDefault(a => a.IsActive && !a.IsDeleted && a.AccountId == account.Id);
                if (accountBalance != null)
                {
                    account.Name = AccountResource.Name;
                    accountRepository.InsertOrUpdate(account);
                    accountRepository.Save();
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                    return new HttpResponseMessage<AccountResource>(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<AccountResource>(new AccountResource { Name = account.Name, Id = account.Id, Amount = accountBalance.Balance }),
                    };
                }
            }
            return new HttpResponseMessage<AccountResource>(HttpStatusCode.NotFound);

        }

        // Apenas deletar (mudar estado para IsDeleted) se o último balanço for igual a zero
        [WebInvoke(UriTemplate = "/{id}", Method = "DELETE")]
        public HttpResponseMessage DeleteAccount(int id)
        {
            var userSession = new AuthHelper().Authenticate();
            var account = accountRepository.All.SingleOrDefault(a => a.Id == id && !a.IsDeleted && a.UserId == userSession.UserId);
            if (account != null)
            {
                var accountBalance = accountBalanceRepository.All.SingleOrDefault(a => a.IsActive &&
                    !a.IsDeleted &&
                    a.AccountId == account.Id &&
                    a.Balance < 0.01 &&
                    a.Balance > -0.01 );
                if (accountBalance != null)
                {
                    account.IsDeleted = true;
                    accountRepository.InsertOrUpdate(account);
                    accountRepository.Save();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return new HttpResponseMessage(HttpStatusCode.NotModified);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

        //Na verdade, implementar o account resource
        [WebInvoke(UriTemplate = "", Method = "POST")]
        // todo: melhorar o retorno de statuscode na exception
        public HttpResponseMessage<AccountResource> Add(AccountResource AccountInput)
        {

            UserSession userSession = new AuthHelper().Authenticate();
            if (userSession != null)
            {
                float amount = AccountInput.Amount;
                string accountName = AccountInput.Name;
                DateTime today = DateTime.Now;
                Account account = accountRepository.All.SingleOrDefault(a => a.Name == accountName &&
                    a.UserId == userSession.UserId && a.IsDeleted == false);
                if (account != null)
                {
                    throw new HttpResponseException(HttpStatusCode.Conflict);
                }
                account = new Account { Name = accountName, UserId = userSession.UserId };
                accountRepository.InsertOrUpdate(account);
                accountRepository.Save();
                AccountBalance accountBalance = new AccountBalance
                {
                    AccountId = account.Id,
                    Balance = amount,
                    Date = today,
                    IsActive = true,
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
                thisMonthBuffer.Amount += amount;
                monthBufferRepository.InsertOrUpdate(thisMonthBuffer);
                monthBufferRepository.Save();

                Income income = new Income
                {
                    Date = today,
                    Amount = amount,
                    Memo = String.Format("Valor inicial da conta \"{0}\"", accountName),
                    AccountBalanceId = accountBalance.Id,
                    MonthBufferId = thisMonthBuffer.Id
                };
                incomeRepository.InsertOrUpdate(income);
                incomeRepository.Save();
                var accountResponse = new AccountResource { Id = account.Id, Name = account.Name, Amount = amount };
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;
                return new HttpResponseMessage<AccountResource>(HttpStatusCode.Created)
                {
                    Content = new ObjectContent<AccountResource>(accountResponse)
                };

            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }


        }
    }
}