using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Repositories;
using DogGo.Models;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {

        private readonly IOwnerRepository _ownerRepo;

        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepo = ownerRepository;
        }

        // GET: OwnersController1
        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();

            return View(owners);
        }

        // GET: OwnersController1/Details/5
        public ActionResult Details(int id)
        {
            Owner owner = _ownerRepo.GetOwnersById(id);

            if (owner == null)
            {
                return NotFound();
            }


            return View(owner);
        }

        // GET: OwnersController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnersController1/Create
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

        // GET: OwnersController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OwnersController1/Edit/5
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

        // GET: OwnersController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OwnersController1/Delete/5
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
