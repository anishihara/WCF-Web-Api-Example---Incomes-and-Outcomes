using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Controllers
{   
    public class AccountBalanceController : Controller
    {
		private readonly IAccountRepository accountRepository;
		private readonly IAccountBalanceRepository accountbalanceRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountBalanceController() : this(new AccountRepository(), new AccountBalanceRepository())
        {
        }

        public AccountBalanceController(IAccountRepository accountRepository, IAccountBalanceRepository accountbalanceRepository)
        {
			this.accountRepository = accountRepository;
			this.accountbalanceRepository = accountbalanceRepository;
        }

        //
        // GET: /AccountBalance/

        public ViewResult Index()
        {
            return View(accountbalanceRepository.AllIncluding(accountbalance => accountbalance.Account, accountbalance => accountbalance.Incomes, accountbalance => accountbalance.Outcomes));
        }

        //
        // GET: /AccountBalance/Details/5

        public ViewResult Details(int id)
        {
            return View(accountbalanceRepository.Find(id));
        }

        //
        // GET: /AccountBalance/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAccount = accountRepository.All;
            return View();
        } 

        //
        // POST: /AccountBalance/Create

        [HttpPost]
        public ActionResult Create(AccountBalance accountbalance)
        {
            if (ModelState.IsValid) {
                accountbalanceRepository.InsertOrUpdate(accountbalance);
                accountbalanceRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccount = accountRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AccountBalance/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAccount = accountRepository.All;
             return View(accountbalanceRepository.Find(id));
        }

        //
        // POST: /AccountBalance/Edit/5

        [HttpPost]
        public ActionResult Edit(AccountBalance accountbalance)
        {
            if (ModelState.IsValid) {
                accountbalanceRepository.InsertOrUpdate(accountbalance);
                accountbalanceRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccount = accountRepository.All;
				return View();
			}
        }

        //
        // GET: /AccountBalance/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(accountbalanceRepository.Find(id));
        }

        //
        // POST: /AccountBalance/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            accountbalanceRepository.Delete(id);
            accountbalanceRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

