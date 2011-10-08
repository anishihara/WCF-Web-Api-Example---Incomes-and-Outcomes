using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncomesAndOutcomes_API.Models;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Controllers
{   
    public class AccountaController : Controller
    {
		private readonly IAccountRepository accountRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountaController() : this(new AccountRepository())
        {
        }

        public AccountaController(IAccountRepository accountRepository)
        {
			this.accountRepository = accountRepository;
        }

        //
        // GET: /Accounta/

        public ViewResult Index()
        {
            return View(accountRepository.AllIncluding(account => account.AccountBalances));
        }

        //
        // GET: /Accounta/Details/5

        public ViewResult Details(int id)
        {
            return View(accountRepository.Find(id));
        }

        //
        // GET: /Accounta/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Accounta/Create

        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid) {
                accountRepository.InsertOrUpdate(account);
                accountRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Accounta/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(accountRepository.Find(id));
        }

        //
        // POST: /Accounta/Edit/5

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid) {
                accountRepository.InsertOrUpdate(account);
                accountRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Accounta/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(accountRepository.Find(id));
        }

        //
        // POST: /Accounta/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            accountRepository.Delete(id);
            accountRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

