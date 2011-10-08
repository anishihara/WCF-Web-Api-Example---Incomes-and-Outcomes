using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncomesAndOutcomes_API.Models;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Controllers
{   
    public class OutcomeController : Controller
    {
		private readonly IAccountBalanceRepository accountbalanceRepository;
		private readonly IBudgetRepository budgetRepository;
		private readonly IOutcomeRepository outcomeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OutcomeController() : this(new AccountBalanceRepository(), new BudgetRepository(), new OutcomeRepository())
        {
        }

        public OutcomeController(IAccountBalanceRepository accountbalanceRepository, IBudgetRepository budgetRepository, IOutcomeRepository outcomeRepository)
        {
			this.accountbalanceRepository = accountbalanceRepository;
			this.budgetRepository = budgetRepository;
			this.outcomeRepository = outcomeRepository;
        }

        //
        // GET: /Outcome/

        public ViewResult Index()
        {
            return View(outcomeRepository.AllIncluding(outcome => outcome.AccountBalance, outcome => outcome.Budget));
        }

        //
        // GET: /Outcome/Details/5

        public ViewResult Details(int id)
        {
            return View(outcomeRepository.Find(id));
        }

        //
        // GET: /Outcome/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
			ViewBag.PossibleBudget = budgetRepository.All;
            return View();
        } 

        //
        // POST: /Outcome/Create

        [HttpPost]
        public ActionResult Create(Outcome outcome)
        {
            if (ModelState.IsValid) {
                outcomeRepository.InsertOrUpdate(outcome);
                outcomeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
				ViewBag.PossibleBudget = budgetRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Outcome/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
			ViewBag.PossibleBudget = budgetRepository.All;
             return View(outcomeRepository.Find(id));
        }

        //
        // POST: /Outcome/Edit/5

        [HttpPost]
        public ActionResult Edit(Outcome outcome)
        {
            if (ModelState.IsValid) {
                outcomeRepository.InsertOrUpdate(outcome);
                outcomeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
				ViewBag.PossibleBudget = budgetRepository.All;
				return View();
			}
        }

        //
        // GET: /Outcome/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(outcomeRepository.Find(id));
        }

        //
        // POST: /Outcome/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            outcomeRepository.Delete(id);
            outcomeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

