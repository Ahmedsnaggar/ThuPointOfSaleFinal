using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuPointOfSaleFinal.App.Interfaces;
using ThuPointOfSaleFinal.App.Models;
using ThuPointOfSaleFinal.Entities.Models;

namespace ThuPointOfSaleFinal.App.Controllers
{
    public class ProductsController : Controller
    {
        private IGenericRepository<Product> _repository;
        private IGenericRepository<Category> _repositoryCategory;
        public ProductsController(IGenericRepository<Product> repository, IGenericRepository<Category> repositoryCategory)
        {
            _repository = repository;
            _repositoryCategory = repositoryCategory;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            //var products = from p in _db.Products
            //               join c in _db.Categories on p.categoryId equals c.Id
            //               select p;
            var products = await _repository.GetAllAsync(includes: new[] { "category" });
            return View(products);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _repositoryCategory.GetAllAsync();
            var prod = new Product() { GategoriesList = categories.ToList() };
            return View(prod);
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product item)
        {
            try
            {
                await _repository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _repository.GetAsync(id);
            return View("CreateProduct", product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product item)
        {
            try
            {
                await _repository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("CreateProduct");
            }
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _repository.GetAsync(id);
            return View("DeleteProduct", product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("DeleteProduct");
            }
        }
    }
}
