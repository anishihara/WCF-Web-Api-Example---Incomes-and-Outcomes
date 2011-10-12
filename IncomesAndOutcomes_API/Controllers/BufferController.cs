using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Controllers
{   
    public class BufferController : Controller
    {
		private readonly IMonthBufferRepository monthbufferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public BufferController() : this(new MonthBufferRepository())
        {
        }

        public BufferController(IMonthBufferRepository monthbufferRepository)
        {
			this.monthbufferRepository = monthbufferRepository;
        }

        //
        // GET: /Buffer/

        public ViewResult Index()
        {
            return View(monthbufferRepository.AllIncluding(monthbuffer => monthbuffer.LastBuffer, monthbuffer => monthbuffer.Incomes, monthbuffer => monthbuffer.Budgets));
        }

        //
        // GET: /Buffer/Details/5

        public ViewResult Details(int id)
        {
            return View(monthbufferRepository.Find(id));
        }

        //
        // GET: /Buffer/Create

        public ActionResult Create()
        {
			ViewBag.PossibleLastBuffer = monthbufferRepository.All;
            return View();
        } 

        //
        // POST: /Buffer/Create

        [HttpPost]
        public ActionResult Create(MonthBuffer monthbuffer)
        {
            if (ModelState.IsValid) {
                monthbufferRepository.InsertOrUpdate(monthbuffer);
                monthbufferRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleLastBuffer = monthbufferRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Buffer/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleLastBuffer = monthbufferRepository.All;
             return View(monthbufferRepository.Find(id));
        }

        //
        // POST: /Buffer/Edit/5

        [HttpPost]
        public ActionResult Edit(MonthBuffer monthbuffer)
        {
            if (ModelState.IsValid) {
                monthbufferRepository.InsertOrUpdate(monthbuffer);
                monthbufferRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleLastBuffer = monthbufferRepository.All;
				return View();
			}
        }

        //
        // GET: /Buffer/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(monthbufferRepository.Find(id));
        }

        //
        // POST: /Buffer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            monthbufferRepository.Delete(id);
            monthbufferRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

