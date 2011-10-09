using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IncomesAndOutcomes_API.Models;
using System.ServiceModel.Web;
using System.Net.Http;

namespace IncomesAndOutcomes_API.API
{
    [ServiceContract]
    public class AccountsAPI
    {
        private readonly IAccountRepository accountRepository;

        public AccountsAPI(): this(new AccountRepository())
        { 
        }

        public AccountsAPI(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [WebGet (UriTemplate="")]
        public IQueryable<Account> Get()
        {
            try
            {
                return accountRepository.All;
            }
            catch
            {
                return null;
            }
        }

        //Na verdade, implementar o account resource
        [WebInvoke (UriTemplate="",Method="POST")]
        public HttpResponseMessage Post(Account account)
        {
            try
            {
                accountRepository.InsertOrUpdate(account);
                accountRepository.Save();
                return new HttpResponseMessage();
            }
            catch
            {
                var response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
        }
    }
}