using Microsoft.AspNetCore.Authorization;
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
        private IWebHostEnvironment _environment;
        private IUploadFile _file;
        public ProductsController(IGenericRepository<Product> repository, IGenericRepository<Category> repositoryCategory, IWebHostEnvironment environment, IUploadFile file)
        {
            _repository = repository;
            _repositoryCategory = repositoryCategory;
            _environment = environment;
            _file = file;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index(string search = "")
        {
            var products = await _repository.GetAllAsync(includes: new[] { "category" });
            if(search!= "")
            {
                ViewBag.Search = search;
                products = products.Where(p=> p.ProductName.ToLower().Contains(search.ToLower()));
            }
            return View(products);
        }
        public async Task<ActionResult> SearchByAjax(string search = "")
        {
            var products = await _repository.GetAllAsync(includes: new[] { "category" });
            if (search != "")
            {
                ViewBag.Search = search;
                products = products.Where(p => p.ProductName.ToLower().Contains(search.ToLower()));
            }
            return PartialView("_ProductsCards", products);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize]
        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _repositoryCategory.GetAllAsync();
            var prod = new Product() { GategoriesList = categories.ToList() };
            return View(prod);
        }
        [Authorize]
        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product item)
        {
            try
            {
                if (item.ImageFile != null) {
                   string Image =  await _file.UploadFileAsync("\\Images\\ProductImages", item.ImageFile);
                    item.ProductImage = Image;
                }                
                await _repository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _repository.GetAsync(id);
            return View("EditProduct", product);
        }
        [Authorize]
        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product item)
        {
            try
            {
                if (item.ImageFile != null)
                {
                    string Image = await _file.UploadFileAsync("\\Images\\ProductImages", item.ImageFile);
                    item.ProductImage = Image;
                }
                await _repository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditProduct");
            }
        }
        [Authorize]
        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _repository.GetAsync(id);
            return View("DeleteProduct", product);
        }
        [Authorize]
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
