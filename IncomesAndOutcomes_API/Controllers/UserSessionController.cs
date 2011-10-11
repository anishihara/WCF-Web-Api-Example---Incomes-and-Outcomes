using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncomesAndOutcomes_API.Models;

namespace IncomesAndOutcomes_API.Controllers
{   
    public class UserSessionController : Controller
    {
		private readonly IUserRepository userRepository;
		private readonly IUserSessionRepository usersessionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UserSessionController() : this(new UserRepository(), new UserSessionRepository())
        {
        }

        public UserSessionController(IUserRepository userRepository, IUserSessionRepository usersessionRepository)
        {
			this.userRepository = userRepository;
			this.usersessionRepository = usersessionRepository;
        }

        //
        // GET: /UserSession/

        public ViewResult Index()
        {
            return View(usersessionRepository.AllIncluding());
        }

        //
        // GET: /UserSession/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(usersessionRepository.Find(id));
        }

        //
        // GET: /UserSession/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUser = userRepository.All;
            return View();
        } 

        //
        // POST: /UserSession/Create

        [HttpPost]
        public ActionResult Create(UserSession usersession)
        {
            if (ModelState.IsValid) {
                usersessionRepository.InsertOrUpdate(usersession);
                usersessionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				return View();
			}
        }
        
        //
        // GET: /UserSession/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleUser = userRepository.All;
             return View(usersessionRepository.Find(id));
        }

        //
        // POST: /UserSession/Edit/5

        [HttpPost]
        public ActionResult Edit(UserSession usersession)
        {
            if (ModelState.IsValid) {
                usersessionRepository.InsertOrUpdate(usersession);
                usersessionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				return View();
			}
        }

        //
        // GET: /UserSession/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(usersessionRepository.Find(id));
        }

        //
        // POST: /UserSession/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            usersessionRepository.Delete(id);
            usersessionRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

