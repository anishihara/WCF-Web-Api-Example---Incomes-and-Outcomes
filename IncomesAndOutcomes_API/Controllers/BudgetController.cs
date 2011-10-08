using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncomesAndOutcomes_API.Models;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Controllers
{   
    public class BudgetController : Controller
    {
		private readonly IMonthBufferRepository monthbufferRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IBudgetRepository budgetRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public BudgetController() : this(new MonthBufferRepository(), new CategoryRepository(), new BudgetRepository())
        {
        }

        public BudgetController(IMonthBufferRepository monthbufferRepository, ICategoryRepository categoryRepository, IBudgetRepository budgetRepository)
        {
			this.monthbufferRepository = monthbufferRepository;
			this.categoryRepository = categoryRepository;
			this.budgetRepository = budgetRepository;
        }

        //
        // GET: /Budget/

        public ViewResult Index()
        {
            return View(budgetRepository.AllIncluding(budget => budget.MonthBuffer, budget => budget.Category, budget => budget.Outcomes));
        }

        //
        // GET: /Budget/Details/5

        public ViewResult Details(int id)
        {
            return View(budgetRepository.Find(id));
        }

        //
        // GET: /Budget/Create

        public ActionResult Create()
        {
			ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
			ViewBag.PossibleCategory = categoryRepository.All;
            return View();
        } 

        //
        // POST: /Budget/Create

        [HttpPost]
        public ActionResult Create(Budget budget)
        {
            if (ModelState.IsValid) {
                budgetRepository.InsertOrUpdate(budget);
                budgetRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
				ViewBag.PossibleCategory = categoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Budget/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
			ViewBag.PossibleCategory = categoryRepository.All;
             return View(budgetRepository.Find(id));
        }

        //
        // POST: /Budget/Edit/5

        [HttpPost]
        public ActionResult Edit(Budget budget)
        {
            if (ModelState.IsValid) {
                budgetRepository.InsertOrUpdate(budget);
                budgetRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleMonthBuffer = monthbufferRepository.All;
				ViewBag.PossibleCategory = categoryRepository.All;
				return View();
			}
        }

        //
        // GET: /Budget/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(budgetRepository.Find(id));
        }

        //
        // POST: /Budget/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            budgetRepository.Delete(id);
            budgetRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

