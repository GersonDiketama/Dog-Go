using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Repositories;
using DogGo.Models;
using DogGo.Models.ViewModels;

namespace DogGo.Controllers
{
    public class WalkerController : Controller
    {
        // GET: WalkesController


        private readonly IWalkerRepository _walkerRepo;

        private readonly IwalkRepository _walkRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkerController(IWalkerRepository walkerRepository, IwalkRepository walkRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
        }


        public ActionResult Index()
        {

            // GET: Walkers
          
                List<Walker> walkers = _walkerRepo.GetAllWalkers();

                return View(walkers);
        }

        // GET: WalkesController/Details/5
        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            List<Walk> walks = _walkRepo.GetWalksByWalkerId(id);

            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };

            if (walker == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: WalkesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
