using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IncomesAndOutcomes_API.Models;
using System.ServiceModel.Web;
using System.Linq;

namespace IncomesAndOutcomes_API.API
{
    [ServiceContract]
    public class AccountsAPI
    {
        AccountRepository accountRepository = new AccountRepository();

        [WebGet (UriTemplate="")]
        public IQueryable<Account> Get()
        {
            return accountRepository.AllIncluding();
        }
    }
}