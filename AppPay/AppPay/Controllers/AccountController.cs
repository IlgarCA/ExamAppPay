using AppPay.Models;
using AppPay.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppPay.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) { return View(registerVM); }

            AppUser appUser = new AppUser()
            {
                FullName = registerVM.FullName,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
            };
            var result = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
                return View(registerVM);
            }
            var roleResult = await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            await _userManager.AddToRoleAsync(appUser, "admin");

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) { return View(loginVM); }

            AppUser user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user is null)
            {
                ModelState.AddModelError("", "User not found");
                return View(loginVM);
            }
            if (!await _userManager.CheckPasswordAsync(user, loginVM.Password))
            {
                ModelState.AddModelError("", "Username or password are incorect!");
                return View(loginVM);
            }
            await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}



