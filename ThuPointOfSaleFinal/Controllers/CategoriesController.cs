using Microsoft.AspNetCore.Mvc;
using ThuPointOfSaleFinal.App.Interfaces;
using ThuPointOfSaleFinal.Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace ThuPointOfSaleFinal.App.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private IGenericRepository<Category> _categoryRepository;
        public CategoriesController(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [AllowAnonymous]
        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Category category = await _categoryRepository.GetAsync(id);
            return View(category);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category Item)
        {
            try
            {
                var category = _categoryRepository.GetAllAsync().Result.Any(c=> c.CategoryName == Item.CategoryName);
                if(category == true)
                {
                    ViewBag.Error = "Category Name Already Exists";
                    return View();
                }
                await _categoryRepository.AddAsync(Item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Category category = await _categoryRepository.GetAsync(id);
            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category item)
        {
            try
            {
               await  _categoryRepository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Category category = await _categoryRepository.GetAsync(id);
            return View(category);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
