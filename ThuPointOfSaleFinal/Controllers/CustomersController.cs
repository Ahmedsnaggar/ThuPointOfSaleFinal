using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThuPointOfSaleFinal.App.Interfaces;
using ThuPointOfSaleFinal.Entities.Models;

namespace ThuPointOfSaleFinal.App.Controllers
{
    [Authorize(Roles ="AdminRole")]
    public class CustomersController : Controller
    {
        private IGenericRepository<Customer> _customerRepository;
        public CustomersController(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [AllowAnonymous]
        [Authorize(Roles = "AdminRole, UserRole")]
        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            var customer = await _customerRepository.GetAllAsync();
            return View("CustomersList", customer);
        }

        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return View("CustomerDetails", customer);
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View("CreateCustomer");
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer item)
        {
            try
            {
                await _customerRepository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Errors = ex.Message;
                return View("CreateCustomer");
            }
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return View("EditCustomer", customer);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Customer item)
        {
            try
            {
                await _customerRepository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Errors = ex.Message;
                return View("EditCustomer");
            }
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return View("DeleteCustomer", customer);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _customerRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("DeleteCustomer");
            }
        }
    }
}
