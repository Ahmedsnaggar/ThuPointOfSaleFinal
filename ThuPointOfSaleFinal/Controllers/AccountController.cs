using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThuPointOfSaleFinal.App.Models;
using ThuPointOfSaleFinal.Entities.ViewModels.Account;

namespace ThuPointOfSaleFinal.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newuser = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Address = ""
                };
               var result =  await _userManager.CreateAsync(newuser, model.Password);
                if (result.Succeeded) 
                {
                    await _userManager.AddToRoleAsync(newuser, "UserRole");
                    await _signInManager.SignInAsync(newuser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors) {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Password is not correct");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult AccessDeniedNew()
        {
            return View();
        }
    }
}
