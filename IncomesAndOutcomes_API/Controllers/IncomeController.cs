using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncomesAndOutcomes_API.Models;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Controllers
{   
    public class IncomeController : Controller
    {
		private readonly IAccountBalanceRepository accountbalanceRepository;
		private readonly IMonthBufferRepository monthbufferRepository;
		private readonly IIncomeRepository incomeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public IncomeController() : this(new AccountBalanceRepository(), new MonthBufferRepository(), new IncomeRepository())
        {
        }

        public IncomeController(IAccountBalanceRepository accountbalanceRepository, IMonthBufferRepository monthbufferRepository, IIncomeRepository incomeRepository)
        {
			this.accountbalanceRepository = accountbalanceRepository;
			this.monthbufferRepository = monthbufferRepository;
			this.incomeRepository = incomeRepository;
        }

        //
        // GET: /Income/

        public ViewResult Index()
        {
            return View(incomeRepository.AllIncluding(income => income.AccountBalance, income => income.MonthBuffer));
        }

        //
        // GET: /Income/Details/5

        public ViewResult Details(int id)
        {
            return View(incomeRepository.Find(id));
        }

        //
        // GET: /Income/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
			ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
            return View();
        } 

        //
        // POST: /Income/Create

        [HttpPost]
        public ActionResult Create(Income income)
        {
            if (ModelState.IsValid) {
                incomeRepository.InsertOrUpdate(income);
                incomeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
				ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Income/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
			ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
             return View(incomeRepository.Find(id));
        }

        //
        // POST: /Income/Edit/5

        [HttpPost]
        public ActionResult Edit(Income income)
        {
            if (ModelState.IsValid) {
                incomeRepository.InsertOrUpdate(income);
                incomeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccountBalance = accountbalanceRepository.All;
				ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
				return View();
			}
        }

        //
        // GET: /Income/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(incomeRepository.Find(id));
        }

        //
        // POST: /Income/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            incomeRepository.Delete(id);
            incomeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

