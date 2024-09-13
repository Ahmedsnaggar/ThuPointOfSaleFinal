using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThuPointOfSaleFinal.App.Interfaces;
using ThuPointOfSaleFinal.Entities.Models;

namespace ThuPointOfSaleFinal.App.Controllers
{
    public class StoreController : Controller
    {
        private IGenericRepository<Store> _storeRepository;

        public StoreController(IGenericRepository<Store> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        //private IStoreRepository _storeRepository;
        //public StoreController(IStoreRepository storeRepository)
        //{
        //    _storeRepository = storeRepository;
        //}

        // GET: StoreController
        public async Task<ActionResult> Index()
        {
            //var stores = await _storeRepository.GetAllStores();
            var stores = await _storeRepository.GetAllAsync();
            return View("StoreList", stores);
        }

        // GET: StoreController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var store = await _storeRepository.GetAsync(id);
            return View("StoreDetails", store);
        }

        // GET: StoreController/Create
        public ActionResult Create()
        {
            return View("CreateStore");
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Store item)
        {
            try
            {
               await  _storeRepository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("CreateStore");
            }
        }

        // GET: StoreController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var store = await _storeRepository.GetAsync(id);
            return View("EditStore", store);
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Store item)
        {
            try
            {
               await  _storeRepository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditStore");
            }
        }

        // GET: StoreController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var store =  await _storeRepository.GetAsync(id);
            return View("DeleteStore", store);
        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
              await   _storeRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("DeleteStore");
            }
        }
    }
}
